using SqlDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDataAccessLibrary
{
    public class SqlCrud
    {
        private readonly string _connectionString;

        public SqlDataAccess Db = new SqlDataAccess();

        public SqlCrud(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<PersonModel> ReadAllPeople()
        {
            string sql = @"select *
                           from dbo.People";

            List<PersonModel> rows = Db.LoadData<PersonModel, dynamic>(sql, new { }, _connectionString);

            return rows;

        }

        public List<PersonModel> ReadContactsByEmployer(string employerName)
        {
            string sql = @"select p.Id, p.FirstName, p.LastName
                           from (
                           (dbo.People p inner join dbo.PeopleEmployers pe on p.Id = pe.PersonId)
                           inner join dbo.Employers e on e.Id = pe.EmployerId)
                           where e.CompanyName = @CompanyName";

            List<PersonModel> rows = Db.LoadData<PersonModel, dynamic>( sql, new { CompanyName = employerName}, _connectionString);

            return rows;
        }

        public List<EmployerModel> ReadEmployersByFullName(string FirstName, string LastName)
        {
            string sql = @"select e.CompanyName
                            from ((dbo.Employers e inner join dbo.PeopleEmployers pe on e.Id = pe.EmployerId)
                            inner join dbo.People p on pe.PersonId = p.Id)
                            where p.FirstName = @FirstName and p.LastName = @LastName";

            List<EmployerModel> rows = Db.LoadData<EmployerModel, dynamic>(sql, new {FirstName, LastName }, _connectionString);

            return rows;
        }

        public void CreateContact(FullPersonInfoModel person)
        {
            // Question: SHouldnt I check if the name and lastname already exist??
            string sql = @"insert into dbo.People(FirstName, LastName) values(@FirstName, @LastName)";

            Db.SaveData(sql, new { FirstName = person.BasicInfo.FirstName, LastName = person.BasicInfo.LastName}, _connectionString);

            sql = @"select p.Id
                    from dbo.People p
                    where p.FirstName = @FirstName and p.LastName = @LastName";

            int personId = Db.LoadData<LookupIdModel, dynamic>(sql, new { FirstName = person.BasicInfo.FirstName, LastName = person.BasicInfo.LastName }, _connectionString).First().Id;

            foreach (var address in person.Addresses)
            {
                if (address.Id == 0)
                {
                    sql = @"insert into dbo.Addresses(Street, City, Country, ZipCode) values(@Street, @City, @Country, @ZipCode)";

                    Db.SaveData(sql, new { address.Street, address.City, address.Country, address.ZipCode }, _connectionString);
                }

                sql = @"select a.Id
                    from dbo.Addresses a
                    where a.Street = @Street and a.City = @City and a.Country = @Country and a.ZipCOde = @ZipCode";

                int addressId = Db.LoadData<LookupIdModel, dynamic>(sql, new { address.Street, address.City, address.Country, address.ZipCode }, _connectionString).First().Id;

                sql = @"insert into dbo.PeopleAddresses(PersonId, AddressId) values( @PersonId, @AddressId);";

                Db.SaveData(sql, new { PersonId = personId, AddressId = addressId }, _connectionString);
            }
        }
    }
}
