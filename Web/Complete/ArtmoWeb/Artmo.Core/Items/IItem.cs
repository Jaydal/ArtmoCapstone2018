namespace Artmo.Core.Items
{
    public interface IItem
    {
        DateTime DateAdded { get; set; }
        DateTime DateModified { get; set; }
        string Description { get; set; }
        Guid Guid { get; set; }
        string Name { get; set; }
        byte[] Preview { get; set; }
        int TargetID { get; set; }

        public abstract IItem CreateItem();
    }
}