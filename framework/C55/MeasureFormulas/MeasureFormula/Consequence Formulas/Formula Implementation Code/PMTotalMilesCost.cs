using System.Collections.Generic;
using CL.FormulaHelper.Attributes;
using System.Linq;
using System;
using CL.FormulaHelper;
using MeasureFormulas.Generated_Formula_Base_Classes;
using MeasureFormula.Common_Code;
using MeasureFormula.SharedCode;

namespace CustomerFormulaCode
{
    [Formula]
    public class PMTotalMilesCost : PMTotalMilesCostBase
    {
        public override double?[] GetUnits(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
    		double?[] privateMiles = timeInvariantData.Mileage_Total_32_mileage_32__8211__32_Private_LikelihoodUnitOutput;
    		double privateCost = timeInvariantData.SystemPrivate_32_mileage_32_cost_32__40__163__32_per_32_mile_41_ ?? 0;
    		
    		var totalPMileageCost = ArrayHelper.MultiplyStreamOfValuesByConstant(privateMiles, privateCost);
    		
    		return totalPMileageCost;
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
