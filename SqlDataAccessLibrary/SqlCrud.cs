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
    }
}
