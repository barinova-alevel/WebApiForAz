# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Copy everything from the build context (Solution Root) to /src inside the container
# This includes the WebApiForAz folder, BL folder, DAL folder, etc.
COPY . .

# 2. Run restore on the specific entry project 
# This path is relative to the current WORKDIR (/src) and must include the subfolder name.
RUN dotnet restore "WebApiForAz/WebApiForAz.csproj"

# 3. Publish the application (This step builds ALL referenced projects successfully)
RUN dotnet publish "WebApiForAz/WebApiForAz.csproj" -c Release -o /app/publish /p:UseAppHost=false

# -------------------------------------------------------------------

# Stage 2: Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Ensure app listens on port 80
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Copy published output
COPY --from=build /app/publish .

# Set the entrypoint to your app DLL
ENTRYPOINT ["dotnet", "WebApiForAz.dll"]