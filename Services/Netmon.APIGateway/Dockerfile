FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/Netmon.APIGateway/Netmon.APIGateway.csproj", "Services/Netmon.APIGateway/"]
RUN dotnet restore "Services/Netmon.APIGateway/Netmon.APIGateway.csproj"
COPY . .
WORKDIR "/src/Services/Netmon.APIGateway"
RUN dotnet build "Netmon.APIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Netmon.APIGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Netmon.APIGateway.dll"]
