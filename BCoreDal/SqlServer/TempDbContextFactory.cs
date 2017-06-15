using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCoreDal.SqlServer
{
    public class TempDbContextFactory : IDbContextFactory<SqlServerDbContext>
    {        
        public SqlServerDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<SqlServerDbContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=bcoreapp;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new SqlServerDbContext(builder.Options);
        }
    }
}
