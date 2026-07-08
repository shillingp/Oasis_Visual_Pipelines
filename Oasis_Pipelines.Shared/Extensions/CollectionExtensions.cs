namespace Oasis_Pipelines.Shared.Extensions;

public static class CollectionExtensions
{
    extension<T>(ICollection<T> source)
    {
        public void RemoveRange(IEnumerable<T> items)
        {
            foreach (T item in items) 
                source.Remove(item);
        }

        public void RemoveRange(Func<T, bool> predicate)
        {
            foreach (T item in source.ToArray())
                if (predicate(item))
                    source.Remove(item);
        }
    }
}