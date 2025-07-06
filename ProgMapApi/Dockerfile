FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 441
EXPOSE 442

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProgMapApi.csproj", "./"]
RUN dotnet restore "ProgMapApi.csproj"
RUN dotnet add package Npgsql
RUN dotnet add package Npgsql.DataSource
COPY . .
WORKDIR "/src/"
RUN dotnet build "./ProgMapApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProgMapApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://+:440"]

