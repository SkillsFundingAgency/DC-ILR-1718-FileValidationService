using System.Collections.Generic;

namespace DC.ILR._1718.FileValidationService.Interfaces
{
    public interface IValidationErrorHandler
    {
        void Handle(string ruleName, string fileName, long? ukprn = null, IEnumerable<string> errorMessageParameters = null);
    }
}