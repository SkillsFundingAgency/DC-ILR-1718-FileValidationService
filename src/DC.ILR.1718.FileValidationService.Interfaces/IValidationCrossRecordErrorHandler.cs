using System.Collections.Generic;

namespace DC.ILR.FileValidationService.Interfaces
{
    public interface IValidationCrossRecordErrorHandler
    {
        void Handle(string ruleName, string learnRefNumber = null, long? aimSequenceNumber = null,
            IEnumerable<string> errorMessageParameters = null);
    }
}