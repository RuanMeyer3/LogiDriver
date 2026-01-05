# -----------------------------
# Stage 1: Build
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the project and publish
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# -----------------------------
# Stage 2: Runtime
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# Copy published app from build stage
COPY --from=build /app/publish ./

# Expose port
EXPOSE 5000

# Entry point
ENTRYPOINT ["dotnet", "LogiDriverPortal.dll"]
