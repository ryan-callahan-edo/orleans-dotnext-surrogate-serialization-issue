# create image that will be used for the running the app
FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS base

RUN apt-get update \
  && apt-get -y install dnsutils build-essential unzip htop procps iputils-ping \
  && apt-get clean
RUN curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg

EXPOSE 43000

WORKDIR /app

COPY . .
# RUN dotnet restore
