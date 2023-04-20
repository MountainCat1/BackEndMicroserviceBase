FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AccountApp/AccountApp.csproj", "AccountApp/"]
RUN dotnet restore "AccountApp/AccountApp.csproj"
COPY . .
WORKDIR "/src/AccountApp"
RUN dotnet build "AccountApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountApp.dll"]
