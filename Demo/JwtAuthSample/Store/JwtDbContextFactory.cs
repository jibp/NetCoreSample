using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthSample.Store
{
    public class JwtDbContextFactory : IDesignTimeDbContextFactory<JwtDbContext>
    {
        public JwtDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JwtDbContext>();
             optionsBuilder.UseSqlServer("server=.;database=myDb;trusted_connection=true;");
            //optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=WebBloggingDB; User=root;Password=;");
            return new JwtDbContext(optionsBuilder.Options);
        }
    }
}
