using ECommercePaymentIntegration.Application.Abstraction;
using ECommercePaymentIntegration.Domain.DTO.Request;
using ECommercePaymentIntegration.Domain.DTO.Response;
using ECommercePaymentIntegration.Domain.Validators;
using ECommercePaymentIntegration.Infrastructure;
using ECommercePaymentIntegration.Integrations.Abstracts;
using ECommercePaymentIntegration.Integrations.Models.BalanceManagement.Request;
using ECommercePaymentIntegration.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using ApplicationException = ECommercePaymentIntegration.Shared.Exceptions.ApplicationException;

namespace ECommercePaymentIntegration.Application.Concretes
{
    public class OrdersService(IProductService _productService, IBalanceIntegrationService _balanceIntegration, ECommerceDbContext db)
                             : IOrdersService
    {
        public async Task<OrderCreateResponse> CompleteOrderAsync(string orderId, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(orderId))
                throw new ApplicationException("OrderId is required.");

            if (Guid.TryParse(orderId, out var orderGuid) == false)
                throw new ApplicationException("Invalid OrderId format.");

            Guid id = Guid.Parse(orderId);

            var order = await db.Orders.Where(o => o.Id == id).FirstOrDefaultAsync(ct);

            if (order == null)
                throw new NotFoundException("Order not found.");

            if (order.OrderStatus != Domain.Enum.OrderStatus.Created)
                throw new ApplicationException("Only orders with status 'Created' can be completed.");

            var completeOrderResponse = await _balanceIntegration.CompleteOrder(new CompleteRequest
            {
                orderId = order.Id.ToString()
            }, ct);

            if (!completeOrderResponse.Success.HasValue || !completeOrderResponse.Success.Value)
            {
                order.OrderStatus = Domain.Enum.OrderStatus.Failed;
                order.CancelledAt = DateTime.Now;
                order.UpdatedAt = DateTime.Now;
                db.Orders.Update(order);

                await db.SaveChangesAsync(ct);
                throw new ApplicationException("Complete order failed. Balance service is not available.Order canceled");
            }

            order.UpdatedAt = DateTime.Now;
            order.OrderStatus = Domain.Enum.OrderStatus.Completed;
            order.CompletedAt = DateTime.Now;

            db.Orders.Update(order);

            await db.SaveChangesAsync(ct);


            return new OrderCreateResponse
            {
                Amount = order.TotalPrice,
                OrderId = order.Id,
            };
        }

        public async Task<OrderCreateResponse> CreateOrderAsync(CreateOrderRequest req, CancellationToken ct)
        {
            req.ValidateCreateOrderRequest();

            var products = await _productService.GetProductItemsAsync(ct);

            var selectedProducts = products.Where(v => req.Products.Any(p => p.ProductId == v.Id));

            if (selectedProducts.Count() != req.Products.Count)
                throw new ApplicationException("Some of the products are not found.");

            var stockControl = selectedProducts
                                .Where(v => req.Products.Any(a => a.ProductId == v.Id && a.Quantity > v.Stock))
                                .Any();

            if (stockControl)
                throw new ApplicationException("Some of the products are out of stock.");


            decimal totalAmount = selectedProducts.Sum(s => s.Price * req.Products.First(f => f.ProductId == s.Id).Quantity);

            var balanceResponse = await _balanceIntegration.GetBalance(ct);

            if (!balanceResponse.Success.HasValue || !balanceResponse.Success.Value)
                throw new ApplicationException("Balance service is not available.");

            var balanceData = balanceResponse.Data;

            if (balanceData == null || balanceData.availableBalance < totalAmount)
                throw new ApplicationException("Insufficient balance.");


            var order = new Domain.Entities.Order
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Currency = balanceData.currency,
                ExternalOrderId = Guid.NewGuid().ToString(),
                OrderStatus = Domain.Enum.OrderStatus.Created,
                UserId = balanceData.userId,
                TotalPrice = totalAmount,
            };

            db.Orders.Add(order);
            await db.SaveChangesAsync(ct);

            db.OrderItems.AddRange(selectedProducts.Select(s => new Domain.Entities.OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = s.Id,
                Quantity = req.Products.First(f => f.ProductId == s.Id).Quantity,
                Category = s.Category,
                Currency = s.Currency,
                Price = s.Price,
                PriceTotal = s.Price * req.Products.First(f => f.ProductId == s.Id).Quantity
            }));

            await db.SaveChangesAsync(ct);

            var preOrder = await _balanceIntegration.PreOrder(new Integrations.Models.BalanceManagement.Request.PreOrder
            {
                amount = totalAmount,
                orderId = order.Id.ToString()
            }, ct);


            if (!preOrder.Success.HasValue || !preOrder.Success.Value)
            {
                order.OrderStatus = Domain.Enum.OrderStatus.Failed;
                order.UpdatedAt = DateTime.Now;
                order.CancelledAt = DateTime.Now;
                db.Orders.Update(order);
                await db.SaveChangesAsync(ct);

                throw new ApplicationException("PreOrder failed. Balance service is not available.");
            }

            return new OrderCreateResponse
            {
                Amount = totalAmount,
                OrderId = order.Id,
            };
        }
    }
}
