using TestletRandomizer.Constants;
using TestletRandomizer.Extensions;
using TestletRandomizer.Interfaces;
using TestletRandomizer.Models;

namespace TestletRandomizer.Services
{
    public class TestletService : ITestletService
    {
        public TestletService()
        {
        }

        public Testlet CreateTestlet(string testletId, List<Item> items)
        {
            ValidateInput(testletId, items);
            return new Testlet(testletId, Randomize(items));
        }

        private void ValidateInput(string testletId, List<Item> items)
        {
            if (testletId == null)
            {
                throw new ArgumentNullException(nameof(testletId));
            }

            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (items.Count(t => t.ItemType == ItemTypeEnum.Pretest) != TesletConstants.PretestItemsCount)
            {
                throw new ArgumentException($"Pretest items count not equal to {TesletConstants.PretestItemsCount}");
            }

            if (items.Count(t => t.ItemType == ItemTypeEnum.Operational) != TesletConstants.OperationalItemsCount)
            {
                throw new ArgumentException($"Operational items count not equal to {TesletConstants.OperationalItemsCount}");
            }
        }

        /// <summary>
        /// Shuffle all items and take first pretest part,
        /// shuffle leftover items again, to make remaining Pretest items uniformly distributed,
        /// otherwise probability to meet Pretest item at the end of the Testlet is higher than in the beginning
        /// <see cref={TestletPretestItemsUniformDistributionTest}>
        /// </summary>
        private List<Item> Randomize(List<Item> items)
        {
            var shuffledItems = items.Shuffle();
            var firstPart = shuffledItems.Where(t => t.ItemType == ItemTypeEnum.Pretest).Take(TesletConstants.PretestItemsToBeFirst);
            var leftoverPart = shuffledItems.Where(t => !firstPart.Contains(t)).ToList().Shuffle();

            return firstPart.Concat(leftoverPart).ToList();
        }
    }
}
