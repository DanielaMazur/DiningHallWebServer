FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

# copy csproj and restore as distinct layers
COPY DiningHallServer.sln DiningHallServer.sln
COPY ./DiningHallServer/DiningHallServer.csproj ./DiningHallServer/DiningHallServer.csproj
RUN dotnet restore

# copy everything else and build app
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY --from=build-env /out .
EXPOSE 3000
ENTRYPOINT ["dotnet", "DiningHallServer.dll"]