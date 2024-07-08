#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UPBankAPI.Address/UPBankAPI.Address.csproj", "UPBankAPI.Address/"]
RUN dotnet restore "./UPBankAPI.Address/UPBankAPI.Address.csproj"
COPY . .
WORKDIR "/src/UPBankAPI.Address"
RUN dotnet build "./UPBankAPI.Address.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UPBankAPI.Address.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UPBankAPI.Address.dll"]