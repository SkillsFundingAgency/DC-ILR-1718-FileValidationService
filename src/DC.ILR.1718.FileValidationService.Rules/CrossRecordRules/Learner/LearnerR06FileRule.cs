using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using ESFA.DC.ILR.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DC.ILR.FileValidationService.Rules.CrossRecordRules.Learner
{
    /// <summary>
    /// A Learner reference number must only be used for one learner.
    /// </summary>
    public class LearnerR06FileRule : AbstractFileRule, IRule<IlrFileData>
    {
        private readonly string _ruleName = "Learner_R06";

        public LearnerR06FileRule(IValidationFileErrorHandler validationFileErrorHandler)
            : base(validationFileErrorHandler)
        {
        }

        public void Validate(IlrFileData objectToValidate)
        {
            if (ConditionMet(objectToValidate.Message.Learners))
            {
                //TODO: This is probably the case where sending back the LearnrefNumber will be handy
                HandleValidationError(_ruleName, objectToValidate.FileName);
            }
        }

        public bool ConditionMet(IReadOnlyCollection<ILearner> learners)
        {
            return learners != null &&
                   learners.GroupBy(x => x.LearnRefNumber)
                       .Any(y => y.Count() > 1);
        }
    }
}