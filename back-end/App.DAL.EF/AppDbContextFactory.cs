using  Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace App.DAL.EF;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Server=test-hw.postgres.database.azure.com;Database=test-hw;Port=5432;User Id=hwtest;Password=Kala.Maja1604;Ssl Mode=Require;");

        return new AppDbContext(optionsBuilder.Options);
    }
}