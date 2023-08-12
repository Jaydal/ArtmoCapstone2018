using Artmo.Core.Donor;

namespace Artmo.Core.Items
{
    public interface IDonated
    {
        DateTime DateDonated { get; set; }
        IDonor Donor { get; set; }
    }
}