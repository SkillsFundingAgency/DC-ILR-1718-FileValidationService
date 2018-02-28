using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using ESFA.DC.ILR.Model.Interface;
using System.Collections.Generic;
using System.Linq;

namespace DC.ILR.FileValidationService.Rules.CrossRecordRules.LearningDelivery
{
    /// <summary>
    /// "Traineeship and apprenticeship framework rule:
    /// All component aims must have a corresponding programme aim with the same combination of Programme type, Framework code and Apprenticeship pathway.
    ///If the Framework code and Apprenticeship pathway have not been returned on the component aim, there must be a programme aim with the same NULL values. "
    /// </summary>
    public class LearningDelivery_R30Rule : AbstractCrossRecordRule, IRule<ILearner>
    {
        private readonly string _ruleName = "LearningDelivery_R30";

        public LearningDelivery_R30Rule(IValidationCrossRecordErrorHandler validationErrorHandler)
            : base(validationErrorHandler)
        {
        }

        public void Validate(ILearner objectToValidate)
        {
            if (objectToValidate?.LearningDeliveries == null)

            {
                return;
            }

            foreach (var learningDelivery in GetComponentAims(objectToValidate.LearningDeliveries))
            {
                if (ConditionMet(learningDelivery, objectToValidate.LearningDeliveries) &&
                    !Exclude(learningDelivery))
                {
                    HandleValidationError(_ruleName, objectToValidate.LearnRefNumber,
                        learningDelivery.AimSeqNumberNullable);
                }
            }
        }

        public bool ConditionMet(ILearningDelivery learningDelivery, IReadOnlyCollection<ILearningDelivery> learningDeliveries)
        {
            return !learningDeliveries.Any(x => x.AimTypeNullable.HasValue &&
                                               x.AimTypeNullable.Value == 1 &&
                                               x.ProgTypeNullable == learningDelivery.ProgTypeNullable &&
                                               x.FworkCodeNullable == learningDelivery.FworkCodeNullable &&
                                               x.PwayCodeNullable == learningDelivery.PwayCodeNullable);
        }

        public IEnumerable<ILearningDelivery> GetComponentAims(IReadOnlyCollection<ILearningDelivery> learningDeliveries)
        {
            return learningDeliveries.Where(x => x.AimTypeNullable.HasValue &&
                                               x.AimTypeNullable.Value == 3);
        }

        public bool Exclude(ILearningDelivery learningDelivery)
        {
            return learningDelivery.ProgTypeNullable.HasValue &&
                   learningDelivery.ProgTypeNullable.Value == 25;
        }
    }
}