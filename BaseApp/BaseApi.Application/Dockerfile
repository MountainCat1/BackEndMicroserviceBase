FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BaseApiApp/BaseApiApp.csproj", "BaseApiApp/"]
RUN dotnet restore "AccountApp/AccountApp.csproj"
COPY . .
WORKDIR "/src/BaseApiApp"
RUN dotnet build "BaseApiApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BaseApiApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountApp.dll"]
