using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCoreDal.SqlServer
{
    public class TempDbContextFactory : IDbContextFactory<Context>
    {        
        public Context Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=bcoreapp;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new Context(builder.Options);
        }
    }
}
