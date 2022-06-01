FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApp/WebApp.csproj", "WebApp/"]
COPY ["App.DAL.EF/App.DAL.EF.csproj", "App.DAL.EF/"]
COPY ["App.Domain/App.Domain.csproj", "App.Domain/"]
COPY ["Base.Domain/Base.Domain.csproj", "Base.Domain/"]
COPY ["Base.Contracts.Domain/Base.Contracts.Domain.csproj", "Base.Contracts.Domain/"]
COPY ["App.Resources/App.Resources.csproj", "App.Resources/"]
COPY ["Base.DAL.EF/Base.DAL.EF.csproj", "Base.DAL.EF/"]
COPY ["Base.Contracts.DAL/Base.Contracts.DAL.csproj", "Base.Contracts.DAL/"]
COPY ["Base.Contracts.Base/Base.Contracts.Base.csproj", "Base.Contracts.Base/"]
COPY ["App.Contracts.DAL/App.Contracts.DAL.csproj", "App.Contracts.DAL/"]
COPY ["App.DAL.DTO/App.DAL.DTO.csproj", "App.DAL.DTO/"]
COPY ["Base.DAL/Base.DAL.csproj", "Base.DAL/"]
COPY ["Base.Resources/Base.Resources.csproj", "Base.Resources/"]
COPY ["Base.Extensions/Base.Extensions.csproj", "Base.Extensions/"]
COPY ["App.BLL.DTO/App.BLL.DTO.csproj", "App.BLL.DTO/"]
COPY ["App.BLL/App.BLL.csproj", "App.BLL/"]
COPY ["Base.BLL/Base.BLL.csproj", "Base.BLL/"]
COPY ["Base.Contracts.BLL/Base.Contracts.BLL.csproj", "Base.Contracts.BLL/"]
COPY ["App.Contracts.BLL/App.Contracts.BLL.csproj", "App.Contracts.BLL/"]
COPY ["App.Public.DTO/App.Public.DTO.csproj", "App.Public.DTO/"]
COPY ["Helpers.WebApp/Helpers.WebApp.csproj", "Helpers.WebApp/"]
RUN dotnet restore "WebApp/WebApp.csproj"
COPY . .
WORKDIR "/src/WebApp"
RUN dotnet build "WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.dll"]
