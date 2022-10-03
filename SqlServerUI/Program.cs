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

            /*
            var allPeople = GetAllPeople(sql);

            var hiredPeople = GetPeopleByEmployerName(sql, "Dardun S");

            var employers = GetPersonsEmployers(sql, "Dunja", "Milosavljevic");
            */

            FullPersonInfoModel mihic = new FullPersonInfoModel {
                BasicInfo = new PersonModel { FirstName = "Milan", LastName = "Mihic" }
                };

            mihic.Addresses.Add(new AddressModel
            {
                Street = "Bezanijska 3",
                City = "Novi Beograd",
                Country = "Serbia",
                ZipCode = "11000"
            });

            mihic.Employers.Add(new EmployerModel
            {
                CompanyName = "Strabag d.o.o."
            });
            

            CreatePerson(sql, mihic);

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

        private static List<PersonModel> GetPeopleByEmployerName(SqlCrud sql, string employerName)
        {
            return sql.ReadContactsByEmployer(employerName);
        }

        private static List<EmployerModel> GetPersonsEmployers(SqlCrud sql, string firstName, string lastName)
        {
            return sql.ReadEmployersByFullName(firstName, lastName);
        }

        private static void CreatePerson(SqlCrud sql, FullPersonInfoModel person)
        {
            sql.CreateContact(person);
        }
    
    }
}
