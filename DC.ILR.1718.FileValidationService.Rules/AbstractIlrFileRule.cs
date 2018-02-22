using System.Collections.Generic;
using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model.Interfaces;

namespace DC.ILR.FileValidationService.Rules
{
    public abstract class AbstractIlrFileRule : IIlrFileRule
    {
        private readonly IValidationErrorHandler _validationErrorHandler;

        protected AbstractIlrFileRule(IValidationErrorHandler validationErrorHandler)
        {
            _validationErrorHandler = validationErrorHandler;
        }

        public abstract void Validate(IIlrFile fileToValidate);

        protected void HandleValidationError(string ruleName, string fileName, long? ukprn = null, IEnumerable<string> errorMessageParameters = null)
        {
            _validationErrorHandler.Handle(ruleName, fileName, ukprn, errorMessageParameters);
        }
    }
}
