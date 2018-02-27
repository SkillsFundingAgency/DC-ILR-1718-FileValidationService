using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using ESFA.DC.ILR.Model.Interface;
using System.Collections.Generic;
using System.Linq;

namespace DC.ILR.FileValidationService.Rules.Entity
{
    public class Entity1FileRule : AbstractFileRule, IRule<IMessage>
    {
        private readonly string _ruleName = "Entity_1";

        public Entity1FileRule(IValidationFileErrorHandler validationFileErrorHandler)
            : base(validationFileErrorHandler)
        {
        }

        public void Validate(IMessage objectToValidate)
        {
            if (ConditionMet(objectToValidate.Learners, objectToValidate.LearnerDestinationAndProgressions))
            {
                HandleValidationError(_ruleName, null, null);
            }
        }

        public bool ConditionMet(IReadOnlyCollection<ILearner> learners, IReadOnlyCollection<ILearnerDestinationAndProgression> learnerDestinationAndProgressions)
        {
            return ConditionMetLearners(learners) &&
                   ConditionMetLearnerDestinationAndProgression(learnerDestinationAndProgressions);
        }

        public bool ConditionMetLearners(IReadOnlyCollection<ILearner> learners)
        {
            return learners == null || !learners.Any();
        }

        public bool ConditionMetLearnerDestinationAndProgression(IReadOnlyCollection<ILearnerDestinationAndProgression> learnerDestinationAndProgressions)
        {
            return learnerDestinationAndProgressions == null || !learnerDestinationAndProgressions.Any();
        }
    }
}