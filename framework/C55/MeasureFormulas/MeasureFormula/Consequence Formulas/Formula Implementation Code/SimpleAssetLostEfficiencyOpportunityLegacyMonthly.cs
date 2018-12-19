using System.Collections.Generic;
using CL.FormulaHelper.Attributes;
using System;
using MeasureFormulas.Generated_Formula_Base_Classes;
using MeasureFormula.Common_Code;
using CL.FormulaHelper.DTOs;
using MeasureFormula.SharedCode;


namespace CustomerFormulaCode
{
    [Formula]
    public class SimpleAssetLostEfficiencyOpportunityLegacyMonthly : SimpleAssetLostEfficiencyOpportunityLegacyMonthlyBase
    {
        public override double?[] GetUnits(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
    		// TODO: AssetAnnual_32_Degradation_32__37_
    		
    		// TODO: AssetTechnology_32_Improvement_32__37_
    		
            if (timeInvariantData.AssetGenerationGroup == null
    		    || !timeInvariantData.AssetGenerationGroup.UnitCapacity.HasValue
    		    || timeInvariantData.StrategyAlternative == null
    		    || timeInvariantData.StrategyAlternative.AvoidedCo2TimeSeries == null)
//              || !timeInvariantData.AssetAnnual_32_Degradation_32__37_.HasValue)
            {
                // Without a generation group we do not know the unit capacity and so cannot
                // determine the lost efficiency opportunity.  Same if there's no "best" score 
                // provided
                return null;
            }

    		double annualDegradation = 0.01; // timeInvariantData.AssetAnnual_32_Degradation_32__37_.Value * 0.01;

            //----- Support multiple replacements... Only include technology improvement if this is the first replacement...
//            double technologyImprovement = timeInvariantData.AssetTechnology_32_Improvement_32__37_.HasValue
//                        ? HelperUtility.IsFirstReplacement(timeInvariantData.GenARM_Condition_ConsqUnitOutput_B, timeInvariantData.SystemCondition_32_Score_32_Best.Value)
//                            ? timeInvariantData.AssetTechnology_32_Improvement_32__37_.Value * 0.01
//                            : 0.0
//                        : 0.0;
            double technologyImprovement = 0.0;
            
            var unitCapacity = timeInvariantData.AssetGenerationGroup.UnitCapacity;

            
            var inServiceMonthOffset =
                DateHelpers.LastInServiceMonthOffset(timeInvariantData.ConditionScore_Condition_ConsqUnitOutput_B,
                                                     timeInvariantData.AssetInServiceDate,
                                                     startFiscalYear,
                                                     CustomerConstants.BestConditionScore);
            if (inServiceMonthOffset == null) return null;
            
            TimeSeriesDTO avoidedCO2Values = timeInvariantData.StrategyAlternative.AvoidedCo2TimeSeries;
            TimeSeriesDTO energyValues = timeInvariantData.StrategyAlternative.DefaultEnergyTimeSeries;
            var energyBaseYear = energyValues.BaseYear??startFiscalYear;

            
            var retStartIndex = Math.Max(0, inServiceMonthOffset.Value);
            var ageInMonthsStartIndex = retStartIndex - inServiceMonthOffset.Value;

            double?[] ret = new double?[months];
            for (int monthOffset = retStartIndex, ageInMonths = ageInMonthsStartIndex; monthOffset < months; monthOffset++, ageInMonths++)
            {
                int fiscalYearOffset = monthOffset / 12;
                int currentFiscalYear = startFiscalYear + fiscalYearOffset;
                if (currentFiscalYear >= energyBaseYear)
                {
                    double monthlyEnergyValue = HelperFunctions.GetMonthlyValue(energyValues, startFiscalYear, monthOffset);
                    double avoidedCO2InDollarsPerMWh = HelperFunctions.GetMonthlyValue(avoidedCO2Values, startFiscalYear, monthOffset);
                    double monthlyDollarsPerMWh = monthlyEnergyValue + avoidedCO2InDollarsPerMWh;
                    double ageInYears = ageInMonths / CommonConstants.MonthsPerYear;
                    ret[monthOffset] = (ageInYears * annualDegradation + technologyImprovement) * unitCapacity / CommonConstants.MonthsPerYear  * monthlyDollarsPerMWh;
                }
            }

            return ret;
    	}


        public override double?[] GetZynos(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData,
            IReadOnlyList<TimeVariantInputDTO> timeVariantData,
            double?[] unitOutput)
        {
            return unitOutput;
        }
    }
}
