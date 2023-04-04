namespace TestletRandomizer.Models
{
    public class Testlet
    {
        public string TestletId { get; private set; }
        public List<Item> Items { get; private set; }

        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }
    }
}
