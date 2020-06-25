ARG BRANCH="main"
ARG COMMIT=""

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

COPY ["SPMS.Web/SPMS.Web.csproj", "SPMS.Web/"]
RUN dotnet restore "SPMS.Web/SPMS.Web.csproj"
COPY . .
WORKDIR "/src/SPMS.Web"
RUN npm install
RUN dotnet build "SPMS.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SPMS.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV COMMIT_SHA=${COMMIT}
ENV COMMIT_BRANCH=${BRANCH}
ENTRYPOINT ["dotnet", "SPMS.Web.dll"]
