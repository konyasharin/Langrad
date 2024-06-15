using System;
using System.Collections.Generic;

namespace Resources.Scripts.Utils
{
    public static class Random
    {
        public static T GetByWeights<T>(List<T> array, Dictionary<int, int> weights)
        {
            int weightsSum = 0;
            
            foreach (var key in weights.Keys)
            {
                weightsSum += weights[key];
            }

            int randomNumber = UnityEngine.Random.Range(0, weightsSum);
            int sumSkippedWeights = 0;
            foreach (var key in weights.Keys)
            {
                if (randomNumber <= weights[key] + sumSkippedWeights)
                {
                    return array[key];
                }

                sumSkippedWeights += weights[key];
            }

            throw new Exception("Element didn't get by weights");
        }
    }
}
