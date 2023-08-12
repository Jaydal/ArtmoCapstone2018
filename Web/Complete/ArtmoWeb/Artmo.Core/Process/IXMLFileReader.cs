using Artmo.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artmo.Core.Process
{
    public interface IXMLFileReader

    {
        IItem GetItem(Guid guid);

        IItem GetItem(IItem item);

    }
}
