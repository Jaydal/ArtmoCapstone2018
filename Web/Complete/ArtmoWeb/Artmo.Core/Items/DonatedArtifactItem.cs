using Artmo.Core.Donor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artmo.Core.Items
{
    public class DonatedArtifactItem : IItem, IDonated, IArtifact
    {
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public string Description { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public byte[] Preview { get; set; }
        public int TargetID { get; set; }
        public DateTime DateDonated { get; set; }
        public IDonor Donor { get; set; }
        public string History { get; set; }
        public string Origin { get; set; }

        public IItem CreateItem()
        {
            return new DonatedArtifactItem();
        }
    }
}
