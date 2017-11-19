using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCoreDal.SqlServer
{
    public class TempDbContextFactory : IDesignTimeDbContextFactory<SqlServerDbContext>
    {        
        //public SqlServerDbContext Create(DbContextFactoryOptions options)
        //{            
        //    var builder = new DbContextOptionsBuilder<SqlServerDbContext>();
        //    //builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=bcoreapp;Trusted_Connection=True;MultipleActiveResultSets=true");
        //    builder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=bcoreapp;Integrated Security=True;MultipleActiveResultSets=True");

        //    return new SqlServerDbContext(builder.Options);
        //}

        public SqlServerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlServerDbContext>();
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=bcoreapp;Integrated Security=True;MultipleActiveResultSets=True");

            return new SqlServerDbContext(optionsBuilder.Options);
        }        
    }
}

