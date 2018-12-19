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
    public class HSOperativeFatalities : HSOperativeFatalitiesBase
    {
        public override double?[] GetLikelihoodValues(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
            if (timeInvariantData.Probability_32_of_32_operative_32_fatality == null){
    			
    			return null;
    		}else{
    			
    		var AssetFailProb =  InterpolatePropagate<TimeVariantInputDTO>(timeVariantData, startFiscalYear, months, (x => (x.ProbAssetFailureEvent/(12.0*100.0))));
    		int NoOfOperatives = timeInvariantData.Number_32_of_32_operatives_32_affected ?? 0;
    		double FatalityProb = timeInvariantData.Probability_32_of_32_operative_32_fatality ?? 0;
    		
    		var OperativeFatality = NoOfOperatives * FatalityProb/100.0;
    		
    		var Array1 = ArrayHelper.MultiplyStreamOfValuesByConstant(AssetFailProb, OperativeFatality);
    		
    		return Array1;
        }
    }
}
}