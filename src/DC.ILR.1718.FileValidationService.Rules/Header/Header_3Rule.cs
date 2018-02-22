using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using DC.ILR.FileValidationService.Rules.Query;

namespace DC.ILR.FileValidationService.Rules.Header
{
    public class Header_3Rule : AbstractRule, IRule<IlrFileData>
    {
        private readonly string _ruleName = "Header_3";
        private readonly IIlrFileNameQueryService _fileNameQueryService;

        public Header_3Rule(IValidationErrorHandler validationErrorHandler, IIlrFileNameQueryService fileNameQueryService)
            : base(validationErrorHandler)
        {
            _fileNameQueryService = fileNameQueryService;
        }

        public void Validate(IlrFileData messageToValidate)
        {
            if (ConditionMet(messageToValidate.FileName, messageToValidate.Message.HeaderEntity.SourceEntity.UKPRN))
            {
                HandleValidationError(_ruleName, messageToValidate.FileName, null);
            }
        }

        public bool ConditionMet(string fileName, long? ukprn)
        {
            return _fileNameQueryService.GetUkprn(fileName) != ukprn;
        }
    }
}