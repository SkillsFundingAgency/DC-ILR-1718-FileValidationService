using DC.ILR.FileValidationService.Interfaces;
using System.Collections.Generic;

namespace DC.ILR.FileValidationService.Rules.AbstractRules
{
    public abstract class AbstractRule
    {
        private readonly IValidationErrorHandler _validationErrorHandler;

        protected AbstractRule(IValidationErrorHandler validationErrorHandler)
        {
            _validationErrorHandler = validationErrorHandler;
        }

        protected void HandleValidationError(string ruleName, string fileName = null, IEnumerable<string> errorMessageParameters = null)
        {
            _validationErrorHandler.Handle(ruleName, fileName, errorMessageParameters);
        }
    }
}