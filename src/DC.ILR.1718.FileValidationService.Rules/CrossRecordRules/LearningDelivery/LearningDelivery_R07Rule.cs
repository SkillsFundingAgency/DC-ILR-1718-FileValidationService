using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using ESFA.DC.ILR.Model.Interface;
using System.Linq;

namespace DC.ILR.FileValidationService.Rules.CrossRecordRules.LearningDelivery
{
    /// <summary>
    /// A learner must not have more than one Learning Delivery record with the same Aim sequence number.
    /// </summary>
    public class LearningDelivery_R07Rule : AbstractCrossRecordRule, IRule<ILearner>
    {
        private readonly string _ruleName = "LearningDelivery_R07";

        public LearningDelivery_R07Rule(IValidationCrossRecordErrorHandler validationErrorHandler)
            : base(validationErrorHandler)
        {
        }

        public void Validate(ILearner objectToValidate)
        {
            if (ConditionMet(objectToValidate))
            {
                HandleValidationError(_ruleName, objectToValidate.LearnRefNumber);
            }
        }

        public bool ConditionMet(ILearner learner)
        {
            return learner?.LearningDeliveries != null &&
                   learner.LearningDeliveries.GroupBy(x => x.AimSeqNumberNullable)
                       .Any(y => y.Count() > 1);
        }
    }
}