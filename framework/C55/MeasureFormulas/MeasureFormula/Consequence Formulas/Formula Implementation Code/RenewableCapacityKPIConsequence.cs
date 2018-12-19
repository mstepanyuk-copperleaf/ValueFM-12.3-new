using System.Collections.Generic;
using CL.FormulaHelper.Attributes;
using MeasureFormulas.Generated_Formula_Base_Classes;
using MeasureFormula.SharedCode;

namespace CustomerFormulaCode
{
    [Formula]
    public class RenewableCapacityKPIConsequence : RenewableCapacityKPIConsequenceBase
    {
        public override double?[] GetUnits(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
            return InterpolatePropagate<TimeVariantInputDTO>(timeVariantData,
                                                             startFiscalYear,
                                                             months, (x => x.Renewable_32_Capacity_32_Added / CommonConstants.MonthsPerYear));
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
