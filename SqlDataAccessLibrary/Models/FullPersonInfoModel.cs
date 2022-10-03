using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDataAccessLibrary.Models
{
    public class FullPersonInfoModel
    {
        public PersonModel BasicInfo { get; set; }
        public List<AddressModel> Addresses { get; set; } = new List<AddressModel>();
        public List<EmployerModel> Employers { get; set; } = new List<EmployerModel>();
    }
}
