using DC.ILR.FileValidationService.Interfaces;
using System.Collections.Generic;

namespace DC.ILR.FileValidationService.Rules.AbstractRules
{
    public abstract class AbstractCrossRecordRule
    {
        private readonly IValidationCrossRecordErrorHandler _validationErrorHandler;

        protected AbstractCrossRecordRule(IValidationCrossRecordErrorHandler validationErrorHandler)
        {
            _validationErrorHandler = validationErrorHandler;
        }

        protected void HandleValidationError(string ruleName, string learnRefNumber = null, long? aimSequenceNumber = null, IEnumerable<string> errorMessageParameters = null)
        {
            _validationErrorHandler.Handle(ruleName, learnRefNumber, aimSequenceNumber, errorMessageParameters);
        }
    }
}