# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the SNMPPollingService.csproj
COPY ["Services/SNMPPollingService/SNMPPollingService.csproj", "./"]
COPY ["Shared/DevicesLib/DevicesLib.csproj", "/Shared/DevicesLib/"]

# Restore dependencies for SNMPPollingService
RUN dotnet restore "SNMPPollingService.csproj"

# Copy the Shared project
RUN dotnet restore "/Shared/DevicesLib/DevicesLib.csproj"

# Copy the rest of the source code
COPY ["Services/SNMPPollingService/", "./"]
COPY ["Shared/DevicesLib/", "/Shared/DevicesLib/"]

# Build SNMPPollingService
WORKDIR "/src/"
RUN dotnet build "SNMPPollingService.csproj" -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "SNMPPollingService.csproj" -c Release -o /app/publish

# Stage 3: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# Copy the published files from the previous stage
COPY ["Services/SNMPPollingService/test.sh", "./"]
COPY --from=publish /app/publish .

# Install prerequisites
RUN apt-get update && apt-get install -y \
curl

# Set the entry point
ENTRYPOINT ["dotnet", "SNMPPollingService.dll"]
