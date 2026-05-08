namespace Siemens.Internship2026.GradeBook.Interfaces
{
    public interface IItemService
    {
        IEnumerable<Item> GetTopPassingActiveItems(IEnumerable<Item> items, int n);
    }
}
