FROM mcr.microsoft.com/dotnet/sdk:6.0.300-alpine3.15 AS build

WORKDIR /source/Hedgehog.DTOs
COPY ./Hedgehog.DTOs .

WORKDIR /source/Hedgehog.Persistence
COPY ./Hedgehog.Persistence/*.csproj .

RUN dotnet restore -r linux-musl-x64

COPY ./Hedgehog.Persistence/ .
RUN dotnet publish -c release -o /app -r linux-musl-x64 --no-restore --self-contained false

FROM mcr.microsoft.com/dotnet/aspnet:6.0.5-alpine3.15

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["./Hedgehog.Persistence"]
