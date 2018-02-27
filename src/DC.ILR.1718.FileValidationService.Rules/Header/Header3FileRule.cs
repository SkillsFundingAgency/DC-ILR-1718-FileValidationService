using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using DC.ILR.FileValidationService.Rules.Query;

namespace DC.ILR.FileValidationService.Rules.Header
{
    public class Header3FileRule : AbstractFileRule, IRule<IlrFileData>
    {
        private readonly string _ruleName = "Header_3";
        private readonly IIlrFileNameQueryService _fileNameQueryService;

        public Header3FileRule(IValidationFileErrorHandler validationFileErrorHandler, IIlrFileNameQueryService fileNameQueryService)
            : base(validationFileErrorHandler)
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