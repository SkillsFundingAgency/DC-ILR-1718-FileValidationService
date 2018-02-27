using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using DC.ILR.FileValidationService.Rules.Query;

namespace DC.ILR.FileValidationService.Rules.FileName
{
    public class FileName1FileRule : AbstractFileRule, IRule<IlrFileData>
    {
        private readonly string _ruleName = "Filename_1";
        private readonly IIlrFileNameQueryService _fileNameQueryService;

        public FileName1FileRule(IValidationFileErrorHandler validationFileErrorHandler, IIlrFileNameQueryService fileNameQueryService)
            : base(validationFileErrorHandler)
        {
            _fileNameQueryService = fileNameQueryService;
        }

        public void Validate(IlrFileData fileToValidate)
        {
            if (ConditionMet(fileToValidate.FileName))
            {
                HandleValidationError(_ruleName, fileToValidate.FileName);
            }
        }

        public bool ConditionMet(string fileName)
        {
            return !_fileNameQueryService.IsValidFileName(fileName);
        }
    }
}