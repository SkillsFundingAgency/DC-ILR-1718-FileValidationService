using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model.Interfaces;
using DC.ILR.FileValidationService.Rules.Query;

namespace DC.ILR.FileValidationService.Rules.FileName
{
    public class FileName_1Rule : AbstractIlrFileRule
    {
        private readonly string _ruleName = "Filename_1";
        private readonly IIlrFileNameQueryService _fileNameQueryService;

        public FileName_1Rule(IValidationErrorHandler validationErrorHandler, IIlrFileNameQueryService fileNameQueryService)
            : base(validationErrorHandler)
        {
            _fileNameQueryService = fileNameQueryService;
        }

        public override void Validate(IIlrFile fileToValidate)
        {
            if (ConditionMet(fileToValidate.FileName))
            {
                HandleValidationError(_ruleName, fileToValidate.FileName, fileToValidate.Ukprn);
            }
        }

        public bool ConditionMet(string fileName)
        {
            return !_fileNameQueryService.IsValidFileName(fileName);
        }
    }
}