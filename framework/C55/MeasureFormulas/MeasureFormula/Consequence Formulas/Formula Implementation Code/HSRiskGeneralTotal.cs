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
    public class HSRiskGeneralTotal : HSRiskGeneralTotalBase
    {
        public override double?[] GetUnits(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
            return null;
            // TODO - add measure
            
//    		double?[] Fatalities = timeInvariantData.Health_32_and_32_safety_Health_32_and_32_safety_32_risk_32__40_Fatalities_41__ConsqUnitOutput;
//    		double?[] MajorInjuries = timeInvariantData.Health_32_and_32_safety_Health_32_and_32_safety_32_risk_32__40_Major_32_injuries_41__ConsqUnitOutput;
//    		double?[] MinorInjuries = timeInvariantData.Health_32_and_32_safety_Health_32_and_32_safety_32_risk_32__40_Minor_32_injuries_41__ConsqUnitOutput;
//        
//    		return ArrayHelper.SumArrays(new [] {Fatalities, MajorInjuries, MinorInjuries});
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
