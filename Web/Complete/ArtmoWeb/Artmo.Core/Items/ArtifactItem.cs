using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artmo.Core.Items
{
    public class ArtifactItem : IItem, IArtifact
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int TargetID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Preview { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public string Origin { get; set; }
        public string History { get; set; }

        public IItem CreateItem()
        {
            return new ArtifactItem();
        }
    }
}
