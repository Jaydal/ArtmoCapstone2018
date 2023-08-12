using Artmo.Core;
using Artmo.Core.Items;
using Artmo.Core.Process;

namespace Artmo.Core.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public void Test1()
        {
            IItem item = new ArtifactItem();

            XMLFileReader xMLFileReader = new XMLFileReader(item.CreateItem());
            string name=xMLFileReader.GetItem(Guid.NewGuid()).Name;
            Console.WriteLine(name);
            Assert.Pass();
        }
    }
}