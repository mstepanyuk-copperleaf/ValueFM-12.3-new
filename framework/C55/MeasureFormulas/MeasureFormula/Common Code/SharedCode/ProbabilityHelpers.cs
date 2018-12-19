using System;
using System.Linq;
using CL.FormulaHelper.DTOs;
using CL.FormulaHelper;


namespace MeasureFormula.SharedCode
{
    /// <summary>
    /// The table below summarizes which probability functions to use in which scenarios.
    /// 
    /// Probability - PDF (density function), with subsampling
    ///    Baseline: PDFSubsamplingBaselineProbabilities
    ///    Outcome:  PDFSubsamplingOutcomeProbabilities
    /// 
    /// Probability - Conditional (Legacy optimization)
    ///    Outcome and Baseline: ConditionalProbabilities
    /// This is the default preferred mapping for investment optimization.
    /// 
    /// Probability - Legacy (AA)
    ///    Baseline: LegacyAABaselineProbabilities
    ///    Outcome:  LegacyAAOutcomeProbabilities
    /// This is the default preferred mapping for asset analytics.
    /// </summary>

    public static class ProbabilityHelpers
    {
        /// <summary>
        /// Probability density functions are an attempt to reduce the overly high risk and cost over long time frames
        /// of cumulative density functions.  However they understate the risk of failure in later years because 
        /// that failure was already accounted for.  Subsampling alleviates the problem by adding a second probability density
        /// function for the replacement asset.  The number of subsamples how many different times to average in 
        /// </summary>
        /// <param name="conditionToFailureCurve"> Asset condition to conditional probability of failure in a year</param>
        /// <param name="numBaselinePdfSubsamples">Controls the number of times replacement with another asset is considered.</param>
        /// <returns>The monthly probability density values including the lifetime of a single reactive replacement asset.</returns>
        public static double?[] PDFSubsamplingBaselineProbabilities(
            int startFiscalYear,
            int months,
            double?[] monthlyBaselineConditionScores,
            double bestConditionScore,
            double worstConditionScore,
            XYCurveDTO ageToConditionCurve,
            XYCurveDTO conditionToFailureCurve,
            int? numBaselinePdfSubsamples)
        {
            var startOffsetInMonthsFromStartFiscalYear = 0;
            return FormulaBase.PdfValuesWithSubsampling(
                startFiscalYear,
                months,
                startOffsetInMonthsFromStartFiscalYear,
                monthlyBaselineConditionScores,
                bestConditionScore,
                worstConditionScore,
                ageToConditionCurve,
                conditionToFailureCurve,
                numBaselinePdfSubsamples ?? CommonConstants.NoSubsampling);
            }
        
        /// <summary>
        /// See <see cref="PDFSubsamplingBaselineProbabilities(int, int,double?[], double, double, XYCurveDTO, XYCurveDTO, int?)"/>  for suitability.
        /// This is its partner for the Outcome probabilitiy
        /// </summary>
        /// <param name="conditionToFailureCurve"> Asset condition to conditional probability of failure in a year</param>
        /// <returns>The monthly probabilities of the impact event occurring.</returns>
        public static double?[] PDFSubsamplingOutcomeProbabilities(
            double?[] monthlyBaselineConditionScores,
            double?[] monthlyOutcomeConditionScores,
            double bestConditionScore,
            double worstConditionScore,
            XYCurveDTO conditionToFailureCurve)
        {
            return FormulaBase.MonthlyImpactProbabilities(
                                                          monthlyBaselineConditionScores,
                                                          conditionToFailureCurve,
                                                          monthlyOutcomeConditionScores,
                                                          conditionToFailureCurve,
                                                          bestConditionScore,
                                                          worstConditionScore);
        }
        
        /// <summary>
        /// This is the recommended failure measure for investment optimization.
        /// Because it uses conditional probabilities, it becomes unrealistic over longer time frames tending to overstate risk and cost.
        /// In general with this measure, the maximum value is achieved by letting the asset condition go to 0 and replacing at condition 0.
        /// Two factors that mitigate this behaviour are the value calculation horizon and discounting.
        /// 
        /// Use for both Baseline and Outcome.
        /// </summary>
        /// <param name="conditionToFailureCurve"> Asset condition to conditional probability of failure in a year</param>
        public static double?[] ConditionalProbabilities
        (
            double?[] monthlyConditionScores,
            XYCurveDTO conditionToFailureCurve
        )
        {
            var missingAnInput = (monthlyConditionScores == null || conditionToFailureCurve == null);
            if (missingAnInput) return null;

            return FormulaBase.ConvertConditionToMonthlyProbability(
                monthlyConditionScores,
                conditionToFailureCurve,
                treatProbabilityAsFrequency: true);  // check with Nick
        }

        /// <summary>
        /// This is the recommended failure measure for asset analytics.  
        /// Designed for calculating the cost of the current asset up until the point of intervention, and to ignore costs after the intervention
        /// Any benefits/risk mitigation following the intervention is ignored.
        /// Because it uses conditional probabilities, it becomes unrealistic over longer time frames tending to overstate risk and cost.
        /// These value measures fit well with the asset analytics report and are well behaved in asset analytics.
        /// Does tend to be over-aggressive in asset replacements.
        /// </summary>
        /// <param name="conditionToFailureCurve"> Asset condition to conditional probability of failure in a year</param>
        public static double?[] LegacyAABaselineProbabilities(
            double?[] monthlyBaselineConditionScores,
            double bestConditionScore,
            XYCurveDTO conditionToFailureCurve)
        {
            var missingAnInput = (monthlyBaselineConditionScores == null || conditionToFailureCurve == null);
            if (missingAnInput) return null;

            const int interventionMonthOffset = 0;
            return LegacyAAProbabilities(interventionMonthOffset, monthlyBaselineConditionScores, bestConditionScore, conditionToFailureCurve);
        }

