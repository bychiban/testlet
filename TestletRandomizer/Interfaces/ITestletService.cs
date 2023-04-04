using TestletRandomizer.Models;

namespace TestletRandomizer.Interfaces
{
    public interface ITestletService
    {
        Testlet CreateTestlet(string testletId, List<Item> items);
    }
}
