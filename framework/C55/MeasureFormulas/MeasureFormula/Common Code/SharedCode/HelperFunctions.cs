using System;
using System.Linq;
using CL.FormulaHelper.DTOs;

namespace MeasureFormula.SharedCode
{
    public static class CommonConstants
    {
        /// <summary>
        /// Typical conversion factors used widely throughout the formulae
        /// </summary>

        public const double DaysPerWeek = 7d;
        public const double DaysPerYear = 365d;
        public const double HoursPerDay = 24d;
        public const double HoursPerYear = DaysPerYear * HoursPerDay;
        public const double HoursPerWeek = DaysPerWeek * HoursPerDay;
        public const int MonthsPerYearInt = 12;
        public const double MonthsPerYear = MonthsPerYearInt;
        public const double SecondsPerHour = 3600d;
		public const double WeeksPerYear = 52.0d;
		public const double MinutesPerHour = 60.0;
		public const double MwToKwConversion = 1000.0d;		
	    public const double DaysPerMonth = 30d;
	    public const double HoursPerMonth = HoursPerDay * DaysPerMonth;

        public const double PercentPerProbabilityOne = 100d;
        
        public const double ExposureFactorFull = 1.0; // 100% exposure factor
        public const double DoubleDifferenceTolerance = 0.000001;

        public const int NoSubsampling = 0;

        public const double TonnesCO2PerKgSF6 = 22.8; //1kg Sf6 = 22.8 tonne CO2
    
    }

    public static class HelperFunctions
    {
        public static bool CurveIsEmpty(XYCurveDTO candidateCurve)
        {
            return candidateCurve == null || candidateCurve.Points == null || candidateCurve.Points.Length == 0;
        }
        
        public static bool IsRelativeTimeSeries(TimeSeriesDTO timeSeries)
        {
            return timeSeries.OffsetType == TimeSeriesDTO.TimeSeriesOffsetType.RelativeMonthly ||
                   timeSeries.OffsetType == TimeSeriesDTO.TimeSeriesOffsetType.RelativeYearly;
        }
        
        public static double GetMonthlyValue(TimeSeriesDTO timeSeries, int year, int monthOffset)
        {
            return IsRelativeTimeSeries(timeSeries)
                       ? timeSeries.GetMonthlyValue(monthOffset)
                       : timeSeries.GetMonthlyValue(year, monthOffset);
        }
                
        public static double GetMonthlyValue(double annualValue)
        {
            return annualValue / CommonConstants.MonthsPerYear;
        }
        
        /// <summary>
        /// If the string does not represent a valid double, return 0, else return the double.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        ///
        public static double ValidStringsToDoubleOthersToZero(string inputString)
        {
            double outVal;
            if (!double.TryParse(inputString, out outVal))
            {
                outVal = 0d;
            }

            return outVal;
        }
        
        /// <summary>
        /// Converts a vector containing annual probabilities to the corresponding monthly values.
        /// This is the correct formula to use when the probability is a true annual probability;
        /// it uses the power function rather than the simple "divide by 12" approach.
        /// </summary>
        public static double?[] GetMonthlyProbabilityValueFromAnnualProbability(double?[] annualProbabilities)
        {
            if (annualProbabilities == null) return null;

            var annualProbabilityOutOfRange = annualProbabilities.Where(x => x < 0 || x > 1).ToArray().Length;
            if (annualProbabilityOutOfRange > 0) return null;

            var result = annualProbabilities.Select(x => 1 - Math.Pow(1 - ( x ?? 0), 1 / CommonConstants.MonthsPerYear)).Cast<double?>().ToArray();

            return result;
        }

        /// <summary>
        /// Converts an annual frequency to the corresponding monthly value.
        /// This is the correct formula to use when the probability is a rate or frequency;
        /// it uses the simple "divide by 12" approach.
        /// </summary>
        public static double? GetMonthlyProbabilityValue(double? annualFrequency)
        {
            return  annualFrequency.HasValue ? (double?) annualFrequency.Value / CommonConstants.MonthsPerYear : null;
        }
        
        /// <summary>
        /// Converts a vector containing annual frequencies to the corresponding monthly values.
        /// This is the correct formula to use when the probability is a rate or frequency;
        /// it uses the simple "divide by 12" approach.
        /// </summary>
        [Obsolete("This function should be obsoleted moving forward. Please use GetMonthlyProbability instead -- the refactored version)")]
        public static double?[] GetMonthlyProbabilityValue(int months, double?[] annualFrequencies)
        {
            if (annualFrequencies == null) return null;

            var result = new double?[months];
            for (var i = 0; i < months; i++)
            {
                result[i] = GetMonthlyProbabilityValue(annualFrequencies[i]);
            }
            return result;
        }
        
        ///<summary>
        /// Converts a vector containing annual frequencies to the corresponding monthly values.
        /// This is the correct formula to use when the probability is a rate or frequency;
        /// it uses the simple "divide by 12" approach.
        ///<summary>
        public static double?[] GetMonthlyProbability(double?[] annualFrequencies)
        {
            if (annualFrequencies == null) return null;
            
            var result = annualFrequencies.Select(x => x / CommonConstants.MonthsPerYear).ToArray();
            return result;
        }
        
        ///<summary>
        /// Multiples the inputValue and factor to the first few number of months in the array
        ///<summary>
        public static double?[] ScaleValues(int months, double?[] inputValues, double? factor)
        {
            if (inputValues == null) return null;
            
            var result = new double?[months];

            for (var i = 0; i < months; i++)
            {
                result[i] = (factor ?? 0) * inputValues[i];
            }
            return result;
        }
        
        ///<summary>
        /// Multiples the factor to all of the cells of the inputValue array
        ///<summary>
        public static double?[] ScaleEntireArrayValues(double?[] inputValues, double? factor)
        {
            if (inputValues == null) return null;
            
            var result = inputValues.Select(x => x * (factor ?? 0)).ToArray();
            return result;
        }
        
        public static bool IsFirstReplacement(double?[] conditions, double bestCondition)
        {
            if (conditions == null) return true;
            
            return Array.FindAll(conditions, x => Equals(x, bestCondition)).Length == 0;
        }

        public static double? GetCustomFieldValue(CustomFieldListItemDTO customField)
        {
            if (customField == null) return null;

            return customField.Value;
        }
        
        public static int? GetCustomFieldValueAsInt(CustomFieldListItemDTO customField)
        {
            if (customField == null) return null;

            return customField.ValueAsInteger;
        }

        public static double?[] CalculateMeasureValue(int months, double?[] consequences, double?[] likelihoods)
        {
            if (consequences == null || likelihoods == null) return null;

            return (new double?[months]).Select((val, i) => consequences[i] * likelihoods[i]).ToArray();
        }
    }
}
