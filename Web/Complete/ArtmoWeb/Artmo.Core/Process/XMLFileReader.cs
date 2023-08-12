using Artmo.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artmo.Core.Process
{
    public class XMLFileReader
    {
        IItem _item;

        public XMLFileReader(IItem item)
        {
            _item= item; 
        }

        public IItem GetItem(Guid guid) {

            _item.Name = "TestName";

            return _item;
        }
    }
}
