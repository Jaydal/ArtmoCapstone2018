using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artmo.Core.Donor
{
    public class Organization : IDonor
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
