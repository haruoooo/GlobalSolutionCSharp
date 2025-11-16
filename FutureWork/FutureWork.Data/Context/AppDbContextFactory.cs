using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FutureWork.Data.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseOracle("User Id=rm97663;Password=180104;Data Source=oracle.fiap.com.br:1521/orcl;");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
