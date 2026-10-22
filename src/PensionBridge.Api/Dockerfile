FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["PensionBridge.Api/PensionBridge.Api.csproj", "PensionBridge.Api/"]
RUN dotnet restore "PensionBridge.Api/PensionBridge.Api.csproj"
COPY . .
WORKDIR "/src/PensionBridge.Api"
RUN dotnet build "PensionBridge.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PensionBridge.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PensionBridge.Api.dll"]
