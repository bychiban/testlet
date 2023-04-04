namespace TestletRandomizer.Models
{
    public class Item
    {
        public string ItemId { get; private set; }
        
        public ItemTypeEnum ItemType { get; private set; }

        public Item(string itemId, ItemTypeEnum itemType) 
        {
            ItemId = itemId;
            ItemType = itemType;
        }
    }
}