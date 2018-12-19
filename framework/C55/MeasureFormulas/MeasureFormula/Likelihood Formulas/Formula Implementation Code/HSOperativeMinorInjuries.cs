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
    public class HSOperativeMinorInjuries : HSOperativeMinorInjuriesBase
    {
        public override double?[] GetLikelihoodValues(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
    		if (timeInvariantData.Probability_32_of_32_operative_32_minor_32_injury == null){
    			
    			return null;
    		}else{
    			
    		var AssetFailProb =  InterpolatePropagate<TimeVariantInputDTO>(timeVariantData, startFiscalYear, months, (x => (x.ProbAssetFailureEvent/(12.0*100.0))));
    		int NoOfOperatives = timeInvariantData.Number_32_of_32_operatives_32_affected ?? 0;
    		double MinorInjuryProb = timeInvariantData.Probability_32_of_32_operative_32_minor_32_injury ?? 0;
    		
    		var OperativeMinorInjury = NoOfOperatives * MinorInjuryProb/100.0;
    		
    		var Array1 = ArrayHelper.MultiplyStreamOfValuesByConstant(AssetFailProb, OperativeMinorInjury);
    		
    		return Array1;
        }
    }
}
}