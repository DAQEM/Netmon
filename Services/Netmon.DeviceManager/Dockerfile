# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the Netmon.DeviceManager.csproj
COPY ["Services/Netmon.DeviceManager/Netmon.DeviceManager.csproj", "./"]

COPY ["Shared/Netmon.Data/Netmon.Data.csproj", "/Shared/Netmon.Data/"]
COPY ["Shared/Netmon.Data.EntityFramework/Netmon.Data.EntityFramework.csproj", "/Shared/Netmon.Data.EntityFramework/"]
COPY ["Shared/Netmon.Data.EntityFramework.Read/Netmon.Data.EntityFramework.Read.csproj", "/Shared/Netmon.Data.EntityFramework.Read/"]
COPY ["Shared/Netmon.Data.EntityFramework.Write/Netmon.Data.EntityFramework.Write.csproj", "/Shared/Netmon.Data.EntityFramework.Write/"]
COPY ["Shared/Netmon.Data.Services.Read/Netmon.Data.Services.Read.csproj", "/Shared/Netmon.Data.Services.Read/"]
COPY ["Shared/Netmon.Data.Services.Write/Netmon.Data.Services.Write.csproj", "/Shared/Netmon.Data.Services.Write/"]
COPY ["Shared/Netmon.Models/Netmon.Models.csproj", "/Shared/Netmon.Models/"]

# Restore dependencies for Netmon.DeviceManager
RUN dotnet restore "Netmon.DeviceManager.csproj"

# Copy the Shared project
RUN dotnet restore "/Shared/Netmon.Data/Netmon.Data.csproj"
RUN dotnet restore "/Shared/Netmon.Data.EntityFramework/Netmon.Data.EntityFramework.csproj"
RUN dotnet restore "/Shared/Netmon.Data.EntityFramework.Read/Netmon.Data.EntityFramework.Read.csproj"
RUN dotnet restore "/Shared/Netmon.Data.EntityFramework.Write/Netmon.Data.EntityFramework.Write.csproj"
RUN dotnet restore "/Shared/Netmon.Data.Services.Read/Netmon.Data.Services.Read.csproj"
RUN dotnet restore "/Shared/Netmon.Data.Services.Write/Netmon.Data.Services.Write.csproj"
RUN dotnet restore "/Shared/Netmon.Models/Netmon.Models.csproj"

# Copy the rest of the source code
COPY ["Services/Netmon.DeviceManager/", "./"]

COPY ["Shared/Netmon.Data/", "/Shared/Netmon.Data/"]
COPY ["Shared/Netmon.Data.EntityFramework/", "/Shared/Netmon.Data.EntityFramework/"]
COPY ["Shared/Netmon.Data.EntityFramework.Read/", "/Shared/Netmon.Data.EntityFramework.Read/"]
COPY ["Shared/Netmon.Data.EntityFramework.Write/", "/Shared/Netmon.Data.EntityFramework.Write/"]
COPY ["Shared/Netmon.Data.Services.Read/", "/Shared/Netmon.Data.Services.Read/"]
COPY ["Shared/Netmon.Data.Services.Write/", "/Shared/Netmon.Data.Services.Write/"]
COPY ["Shared/Netmon.Models/", "/Shared/Netmon.Models/"]

# Build Netmon.DeviceManager
WORKDIR "/src/"
RUN dotnet build "Netmon.DeviceManager.csproj" -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "Netmon.DeviceManager.csproj" -c Release -o /app/publish

# Stage 3: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# Copy the published files from the previous stage
COPY --from=publish /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "Netmon.DeviceManager.dll"]
