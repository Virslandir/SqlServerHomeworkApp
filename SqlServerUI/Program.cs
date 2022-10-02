using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using SqlDataAccessLibrary.Models;
using SqlDataAccessLibrary;

namespace SqlServerUI
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlCrud sql = new SqlCrud(GetConnectionString());

            var people = GetAllPeople(sql);

            Console.ReadLine(); 
        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");

            var config = builder.Build();

            var connection = config.GetConnectionString(connectionStringName);

            return connection;
        }
    
        private static List<PersonModel> GetAllPeople(SqlCrud sql)
        {
            return sql.ReadAllPeople();
            
        }
    
    }
}
