using System.Collections.Generic;
using System.Linq;

namespace Resources.Scripts.Utils
{
    public static class Combinations
    {
        public static T[][] GenerateCombinations<T>(int countElements, T[] arrayElements)
        {
            int[] indexCombinations = new int[countElements + 2];
            List<List<T>> combinations = new List<List<T>>();

            for (int i = 0; i < countElements; i++)
            {
                indexCombinations[i] = i;
            }

            indexCombinations[countElements] = arrayElements.Length;
            indexCombinations[countElements + 1] = 0;
            int j = 0;
            while (j < countElements)
            {
                j = 0;
                AddCombination(indexCombinations, combinations, arrayElements, 0, countElements);
                while (indexCombinations[j] + 1 == indexCombinations[j + 1])
                {
                    indexCombinations[j] = j;
                    j += 1;
                }

                indexCombinations[j] += 1;
            }

            T[][] result = combinations.Select(combination => combination.ToArray()).ToArray();
            
            return result;
        }

        private static void AddCombination<T>(int[] indexCombinations, List<List<T>> combinations, T[] arrayElements, int from, int to)
        {
            List<T> newCombination = new List<T>();
            for (int i = from; i < to; i++)
            {
                newCombination.Add(arrayElements[indexCombinations[i]]);
            }
            combinations.Add(newCombination);
        }
    }
}
