using System.Collections.Generic;

namespace DC.ILR.FileValidationService.Interfaces
{
    public interface IValidationErrorHandler
    {
        void Handle(string ruleName, string fileName, IEnumerable<string> errorMessageParameters = null);
    }
}