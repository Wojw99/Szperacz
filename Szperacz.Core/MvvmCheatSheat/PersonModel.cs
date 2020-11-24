using System;
using System.Collections.Generic;
using System.Text;

namespace Szperacz.Core.Models
{
    public class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PersonModel(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }
    }
}
