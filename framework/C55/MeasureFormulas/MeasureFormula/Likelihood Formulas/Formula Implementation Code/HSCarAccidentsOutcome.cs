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
    public class HSCarAccidentsOutcome : HSCarAccidentsOutcomeBase
    {
        public override double?[] GetLikelihoodValues(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
            return null;
            // TODO - add measure
            
//            double?[] CarAccidents = timeInvariantData.Health_32_and_32_safety_Number_32_of_32_car_32_accidents_LikelihoodUnitOutput_B;
    		
//    		double PercentReduction = timeInvariantData.PercentReduction;
    		
//    		var PercentReductionPC = 1.0 - (PercentReduction / 100.0);
    		
//    		return ArrayHelper.MultiplyStreamOfValuesByConstant(CarAccidents, PercentReductionPC);
        }
    }
}
