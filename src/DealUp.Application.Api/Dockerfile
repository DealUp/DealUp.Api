FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG TARGETARCH
ARG BUILD_CONFIGURATION=Release

WORKDIR /build
COPY ["src/DealUp.Application.Api/DealUp.Application.Api.csproj", "src/DealUp.Application.Api/"]
RUN dotnet restore "src/DealUp.Application.Api/DealUp.Application.Api.csproj" -a $TARGETARCH
COPY . .

WORKDIR "/build/src/DealUp.Application.Api"
RUN dotnet build "DealUp.Application.Api.csproj" -c $BUILD_CONFIGURATION -a $TARGETARCH -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH

RUN dotnet publish "DealUp.Application.Api.csproj" -c $BUILD_CONFIGURATION -a $TARGETARCH -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DealUp.Application.Api.dll"]
