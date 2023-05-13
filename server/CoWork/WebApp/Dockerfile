FROM mcr.microsoft.com/dotnet/aspnet:latest AS runtime
WORKDIR /app
ENV ConnectionStrings:SqlServerConnection="Server=alpha.akaver.com,1533;User Id=student;Password=Student.Bad.password.0;Database=akaver_sportmap_123;MultipleActiveResultSets=true"
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]