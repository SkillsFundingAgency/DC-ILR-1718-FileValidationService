using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using ESFA.DC.ILR.Model.Interface;
using System.Collections.Generic;
using System.Linq;

namespace DC.ILR.FileValidationService.Rules.CrossRecordRules.LearningDelivery
{
    /// <summary>
    /// "Programme rule: All open component aims must have the same combination of Programme type, Framework code, Apprenticeship pathway and Standard code as recorded on an open programme aim.
    ///If the Framework code and Apprenticeship pathway or Standard code have not been returned on the open component aims, there must be an open programme aim with the same NULL values."
    /// </summary>
    public class LearningDelivery_R29Rule : AbstractCrossRecordRule, IRule<ILearner>
    {
        private readonly string _ruleName = "LearningDelivery_R29";

        public LearningDelivery_R29Rule(IValidationCrossRecordErrorHandler validationErrorHandler)
            : base(validationErrorHandler)
        {
        }

        public void Validate(ILearner objectToValidate)
        {
            foreach (var learningDelivery in GetOpenComponentAims(objectToValidate.LearningDeliveries))
            {
                if (ConditionMet(learningDelivery, objectToValidate.LearningDeliveries))
                {
                    HandleValidationError(_ruleName, objectToValidate.LearnRefNumber, learningDelivery.AimSeqNumberNullable);
                }
            }
        }

        public bool ConditionMet(ILearningDelivery learningDelivery, IReadOnlyCollection<ILearningDelivery> learningDeliveries)
        {
            return !learningDeliveries.Any(x => x.AimTypeNullable.HasValue &&
                                               x.AimTypeNullable.Value == 1 &&
                                               !x.LearnActEndDateNullable.HasValue &&
                                               x.StdCodeNullable == learningDelivery.StdCodeNullable &&
                                               x.ProgTypeNullable == learningDelivery.ProgTypeNullable &&
                                               x.FworkCodeNullable == learningDelivery.FworkCodeNullable &&
                                               x.PwayCodeNullable == learningDelivery.PwayCodeNullable);
        }

        public IEnumerable<ILearningDelivery> GetOpenComponentAims(IReadOnlyCollection<ILearningDelivery> learningDeliveries)
        {
            return learningDeliveries.Where(x => x.AimTypeNullable.HasValue &&
                                               x.AimTypeNullable.Value == 3 &&
                                               !x.LearnActEndDateNullable.HasValue);
        }
    }
}