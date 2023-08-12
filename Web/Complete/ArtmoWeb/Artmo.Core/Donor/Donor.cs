using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artmo.Core.Donor
{
    public class Donor : IDonor
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
