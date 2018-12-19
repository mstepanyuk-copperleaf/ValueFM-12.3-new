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
    public class CMCarbonRisk : CMCarbonRiskBase
    {
        public override double?[] GetUnits(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
            double?[] CompanyT= timeInvariantData.Mileage_Carbon_32_savings_32__8211__32_Company_32__40_Tonnes_32_C02e_41__ConsqUnitOutput;
    		var ConvertCarbon = timeInvariantData.SystemNon_45_traded_32_price_32_of_32_carbon_32__40_per_32_Tonnes_32_C02e_41_;
    		
    		return ArrayHelper.MultiplyArrayByTimeSeries(CompanyT, ConvertCarbon, startFiscalYear);
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
