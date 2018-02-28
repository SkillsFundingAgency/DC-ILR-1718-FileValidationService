using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using ESFA.DC.ILR.Model.Interface;
using System.Collections.Generic;
using System.Linq;

namespace DC.ILR.FileValidationService.Rules.CrossRecordRules.LearningDelivery
{
    /// <summary>
    /// Programme rule:
    ///Each open programme aim must have at least one corresponding component aim with the same combination of Programme type, Framework code and Apprenticeship pathway and Standard code.
    ///It is possible for a closed programme aim to have no corresponding component aims. This would occur if the learner transfers from one framework to another and continues with all the original component learning aims.
    /// </summary>
    public class LearningDelivery_R31Rule : AbstractCrossRecordRule, IRule<ILearner>
    {
        private readonly string _ruleName = "LearningDelivery_R31";

        public LearningDelivery_R31Rule(IValidationCrossRecordErrorHandler validationErrorHandler)
            : base(validationErrorHandler)
        {
        }

        public void Validate(ILearner objectToValidate)
        {
            if (objectToValidate? .LearningDeliveries == null)

            {
                return;
            }

            foreach (var learningDelivery in GetMainProgramAims(objectToValidate.LearningDeliveries))
            {
                if (ConditionMet(learningDelivery, objectToValidate.LearningDeliveries)) 
                {
                    HandleValidationError(_ruleName, objectToValidate.LearnRefNumber,
                        learningDelivery.AimSeqNumberNullable);
                }
            }
        }

        public bool ConditionMet(ILearningDelivery learningDelivery, IReadOnlyCollection<ILearningDelivery> learningDeliveries)
        {
            return !learningDeliveries.Any(x => x.AimTypeNullable.HasValue &&
                                               x.AimTypeNullable.Value == 3 &&
                                               x.ProgTypeNullable == learningDelivery.ProgTypeNullable &&
                                                x.StdCodeNullable == learningDelivery.StdCodeNullable &&
                                               x.FworkCodeNullable == learningDelivery.FworkCodeNullable &&
                                               x.PwayCodeNullable == learningDelivery.PwayCodeNullable);
        }

        public IEnumerable<ILearningDelivery> GetMainProgramAims(IReadOnlyCollection<ILearningDelivery> learningDeliveries)
        {
            return learningDeliveries.Where(x => x.AimTypeNullable.HasValue &&
                                                x.AimTypeNullable.Value == 1 &&
                                                !x.LearnActEndDateNullable.HasValue);
        }

      
    }
}