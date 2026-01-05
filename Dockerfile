# Use official ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["LogiDriverPortal.csproj", "./"]
RUN dotnet restore "./YourProject.csproj"

# Copy the rest of the source code
COPY . .

# Build the app in Release mode
RUN dotnet publish "LogiDriverPortal.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Entry point
ENTRYPOINT ["dotnet", "LogiDriverPortal.dll"]
