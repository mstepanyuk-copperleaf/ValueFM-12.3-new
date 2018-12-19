using System;
using CL.FormulaHelper;

namespace MeasureFormula.SharedCode
{
    public static class DateHelpers
    {
        //This method should be renamed to FindLastInServiceOffset
        public static int? FindLastInServiceMonthOffset(double?[] conditions, int startFiscalYear, double bestCondition)
        {
            if (conditions == null) return null;
            
            var lastBestConditionOffset = Array.FindLastIndex(conditions, x => Equals(x, bestCondition));
            return (lastBestConditionOffset  < 0) ? (int?) null : lastBestConditionOffset;

        }

        public static int? LastInServiceMonthOffset(double?[] conditions, DateTime? inServiceDate, int startFiscalYear, double bestConditionScore)
        {
            var inServiceOffset = FindLastInServiceMonthOffset(conditions, startFiscalYear, bestConditionScore);
            
            if (inServiceOffset.HasValue) return inServiceOffset;

            return inServiceDate.HasValue ?  FormulaBase.ConvertDateTimeToOffset(inServiceDate.Value, startFiscalYear) : (int?) null;
        }
        
        private static void FiscalDateTimeFromMonthOffset (int startFiscalYear, int monthOffset, out int fiscalYear, out int fiscalPeriod)
        {
            fiscalYear = startFiscalYear;
            while (monthOffset < 0)
            {
                fiscalYear -= 1;
                monthOffset += 12;
            }

            fiscalYear = startFiscalYear + (monthOffset / 12);
            fiscalPeriod = 1 + monthOffset % 12;
        }

        public static void CalendarYearMonthToFiscalDate(
            DateTime calendarYearMonth,
            int lastCalendarMonthOfFiscalYear,
            out int fiscalYear,
            out int fiscalPeriod)
        {
            if (lastCalendarMonthOfFiscalYear < 1 || lastCalendarMonthOfFiscalYear > 12)
            {
                throw new ArgumentOutOfRangeException("lastCalendarMonthOfFiscalYear",
                    "Value should be 1, 2, 3, ..., or 12.");
            }
            var pseudoDate = calendarYearMonth.AddMonths(12 - lastCalendarMonthOfFiscalYear);
            fiscalYear = pseudoDate.Year;
            fiscalPeriod = pseudoDate.Month;
        }

        public static int ConvertFiscalDateToOffsetFromStartFiscalYear(int fiscalYear, int fiscalPeriod, int startFiscalYear)
        {
            return (fiscalYear - startFiscalYear) * CommonConstants.MonthsPerYearInt + fiscalPeriod - 1;
        }
    }
}