        /// <summary>
        /// See <see cref="LegacyAABaselineProbabilities(double?[], double, XYCurveDTO)"/>  for suitability.
        /// This is its partner for the Outcome probabilitiy
        /// </summary>
        /// <param name="conditionToFailureCurve"> Asset condition to conditional probability of failure in a year</param>
        public static double?[] LegacyAAOutcomeProbabilities(
            int startFiscalYear,
            DateTime interventionCalendarYearStartTime,
            double?[] monthlyBaselineConditionScores,
            double bestConditionScore,
            XYCurveDTO conditionToFailureCurve)
        {
            var missingAnInput = (monthlyBaselineConditionScores == null || conditionToFailureCurve == null);
            if (missingAnInput) return null;

            var interventionMonth = FormulaBase.ConvertDateTimeToOffset(interventionCalendarYearStartTime, startFiscalYear);

            return LegacyAAProbabilities(interventionMonth, monthlyBaselineConditionScores, bestConditionScore, conditionToFailureCurve);
        }

        private static double? SubtractMintProbabilityOfFailure(
            int monthIndex,
            double? monthlyProbabilityFailure,
            int interventionMonth,
            double? mintMonthlyProbabilityFailure)
        {
            return (monthIndex >= interventionMonth) ? monthlyProbabilityFailure - mintMonthlyProbabilityFailure : 0;
        }

        private static double?[] LegacyAAProbabilities(
            int interventionMonth,
            double?[] monthlyBaselineConditionScores,
            double bestConditionScore,
            XYCurveDTO conditionToFailureCurve)
        {
            var monthlyProbabilities =
                FormulaBase.ConvertConditionToMonthlyProbability(monthlyBaselineConditionScores,
                                                                 conditionToFailureCurve,
                                                                 treatProbabilityAsFrequency: true);

            if (monthlyProbabilities == null) return null;

            var mintMonthlyProbability =
                HelperFunctions.GetMonthlyProbabilityValue(conditionToFailureCurve.YFromX(bestConditionScore));

            return monthlyProbabilities.
                Select((monthlyProbability, monthIndex) => SubtractMintProbabilityOfFailure(monthIndex,
                                                                                            monthlyProbability,
                                                                                            interventionMonth,
                                                                                            mintMonthlyProbability)).ToArray();
        }

        private class LogisticRegression
        {
            private readonly double Convexity;

            private readonly double CurveBias;

            private readonly double MidpointAssetHealthIndexScale;

            private readonly double ExpOfMidCondition;

            /// <summary>
            /// For visualization and to explore the curve with different convexity and curveBias
            /// look at AssetPOF-DegradationModel.xlsx
            /// </summary>
            public LogisticRegression(double convexity, double curveBias, double bestConditionScore, double worstConditionScore)
            {
                Convexity = convexity;
                CurveBias = curveBias;
                MidpointAssetHealthIndexScale = (bestConditionScore + worstConditionScore) / 2.0;
                ExpOfMidCondition = Math.Exp(MidpointAssetHealthIndexScale);
            }

            public double LogisticRegressionAt(double condition)
            {
                return ExpOfMidCondition / (1 + Math.Exp(MidpointAssetHealthIndexScale + CurveBias + Convexity * condition));
            }

        }

        public class LogisticConditionToAnnualProbabilityOfFailure
        {
            private readonly LogisticRegression RegressionFunction;
            private readonly double BestConditionScore;
            private readonly double WorstConditionScore;

            public LogisticConditionToAnnualProbabilityOfFailure(
                double bestConditionScore,
                double worstConditionScore,
                double convexity = 0.463,
                double curveBias = 0.22)
            {
                RegressionFunction = new LogisticRegression(convexity, curveBias, bestConditionScore, worstConditionScore);
                BestConditionScore = bestConditionScore;
                WorstConditionScore = worstConditionScore;
            }

            /// <summary>
            /// Condition to Probability of Annual Failure via logistic regression
            /// </summary>
            public XYCurveDTO ConstructConditionToAnnualProbabilityOfFailureCurve()
            {
                var lowestConditionScore = Math.Min(WorstConditionScore, BestConditionScore);
                var highestConditionScore = Math.Max(WorstConditionScore, BestConditionScore);
                int numConditions = 1 + (int)(highestConditionScore - lowestConditionScore);
                const double epsilon = 1e-4;
                var missingLastPoint = Math.Abs(highestConditionScore - (lowestConditionScore + numConditions - 1)) > epsilon;
                if (missingLastPoint)
                    numConditions++;

                var conditionToFailureCurve = new XYCurveDTO
                {
                    Points = Enumerable.Range(0, numConditions).
                        Select(conditionIndex => conditionIndex + lowestConditionScore < highestConditionScore ? new CurvePointDTO
                        {
                            X = conditionIndex + lowestConditionScore,
                            Y = RegressionFunction.LogisticRegressionAt(conditionIndex + lowestConditionScore)
                        } : new CurvePointDTO
                        {
                            X = highestConditionScore,
                            Y = RegressionFunction.LogisticRegressionAt(highestConditionScore)
                        }).ToArray()
                };

                return conditionToFailureCurve;
            }
        }
    }
}
