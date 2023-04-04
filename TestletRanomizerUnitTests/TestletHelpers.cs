using TestletRandomizer.Models;

namespace TestletRanomizerUnitTests
{
    public static class TestletHelpers
    {
        public static List<Item> GenerateItemsByRule(int pretestItemsCount, int operationalItemsCount)
        {
            var items = new List<Item>();
            for (int i = 0; i < operationalItemsCount; i++)
            {
                items.Add(new Item($"operational{i}", ItemTypeEnum.Operational));
            }

            for (int i = 0; i < pretestItemsCount; i++)
            {
                items.Add(new Item($"pretest{i}", ItemTypeEnum.Pretest));
            }

            return items;
        }

        public static List<Item> ItemsWithDuplicates() 
        {
            return new List<Item> {
                new Item("1", ItemTypeEnum.Pretest),
                new Item("1", ItemTypeEnum.Pretest),
                new Item("3", ItemTypeEnum.Pretest),
                new Item("3", ItemTypeEnum.Pretest),
                new Item("1", ItemTypeEnum.Operational),
                new Item("2", ItemTypeEnum.Operational),
                new Item("3", ItemTypeEnum.Operational),
                new Item("4", ItemTypeEnum.Operational),
                new Item("5", ItemTypeEnum.Operational),
                new Item("5", ItemTypeEnum.Operational),
            };
        }

        public static List<Item> DefaultItems()
        {
            return new List<Item> {
                new Item("6", ItemTypeEnum.Operational),
                new Item("4", ItemTypeEnum.Operational),
                new Item("1", ItemTypeEnum.Pretest),
                new Item("2", ItemTypeEnum.Operational),
                new Item("2", ItemTypeEnum.Pretest),
                new Item("5", ItemTypeEnum.Operational),
                new Item("3", ItemTypeEnum.Operational),
                new Item("3", ItemTypeEnum.Pretest),
                new Item("1", ItemTypeEnum.Operational),
                new Item("4", ItemTypeEnum.Pretest),
            };
        }
    }
}
