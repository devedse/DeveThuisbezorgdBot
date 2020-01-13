#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DeveThuisbezorgdBot.WebApp/DeveThuisbezorgdBot.WebApp.csproj", "DeveThuisbezorgdBot.WebApp/"]
COPY ["DeveThuisbezorgdBot.TelegramBot/DeveThuisbezorgdBot.TelegramBot.csproj", "DeveThuisbezorgdBot.TelegramBot/"]
COPY ["DeveThuisbezorgdBot/DeveThuisbezorgdBot.csproj", "DeveThuisbezorgdBot/"]
RUN dotnet restore "DeveThuisbezorgdBot.WebApp/DeveThuisbezorgdBot.WebApp.csproj"
COPY . .
WORKDIR "/src/DeveThuisbezorgdBot.WebApp"
RUN dotnet build "DeveThuisbezorgdBot.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeveThuisbezorgdBot.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeveThuisbezorgdBot.WebApp.dll"]