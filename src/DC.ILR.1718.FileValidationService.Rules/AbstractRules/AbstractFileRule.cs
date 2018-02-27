using DC.ILR.FileValidationService.Interfaces;
using System.Collections.Generic;

namespace DC.ILR.FileValidationService.Rules.AbstractRules
{
    public abstract class AbstractFileRule
    {
        private readonly IValidationFileErrorHandler _validationFileErrorHandler;

        protected AbstractFileRule(IValidationFileErrorHandler validationFileErrorHandler)
        {
            _validationFileErrorHandler = validationFileErrorHandler;
        }

        protected void HandleValidationError(string ruleName, string fileName = null, IEnumerable<string> errorMessageParameters = null)
        {
            _validationFileErrorHandler.Handle(ruleName, fileName, errorMessageParameters);
        }
    }
}