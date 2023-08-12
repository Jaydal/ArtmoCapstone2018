using Artmo.Core.Donor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artmo.Core.Items
{
    public class Donated : IDonated
    {
        public IDonor Donor { get; set; }
        public DateTime DateDonated { get; set; }
    }
}
