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
    public class HSDSEInjuryRisk : HSDSEInjuryRiskBase
    {
        public override double?[] GetUnits(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
            return null;
            //TDO add measure
//            double?[] NoDSEInjuries = timeInvariantData.Health_32_and_32_safety_Number_32_of_32_DSE_32_injuries_LikelihoodUnitOutput;
//    		double CostDSE = timeInvariantData.SystemDSEInjuryCost ?? 0;
    		
//    		return ArrayHelper.MultiplyStreamOfValuesByConstant(NoDSEInjuries, CostDSE);
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
