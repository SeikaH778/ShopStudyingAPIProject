
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ShopStudyingAPI/ShopStudyingAPI.csproj", "ShopStudyingAPI/"]
COPY ["ShopInfrastructure/ShopApplication.csproj", "ShopInfrastructure/"]
COPY ["ShopDomain/ShopDomain.csproj", "ShopDomain/"]
COPY ["ShopPersistance/ShopPersistance.csproj", "ShopPersistance/"]
RUN dotnet restore "ShopStudyingAPI/ShopStudyingAPI.csproj"
COPY . .
WORKDIR "/src/ShopStudyingAPI"
RUN dotnet publish "ShopStudyingAPI.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080
ENTRYPOINT ["dotnet", "ShopStudyingAPI.dll"]