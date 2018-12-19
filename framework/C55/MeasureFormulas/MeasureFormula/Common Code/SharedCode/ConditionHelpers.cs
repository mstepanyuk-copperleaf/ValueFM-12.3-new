using System;
using System.Linq;
using CL.FormulaHelper;
using CL.FormulaHelper.DTOs;

namespace MeasureFormula.SharedCode
{
    public class ConditionScoreAnswerDto
    {
        public DateTime Date;
        public double ConditionScore;
    }
    
    public class ConditionHelpers
    {
        /// <summary>
        /// // Construct a generic end of life condition decay curve, based on the design life entered by the user.
        /// </summary>
        public static XYCurveDTO ConstructConditionDecayCurve(double estimatedDesignLifeInYears)
        {
            var conditionDecayCurve = new XYCurveDTO();
            var points = new CurvePointDTO[3];

            // - - - - - - - - - - -> When brand new, the asset is in perfect condition
            points[0] = new CurvePointDTO
            {
                X = 0d,
                Y = 10.0
            };
            var conditionAtEndOfDesignLife = 3d;
            // - - - -> When at its design life, the asset has a condition of 3
            points[1] = new CurvePointDTO
            {
                X = estimatedDesignLifeInYears,
                Y = conditionAtEndOfDesignLife
            };

            // -> When past its design life (assuming linear degradation of the same slope), Design Life/0.7 corresponds to a condition of 0
            points[2] = new CurvePointDTO
            {
                X = estimatedDesignLifeInYears / 0.7,
                Y = 0.0
            };

            conditionDecayCurve.Points = points;

            return conditionDecayCurve;
        }
        
        /// <summary>
        /// This wrapper is deprecated.  It's only there for the convenience of porting old code over to the better implementation.  New users should call
        /// the underlying functions directly.
        /// </summary>
       
        public static double?[] ComputeConditionVectorFromOffset(int startFiscalYear,
                                                                 int months,
                                                                 int? conditionEntryOffsetMonths,
                                                                 int startOffsetMonth,
                                                                 XYCurveDTO conditionCurve,
                                                                 double offsetCondition,
                                                                 bool hasOffset,
                                                                 bool isBaseline)
        {
            if (hasOffset) return ComputeConditionsFromInServiceDate(months, startOffsetMonth, conditionCurve);

            if (isBaseline)
                return ComputeConditionsfromOffsetBaseline(startFiscalYear, months, conditionEntryOffsetMonths, startOffsetMonth, conditionCurve,
                                                           offsetCondition);

            return ComputeConditionsfromOffsetOutcome(startFiscalYear, months, startOffsetMonth, conditionCurve, offsetCondition);
        }

        /// <summary>
        ///This function computes the conditions corresponding to the condition curve derived value based on the age of the asset in either baseline or outcome case
        /// </summary>
        ///<param name="inServiceDateOffsetMonth">represents the number of months difference between the start year and the inservice date of the asset.
        /// positive when set to end of spend in outcome case.</param>
        public static double?[] ComputeConditionsFromInServiceDate(
            int months,
            int inServiceDateOffsetMonth,
            XYCurveDTO ageInYearsToConditionCurve
        )
        {
            if (HelperFunctions.CurveIsEmpty(ageInYearsToConditionCurve)) return null;
            
            var monthlyConditions = Enumerable.Repeat((double?)null, months).ToArray();

            var firstMonthIndexOnOrAfterInServiceDate = Math.Max(0, inServiceDateOffsetMonth);
            for (var monthIndex = firstMonthIndexOnOrAfterInServiceDate; monthIndex < months; monthIndex++)
            {
                var monthsSinceInServiceDate = monthIndex - inServiceDateOffsetMonth;
                monthlyConditions[monthIndex] = ageInYearsToConditionCurve.YFromX(monthsSinceInServiceDate / CommonConstants.MonthsPerYear);
            }

            return monthlyConditions;
        }

