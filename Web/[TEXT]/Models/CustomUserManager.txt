using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace ArtmoWeb.WebApplication.Models
{
    public class CustomUserManager : UserManager<ApplicationUser>
    {
        public CustomUserManager()
            : base(new CustomUserSore<ApplicationUser>()){}

        public override Task<ApplicationUser> FindAsync(string userName, string password)
        {
            var taskInvoke = Task<ApplicationUser>.Factory.StartNew(() =>
                {
                    if (IdentityXML.getIdentity(userName, password) > 0)
                    {
                        return new ApplicationUser { Id = "NeedsAnId", UserName = userName };
                    }
                    return null;
                });

            return taskInvoke;
        }
    }
    public class IdentityXML
    {
        public static int getIdentity(string u, string p)
        {
            string path = "~/_Storage/identity.xml";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(path)))
            {
                var xml = XDocument.Load(System.Web.Hosting.HostingEnvironment.MapPath(path));
                    return (from c in xml.Root.Descendants("Identity")
                            where c.Element("username").Value.Equals(Convert.ToBase64String(Encoding.UTF8.GetBytes(u))) && c.Element("password").Value.Equals(Convert.ToBase64String(Encoding.UTF8.GetBytes(p)))
                            select c).Count();
            }
            else
            {
                return 0;
            }
              
        }
    }
}