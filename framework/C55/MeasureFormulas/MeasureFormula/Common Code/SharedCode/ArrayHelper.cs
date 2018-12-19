using System.Linq;

namespace MeasureFormula.SharedCode
{
    public static class ArrayHelper
    {
    /// <summary>
        /// Returns an array that is the sum of the entries of the input arrays.
        /// Null entries are treated as zero (Since that's what Enumerable.Sum does.
        /// </summary>
        public static double?[] SumArrays(double?[][] arraysToSum)
        {
            var summedArrayLength = arraysToSum[0].Length;
            var haveVariableLengths = arraysToSum.Count(x => x.Length != summedArrayLength) > 0;
            if (haveVariableLengths) return null;
            
            var summedArrays = new double?[summedArrayLength];
            for (var resultIndex = 0; resultIndex < summedArrays.Length; resultIndex++)
            {
                summedArrays[resultIndex] = arraysToSum.Select(innerArray => innerArray[resultIndex]).Sum();
            }

            return summedArrays;
        }
        
        /// <summary>
        /// Returns an array that is the product of the entries of the two input arrays.
        /// If an entry is null, the corresponding entry is the product is null.
        /// </summary>
        public static double?[] MultiplyArrays(double?[] firstFactors, double?[] secondFactors)
        {
            if (firstFactors == null || secondFactors == null || firstFactors.Length != secondFactors.Length) return null;

            var productArray = firstFactors.Select((x, i) => x * secondFactors[i] ).ToArray();
			
            return productArray;
        }
        
        public static double?[] MultiplyArrayByTimeSeries(double?[] inputArray,  CL.FormulaHelper.DTOs.TimeSeriesDTO timeSeries, int startFiscalYear) {
            if (inputArray == null || timeSeries == null) {
                return null;
            }
            var result = new double?[inputArray.Length];
            for (int offset = 0; offset < inputArray.Length; offset++) {
                if (inputArray[offset] == null) {
					
                } else {
                    result[offset] = inputArray[offset] * timeSeries.GetMonthlyValue(startFiscalYear, offset);
                }
            }
            return result;
        }
        
        public static double?[] MultiplyStreamOfValuesByConstant(double?[] array, double? constant) {
            if (array == null || constant == null) {
                return null;
            }
            return array.Select(x => x * constant).ToArray();
        }
     

    }
}