        /// <summary>
        ///This function computes the conditions corresponding to the condition curve derived value based either on the age of the asset or a user specific condition override.
        /// </summary>
        ///<param name="conditionOverrideOffset">Negative if you measured the condition at some time in the past.  Positive when you know the condition when the investment is complete, month 0 is investment start </param>
        ///<param name="inServiceDateOffset">represents the number of months difference between the start year and the inservice date of the asset</param>
        ///<param name="overrideCondition">Condition at either conditionOverrideOffset or 0 if that is null </param>
        public static double?[] ComputeConditionsfromOffsetBaseline(
            int startFiscalYear,
            int months,
            int? conditionOverrideOffset,
            int inServiceDateOffset,
            XYCurveDTO ageInYearsToConditionCurve,
            double overrideCondition)
        {
            var monthlyConditionsFromInServiceDate = ComputeConditionsFromInServiceDate(months, inServiceDateOffset, ageInYearsToConditionCurve);

            monthlyConditionsFromInServiceDate = ReplaceNullWithSubsequentAdjacentValue(monthlyConditionsFromInServiceDate);

            var overrideOffsetMonth = ComputeOverrideOffsetMonth(startFiscalYear, conditionOverrideOffset);
            var overriddenConditions = ApplyConditionOverride(overrideOffsetMonth, ageInYearsToConditionCurve, overrideCondition, monthlyConditionsFromInServiceDate);

            return overriddenConditions;
        }

        /// <summary>
        ///This function computes the conditions corresponding to the condition curve derived value based either on the age of the asset or a user specific condition override.
        /// </summary>
        ///<param name="impactDateOffset">represents the number of months difference between the start year and theend of spend. </param>
        ///<param name="overrideCondition">Condition at either conditionOverrideOffset or 0 if that is null </param>
        public static double?[] ComputeConditionsfromOffsetOutcome(
            int startFiscalYear,
            int months,
            int impactDateOffset,
            XYCurveDTO ageInYearsToConditionCurve,
            double overrideCondition)
        {
            var monthlyConditionsFromInServiceDate = ComputeConditionsFromInServiceDate(months, impactDateOffset, ageInYearsToConditionCurve);
            
            var overrideOffset = impactDateOffset;
            var overriddenConditions = ApplyConditionOverride(overrideOffset, ageInYearsToConditionCurve, overrideCondition, monthlyConditionsFromInServiceDate);

            return overriddenConditions;
        }

        public static double?[] ApplyConditionOverride(
            int overrideOffsetMonth,
            XYCurveDTO ageInYearsToConditionCurve,
            double overrideCondition,
            double?[] monthlyConditions
        )
        {
            if (HelperFunctions.CurveIsEmpty(ageInYearsToConditionCurve)) return null;
            
            var overriddenConditions = (double?[])monthlyConditions.Clone();
            var effectiveAgeInYears = ageInYearsToConditionCurve.XFromY(overrideCondition);

            var startIndex = Math.Max(0, overrideOffsetMonth);
            for (var index = startIndex; index < overriddenConditions.Length; index++)
            {
                var yearsSinceOverride = (index - overrideOffsetMonth) / CommonConstants.MonthsPerYear;
                overriddenConditions[index] = ageInYearsToConditionCurve.YFromX(effectiveAgeInYears + yearsSinceOverride);
            }

            return overriddenConditions;
        }

        public static double?[] ReplaceNullWithSubsequentAdjacentValue(
            double?[] monthlyConditions
        )
        {
            var nullReplacedConditions = (double?[])monthlyConditions.Clone();
            var indexLastNullValue = Array.LastIndexOf(nullReplacedConditions, null);

            var allValuesAreNull = indexLastNullValue == nullReplacedConditions.Length - 1;
            if (allValuesAreNull) return nullReplacedConditions;

            while (indexLastNullValue >= 0)
            { //work backwards through the indices into overrideVector[i] and propagate first non-null value
                nullReplacedConditions[indexLastNullValue] = nullReplacedConditions[indexLastNullValue + 1];
                indexLastNullValue--;
            }

            return nullReplacedConditions;
        }

        private static int ComputeOverrideOffsetMonth(
            int startFiscalYear,
            int? conditionOverrideOffset)
        {
            var currentDateOffsetFromFiscalYearStart = FormulaBase.ConvertDateTimeToOffset(DateTime.Now, startFiscalYear);
            return conditionOverrideOffset ?? currentDateOffsetFromFiscalYearStart;
        }



