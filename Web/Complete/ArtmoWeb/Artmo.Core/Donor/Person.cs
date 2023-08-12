using Artmo.Core.Donor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artmo.Core.Person
{
    public class Person: IDonor
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        
        public Person()
        {
            this.Name =$"{FirstName} {LastName}";
        }
    }
}
