// GENERATED CODE - DO NOT EDIT !!!
using System.Collections.Generic;
using CL.FormulaHelper;
using CL.FormulaHelper.Attributes;
using CL.FormulaHelper.DTOs;
using System.Runtime.Serialization;

namespace MeasureFormulas.Generated_Formula_Base_Classes
{
    [FormulaBase]
    public abstract class AvoidedFutureAssetReplacementLikelihood_MSBase : FormulaLikelihoodBase
    {
        [DataContract]
        public class TimeInvariantInputDTO
        {
            public TimeInvariantInputDTO(
                System.Boolean p_AlternativeIsAnalyticsAlternative,
                CL.FormulaHelper.DTOs.AlternativeMilestoneSetDTO p_AlternativeMilestones,
                CL.FormulaHelper.DTOs.DistributionByAccountTypeDTO p_InvestmentSpendByAccountType,
                System.String p_SystemAlternativeMilestoneISDCode,
                System.Int32 p_Years_32_to_32_Certain_32_Replacement)
            {
                AlternativeIsAnalyticsAlternative = p_AlternativeIsAnalyticsAlternative;
                AlternativeMilestones = p_AlternativeMilestones;
                InvestmentSpendByAccountType = p_InvestmentSpendByAccountType;
                SystemAlternativeMilestoneISDCode = p_SystemAlternativeMilestoneISDCode;
                Years_32_to_32_Certain_32_Replacement = p_Years_32_to_32_Certain_32_Replacement;
            }
            
            [CoreFieldInput(FormulaCoreFieldInputType.AlternativeIsAnalyticsAlternative)]
            [DataMember]
            public System.Boolean AlternativeIsAnalyticsAlternative  { get; private set; }
            
            [CoreFieldInput(FormulaCoreFieldInputType.AlternativeMilestones)]
            [DataMember]
            public CL.FormulaHelper.DTOs.AlternativeMilestoneSetDTO AlternativeMilestones  { get; private set; }
            
            [CoreFieldInput(FormulaCoreFieldInputType.InvestmentSpendByAccountType)]
            [DataMember]
            public CL.FormulaHelper.DTOs.DistributionByAccountTypeDTO InvestmentSpendByAccountType  { get; private set; }
            
            [CustomFieldInput("AlternativeMilestoneISDCode", FormulaInputAssociatedEntity.System)]
            [DataMember]
            public System.String SystemAlternativeMilestoneISDCode  { get; private set; }
            
            [PromptInput("Years to Certain Replacement")]
            [DataMember]
            public System.Int32 Years_32_to_32_Certain_32_Replacement  { get; private set; }
        }
        
        [DataContract]
        public class TimeVariantInputDTO : ITimeVariantInputDTO
        {
            public TimeVariantInputDTO(
                CL.FormulaHelper.DTOs.TimePeriodDTO p_TimePeriod)
            {
                TimePeriod = p_TimePeriod;
            }
            
            [CoreFieldInput(FormulaCoreFieldInputType.TimePeriod)]
            [DataMember]
            public CL.FormulaHelper.DTOs.TimePeriodDTO TimePeriod  { get; private set; }
        }
        
        public abstract double?[] GetLikelihoodValues(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData);
            
        
        ///
        /// Class to enable formula debugging
        ///
        [DataContract]
        public class FormulaParams : IFormulaParams
        {
            public FormulaParams(CL.FormulaHelper.MeasureOutputType measureOutputType,
                string measureName,
                long alternativeId,
                string formulaImplClassName,
                bool isProbabilityFormula,
                int fiscalYearEndMonth,
                int startFiscalYear,
                int months,
                TimeInvariantInputDTO timeInvariantData,
                IReadOnlyList<TimeVariantInputDTO> timeVariantData,
                double?[] unitOutput,
                object formulaOutput,
                string exceptionMessage)
            {
                MeasureOutputType = measureOutputType;
                MeasureName = measureName;
                AlternativeId = alternativeId;
                FormulaImplClassName = formulaImplClassName;
                IsProbabilityFormula = isProbabilityFormula;
                FiscalYearEndMonth = fiscalYearEndMonth;
                StartFiscalYear = startFiscalYear;
                Months = months;
                TimeInvariantData = timeInvariantData;
                TimeVariantData = timeVariantData;
                UnitOutput = unitOutput;
                FormulaOutput = formulaOutput;
                ExceptionMessage = exceptionMessage;
            }
            [DataMember(Order = 0)]
            public CL.FormulaHelper.MeasureOutputType MeasureOutputType { get; set; }
            [DataMember(Order = 1)]
            public string MeasureName { get; set; }
            [DataMember(Order = 2)]
            public long AlternativeId { get; set; }
            [DataMember(Order = 3)]
            public string FormulaImplClassName { get; set; }
            [DataMember(Order = 4)]
            public bool IsProbabilityFormula { get; set; }
            [DataMember(Order = 5)]
            public int FiscalYearEndMonth { get; set; }
            [DataMember(Order = 6)]
            public int StartFiscalYear { get; set; }
            [DataMember(Order = 7)]
            public int Months { get; set; }
            [DataMember(Order = 8)]
            public TimeInvariantInputDTO TimeInvariantData { get; set; }
            [DataMember(Order = 9)]
            public IReadOnlyList<TimeVariantInputDTO> TimeVariantData { get; set; }
            [DataMember(Order = 10)]
            public double?[] UnitOutput { get; set; }
            [DataMember(Order = 11)]
            public object FormulaOutput { get; set; }
            [DataMember(Order = 12)]
            public string ExceptionMessage { get; set; }
        }
    }
}
// GENERATED CODE - DO NOT EDIT !!!
