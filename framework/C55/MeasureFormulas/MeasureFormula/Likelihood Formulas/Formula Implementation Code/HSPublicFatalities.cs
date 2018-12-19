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
    public class HSPublicFatalities : HSPublicFatalitiesBase
    {
        public override double?[] GetLikelihoodValues(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
            if (timeInvariantData.Probability_32_of_32_member_32_of_32_public_32_fatality == null){
    			
    			return null;
    		}else{
    			
    		var AssetFailProb =  InterpolatePropagate<TimeVariantInputDTO>(timeVariantData, startFiscalYear, months, (x => (x.ProbAssetFailureEvent/(12.0*100.0))));
    		int NoOfPublic = timeInvariantData.Number_32_of_32_members_32_of_32_public_32_affected ?? 0;
    		double FatalityProb = timeInvariantData.Probability_32_of_32_member_32_of_32_public_32_fatality ?? 0;
    		
    		var PublicFatality = NoOfPublic * FatalityProb/100.0;
    		
    		var Array1 = ArrayHelper.MultiplyStreamOfValuesByConstant(AssetFailProb, PublicFatality);
    		
    		return Array1;
        }
    }
}
}