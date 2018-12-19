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
    public class PMCostsAvoided : PMCostsAvoidedBase
    {
        public override double?[] GetUnits(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
            return null;
            
            // TODO - add measure
//            double?[] privateMResidual =timeInvariantData.Mileage_Cost_32_of_32_total_32_mileage_32__8211__32_Private_32__40__163__41__ConsqUnitOutput;
//    		double?[] privateMBaseline = timeInvariantData.Mileage_Cost_32_of_32_total_32_mileage_32__8211__32_Private_32__40__163__41__ConsqUnitOutput_B;
        
//    		var residualMileageCost = ArrayHelper.MultiplyStreamOfValuesByConstant(privateMResidual, -1.0);
    		
//    		return ArrayHelper.SumArrays(new [] {residualMileageCost, privateMBaseline});
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
