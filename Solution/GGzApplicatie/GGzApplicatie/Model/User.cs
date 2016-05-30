using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGzApplicatie.Model
{
    public class User
    {
        public int ClientCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Admin { get; set; }

        public User()
        {
            //Constructor
        }
    }
}
