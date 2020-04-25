FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet publish Engenharia.Application -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/Engenharia.Application/out .

ENTRYPOINT ["dotnet", "Engenharia.Application.dll"]