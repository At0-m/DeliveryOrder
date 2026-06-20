FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["backend/DeliveryOrder.Api/DeliveryOrder.Api.csproj", "backend/DeliveryOrder.Api/"]
RUN dotnet restore "backend/DeliveryOrder.Api/DeliveryOrder.Api.csproj"
COPY . .
WORKDIR /src/backend/DeliveryOrder.Api
RUN dotnet publish "DeliveryOrder.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DeliveryOrder.Api.dll"]
