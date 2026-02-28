FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ECommercePaymentIntegration.API/ECommercePaymentIntegration.API.csproj", "ECommercePaymentIntegration.API/"]
COPY ["ECommercePaymentIntegration.Application/ECommercePaymentIntegration.Application.csproj", "ECommercePaymentIntegration.Application/"]
COPY ["ECommercePaymentIntegration.Domain/ECommercePaymentIntegration.Domain.csproj", "ECommercePaymentIntegration.Domain/"]
COPY ["ECommercePaymentIntegration.Infrastructure/ECommercePaymentIntegration.Infrastructure.csproj", "ECommercePaymentIntegration.Infrastructure/"]
COPY ["ECommercePaymentIntegration.Shared/ECommercePaymentIntegration.Shared.csproj", "ECommercePaymentIntegration.Shared/"]
COPY ["ECommercePaymentIntegration.Integrations/ECommercePaymentIntegration.Integrations.csproj", "ECommercePaymentIntegration.Integrations/"]

RUN dotnet restore "ECommercePaymentIntegration.API/ECommercePaymentIntegration.API.csproj"

COPY . .
WORKDIR "/src/ECommercePaymentIntegration.API"
RUN dotnet build "ECommercePaymentIntegration.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommercePaymentIntegration.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=publish /app/publish .

# Create data directory with proper permissions
RUN mkdir -p /app/data && chmod 777 /app/data

EXPOSE 8080 8443
ENTRYPOINT ["dotnet", "ECommercePaymentIntegration.API.dll"]
