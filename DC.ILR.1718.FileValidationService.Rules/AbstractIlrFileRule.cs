using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.ILR._1718.FileValidationService.Interfaces;
using DC.ILR._1718.FileValidationService.Model.Interfaces;

namespace DC.ILR._1718.FileValidationService.Rules
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
