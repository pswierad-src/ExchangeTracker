FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ExchangApi/ExchangApi.csproj", "ExchangApi/"]
RUN dotnet restore "ExchangApi/ExchangApi.csproj"
COPY . .
WORKDIR "/src/ExchangApi"
RUN dotnet build "ExchangApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExchangApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExchangApi.dll"]
