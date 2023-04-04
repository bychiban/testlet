namespace TestletRandomizer.Extensions
{
    public static class Shuffler
    {
        private static readonly Random Random = new Random();

        public static List<T> Shuffle<T> (this List<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException (nameof(items));
            }

            for (var i = 0; i < items.Count() - 1; i++)
            {
                int pos = Random.Next(i, items.Count);
                T temp = items[i];
                items[i] = items[pos];
                items[pos] = temp;
            }
            return items;
        }
    }
}
