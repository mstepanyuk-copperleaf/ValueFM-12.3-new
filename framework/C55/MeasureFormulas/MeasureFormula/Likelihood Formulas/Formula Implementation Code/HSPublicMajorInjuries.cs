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
    public class HSPublicMajorInjuries : HSPublicMajorInjuriesBase
    {
        public override double?[] GetLikelihoodValues(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
            if (timeInvariantData.Probability_32_of_32_member_32_of_32_public_32_major_32_injury == null){
    			
    			return null;
    		}else{
    			
    		var AssetFailProb =  InterpolatePropagate<TimeVariantInputDTO>(timeVariantData, startFiscalYear, months, (x => (x.ProbAssetFailureEvent/(12.0*100.0))));
    		int NoOfPublic = timeInvariantData.Number_32_of_32_members_32_of_32_public_32_affected ?? 0;
    		double MajorInjuryProb = timeInvariantData.Probability_32_of_32_member_32_of_32_public_32_major_32_injury ?? 0;
    		
    		var PublicMajorInjury = NoOfPublic * MajorInjuryProb/CommonConstants.PercentPerProbabilityOne;
    		
    		var Array1 = ArrayHelper.MultiplyStreamOfValuesByConstant(AssetFailProb, PublicMajorInjury);
    		
    		return Array1;
        }
    }
}
}