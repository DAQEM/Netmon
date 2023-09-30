# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the DeviceManagerService.csproj
COPY ["Services/DeviceManagerService/DeviceManagerService.csproj", "./"]
COPY ["Shared/DevicesLib/DevicesLib.csproj", "/Shared/DevicesLib/"]

# Restore dependencies for DeviceManagerService
RUN dotnet restore "DeviceManagerService.csproj"

# Copy the Shared project
RUN dotnet restore "/Shared/DevicesLib/DevicesLib.csproj"

# Copy the rest of the source code
COPY ["Services/DeviceManagerService/", "./"]
COPY ["Shared/DevicesLib/", "/Shared/DevicesLib/"]

# Build DeviceManagerService
WORKDIR "/src/"
RUN dotnet build "DeviceManagerService.csproj" -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "DeviceManagerService.csproj" -c Release -o /app/publish

# Stage 3: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# Copy the published files from the previous stage
COPY --from=publish /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "DeviceManagerService.dll"]
