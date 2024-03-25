namespace LMT01500Service
{
    public class LMT01500Utilities
    {
        public async IAsyncEnumerable<T> LMT01500GetListStream<T>(List<T> poParameter)
        {
            foreach (T item in poParameter)
            {
                yield return item;
            }
        }
    }
}
