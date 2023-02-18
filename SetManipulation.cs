using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    public static class SetManipulation
    {
        public static IEnumerable<IEnumerable<T>> GetPowerSet<T>(List<T> list)
        {
            return from m in Enumerable.Range(0, 1 << list.Count)
                select
                    from i in Enumerable.Range(0, list.Count)
                    where (m & (1 << i)) != 0
                    select list[i];
        }
        
        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> set, IEnumerable<T> subset = null)
        {
            if (subset == null) subset = new T[] { };
            if (!set.Any()) yield return subset;

            for (var i = 0; i < set.Count(); i++)
            {
                var newSubset = set.Take(i).Concat(set.Skip(i + 1));
                foreach (var permutation in GetPermutations(newSubset, subset.Concat(set.Skip(i).Take(1))))
                {
                    yield return permutation;
                }
            }
        }
        
    }
}