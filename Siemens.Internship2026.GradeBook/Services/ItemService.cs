namespace Siemens.Internship2026.GradeBook.Services
{
    public class ItemService: IItemServices
    {
        public IEnumerable<Item> GetTopPassingActiveItems(IEnumerable<Item> items, int n)
        {
            return items
                .Where(i => i.IsValue >= 5 && i.IsActive) 
                .Take(n);
        }
    }
}
