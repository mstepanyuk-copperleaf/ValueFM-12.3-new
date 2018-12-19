using System.Collections.Generic;
using CL.FormulaHelper.Attributes;
using System.Linq;
using System;
using CL.FormulaHelper;
using MeasureFormulas.Generated_Formula_Base_Classes;

namespace CustomerFormulaCode
{
    [Formula]
    public class HSRiskOtherTotal : HSRiskOtherTotalBase
    {
        public override double?[] GetUnits(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {

            return null;
            // TODO - add measure
            // double?[] OtherInjuries = timeInvariantData.Health_32_and_32_safety_Health_32_and_32_safety_32_risk_32__40_Other_32_injuries_41__ConsqUnitOutput;
        
    		// return OtherInjuries;
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
