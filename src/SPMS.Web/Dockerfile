FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \ 
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq

COPY ["src/SPMS.Web/SPMS.Web.csproj", "src/SPMS.Web/"]
COPY ["src/SPMS.WebShared/SPMS.WebShared.csproj", "src/SPMS.WebShared/"]
COPY ["src/SPMS.BackgroundService/SPMS.BackgroundService.csproj", "src/SPMS.BackgroundService/"]
COPY ["src/SPMS.Application/SPMS.Application.csproj", "src/SPMS.Application/"]
COPY ["src/SPMS.Common/SPMS.Common.csproj", "src/SPMS.Common/"]
COPY ["src/SPMS.ViewModel/SPMS.ViewModel.csproj", "src/SPMS.ViewModel/"]
COPY ["src/SPMS.Domain/SPMS.Domain.csproj", "src/SPMS.Domain/"]
COPY ["src/SPMS.Persistence.MSSQL/SPMS.Persistence.MSSQL.csproj", "src/SPMS.Persistence.MSSQL/"]
COPY ["src/SPMS.Persistence/SPMS.Persistence.csproj", "src/SPMS.Persistence/"]
COPY ["src/SPMS.Infrastructure/SPMS.Infrastructure.csproj", "src/SPMS.Infrastructure/"]
RUN dotnet restore "src/SPMS.Web/SPMS.Web.csproj"
COPY . .
WORKDIR "/src/src/SPMS.Web"
RUN npm install
RUN dotnet build "SPMS.Web.csproj" -c Release -o /app/build
RUN dotnet test

FROM build AS publish
RUN dotnet publish "SPMS.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SPMS.Web.dll"]
