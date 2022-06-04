FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.props .
COPY *.sln .



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

RUN dotnet restore

# copy everything else and build app


COPY ["WebApp/", "WebApp/"]
COPY ["App.DAL.EF/", "App.DAL.EF/"]
COPY ["App.Domain/", "App.Domain/"]
COPY ["Base.Domain/", "Base.Domain/"]
COPY ["Base.Contracts.Domain/", "Base.Contracts.Domain/"]
COPY ["App.Resources/", "App.Resources/"]
COPY ["Base.DAL.EF/", "Base.DAL.EF/"]
COPY ["Base.Contracts.DAL/", "Base.Contracts.DAL/"]
COPY ["Base.Contracts.Base/", "Base.Contracts.Base/"]
COPY ["App.Contracts.DAL/", "App.Contracts.DAL/"]
COPY ["App.DAL.DTO/", "App.DAL.DTO/"]
COPY ["Base.DAL/", "Base.DAL/"]
COPY ["Base.Resources/", "Base.Resources/"]
COPY ["Base.Extensions/", "Base.Extensions/"]
COPY ["App.BLL.DTO/", "App.BLL.DTO/"]
COPY ["App.BLL/", "App.BLL/"]
COPY ["Base.BLL/", "Base.BLL/"]
COPY ["Base.Contracts.BLL/", "Base.Contracts.BLL/"]
COPY ["App.Contracts.BLL/", "App.Contracts.BLL/"]
COPY ["App.Public.DTO/", "App.Public.DTO/"]

WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:latest AS runtime
WORKDIR /app
ENV ConnectionStrings:SqlServerConnection="Server=alpha.akaver.com,1533;User Id=student;Password=Student.Bad.password.0;Database=akaver_sportmap_123;MultipleActiveResultSets=true"
# my ConnectionStrings= "Host=localhost;Port=5432;Username=postgres;Password=postgres;database=CoWork"
# "Server=alpha.cowork.com,5432;User Id=student;Password=Student.Bad.password.0;Database=CoWorkCafe;MultipleActiveResultSets=true"
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]