namespace Artmo.Core.Donor
{
    public interface IDonor
    {
        Guid Guid { get; set; }
        string Name { get; set; }
    }
}