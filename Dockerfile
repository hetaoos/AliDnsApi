FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app
ENV TZ=Asia/Shanghai

WORKDIR /src
COPY ["AliDnsUpdater/AliDnsUpdater.csproj", "AliDnsUpdater/"]
COPY ["AliDnsApi/AliDnsApi.csproj", "AliDnsApi/"]

RUN dotnet restore "AliDnsUpdater/AliDnsUpdater.csproj"
COPY . .
WORKDIR "/src/AliDnsUpdater"

FROM build AS publish
RUN dotnet publish "AliDnsUpdater.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS runtime
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AliDnsUpdater.dll"]