        //Taken from Ameren on Oct 30, 2017.  The following function returns the condition that corresponds to the equivalent operating hours in the vector eohInput.  Since the vector of EOH in eohInput
        //may not add up to the number of hours in a month (or may indeed exceed the number of hours in a month) this function has to calculate the condition for the
        //fiscal (or calendar month) that should be returned.
        public static double?[] ComputeConditionFromEoh(double?[] eohInput, XYCurveDTO conditionCurve, int startFiscalYear, int months)
        {
            var conditionVector = new double?[months];
            var condIndex = 0;          //Index into conditionm vector (assumes that the X,Y points are ordered in ascending age!

            if (conditionCurve == null || conditionCurve.Points.Length < 1)
            {
                return null;
            }

            for (var i = 0; i < months; i++)
            {
                if (eohInput[i].HasValue)
                {
                    var hourTotal = eohInput[i].Value; //Total hours accumulated from EOH vector
                    //Check that the hours accumulated are less than or equal to the next point in the condition curve.  If not increment the index into the
                    //condition vector but ernsure that the index condIndex remains less than the total length of the condition vector.
                    if (hourTotal >= conditionCurve.Points[condIndex].X &&
                        condIndex < conditionCurve.Points.Length - 1)
                    {
                        condIndex++;
                    }

                    //Compute the slope of the condition curve between the point condIndex and condIndex-1.
                    var slope = (conditionCurve.Points[condIndex].Y - conditionCurve.Points[condIndex - 1].Y) /
                                (conditionCurve.Points[condIndex].X - conditionCurve.Points[condIndex - 1].X);
                    //Now compute the value of the condition for the month i...
                    var temp = conditionCurve.Points[condIndex - 1].Y + slope * hourTotal;
                    conditionVector[i] = temp < 0 ? 0 : temp > 10 ? 10 : temp;
                }
            }

            return conditionVector;
        }

        public static bool IsBestConditionScore(double score, double bestConditionScore)
        {
            return Math.Abs(score - bestConditionScore) < CommonConstants.DoubleDifferenceTolerance;
        }

        public static int? LastInServiceOffset(ConditionScoreAnswerDto[] conditionScoreAnswers, DateTime? inServiceDate, int startFiscalYear, double bestConditionScore)
        {
           
            var bestScoreAnswers = conditionScoreAnswers != null
                ? conditionScoreAnswers.Where(x => Math.Abs(x.ConditionScore - bestConditionScore) < CommonConstants.DoubleDifferenceTolerance).ToList()
                : null;

            if ( bestScoreAnswers == null || bestScoreAnswers.Count <= 0)
            {
                return (inServiceDate.HasValue) ? FormulaBase.ConvertDateTimeToOffset(inServiceDate.Value, startFiscalYear) : (int?) null;
            }

            return FormulaBase.ConvertDateTimeToOffset(bestScoreAnswers.Last().Date, startFiscalYear);
        }

        public static DateTime? FindLastInServiceCalendarDate(double?[] conditions, int startFiscalYear, double bestCondition)
        {
            if (conditions == null) return null;
            
            var lastBestConditionOffset = Array.FindLastIndex(conditions, x => Equals(x, bestCondition));
            if (lastBestConditionOffset  < 0) return null;

            return ConvertMonthOffsetToCalendarDateTime(startFiscalYear, lastBestConditionOffset );
        }

        public static DateTime ConvertMonthOffsetToCalendarDateTime(int startFiscalYear, int monthOffset)
        {
            var year = startFiscalYear + monthOffset / CommonConstants.MonthsPerYearInt;
            var month = monthOffset % CommonConstants.MonthsPerYearInt + 1;
            return FormulaBase.GetCalendarDateTime(year, month);
        }

        public static int? LastInServiceMonthOffset(double?[] conditions, DateTime? inServiceDate, int startFiscalYear, double bestConditionScore)
        {
            var inServiceCalendarDate = FindLastInServiceCalendarDate(conditions, startFiscalYear, bestConditionScore);
            if (!inServiceCalendarDate.HasValue)
            {
                if (inServiceDate.HasValue)
                {
                    inServiceCalendarDate = inServiceDate;
                }
            }
            if (!inServiceCalendarDate.HasValue)
            {
                return null;
            }
            return FormulaBase.ConvertDateTimeToOffset(inServiceCalendarDate.Value, startFiscalYear);
        }

        public static bool IsFirstReplacement(double?[] conditions, double bestCondition)
        {
            if (conditions != null)
            {
                return !conditions.Any(x => Equals(x, bestCondition));
            }
            return true;
        }
    }
}
