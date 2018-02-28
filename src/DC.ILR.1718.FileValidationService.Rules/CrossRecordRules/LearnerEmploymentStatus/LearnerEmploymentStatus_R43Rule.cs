using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using ESFA.DC.ILR.Model.Interface;
using System.Linq;

namespace DC.ILR.FileValidationService.Rules.CrossRecordRules.LearnerEmploymentStatus
{
    /// <summary>
    /// A learner must not have more than one Learner Employment Status record with the same Date employment status applies.
    /// </summary>
    public class LearnerEmploymentStatus_R43Rule : AbstractCrossRecordRule, IRule<ILearner>
    {
        private readonly string _ruleName = "LearnerEmploymentStatus_R43";

        public LearnerEmploymentStatus_R43Rule(IValidationCrossRecordErrorHandler validationErrorHandler)
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
            return learner?.LearnerEmploymentStatuses != null &&
                   learner.LearnerEmploymentStatuses.Where(x => x.DateEmpStatAppNullable.HasValue)
                       .GroupBy(x => x.DateEmpStatAppNullable)
                       .Any(y => y.Count() > 1);
        }
    }
}