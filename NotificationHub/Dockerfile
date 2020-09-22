FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NotificationHub/NotificationHub.csproj", "NotificationHub/"]
RUN dotnet restore "NotificationHub/NotificationHub.csproj"
COPY . .
WORKDIR "/src/NotificationHub"
RUN dotnet build "NotificationHub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationHub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationHub.dll"]