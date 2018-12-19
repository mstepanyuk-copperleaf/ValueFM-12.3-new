using System;
using System.Linq;

namespace MeasureFormula.SharedCode
{
    /// <summary>
    /// This class provides a way to find which band a value lies in and return a corresponding value.
    /// Each value in the ThresholdValues will be treated as the upper bound of the range and ThresholdValues.
    /// If the final value is not double.MaxValue, then any inputValues that are greater than it will
    /// be assigned the DefaultValue
    /// </summary>
    public class ThresholdRangeToValue
    {
        public enum RightBoundaryType
        {
            RightBoundaryOpen,
            RightBoundaryClosed
        }
        private readonly double[] ThresholdValues;
        private readonly double[] OutputValues;
        private readonly double InputOutOfRangeOutputValue;
        private readonly RightBoundaryType BoundaryType;


        public ThresholdRangeToValue(double[] thresholdValues, double[] outputValues, RightBoundaryType boundaryType, double inputOutOfRangeOutputValue)
        {
            if(thresholdValues.Length != outputValues.Length) throw new ArgumentException();

            var notAscending = thresholdValues.Where((x, i) => i > 0 && x <= thresholdValues[i-1]).Any();
            if(notAscending) throw new ArgumentException();

            ThresholdValues = thresholdValues;
            OutputValues = outputValues;
            InputOutOfRangeOutputValue = inputOutOfRangeOutputValue;
            BoundaryType = boundaryType;
        }
        
        public double ValueAt(double? inputValue)
        {
            if (inputValue == null) return InputOutOfRangeOutputValue;

            Predicate<double> rightClosed = x => inputValue.Value <= x;
            Predicate<double> rightOpen =  x => inputValue.Value < x;
            var thresholdCrossed = BoundaryType == RightBoundaryType.RightBoundaryOpen ? rightOpen : rightClosed;
            
            var rangeIndex = Array.FindIndex(ThresholdValues, thresholdCrossed);
            return rangeIndex < 0 ? InputOutOfRangeOutputValue : OutputValues[rangeIndex];
        }
    }
}
