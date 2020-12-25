#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app
ENV TZ=Asia/Shanghai

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AliDnsUpdater/AliDnsUpdater.csproj", "AliDnsUpdater/"]
RUN dotnet restore "AliDnsUpdater/AliDnsUpdater.csproj"
COPY . .
WORKDIR "/src/AliDnsUpdater"
RUN dotnet build "AliDnsUpdater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AliDnsUpdater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AliDnsUpdater.dll"]