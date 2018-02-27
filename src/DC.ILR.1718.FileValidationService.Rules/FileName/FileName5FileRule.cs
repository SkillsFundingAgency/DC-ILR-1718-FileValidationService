using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using DC.ILR.FileValidationService.Rules.Constants;
using DC.ILR.FileValidationService.Rules.Query;

namespace DC.ILR.FileValidationService.Rules.FileName
{
    public class FileName5FileRule : AbstractFileRule, IRule<IlrFileData>
    {
        private readonly string _ruleName = "Filename_5";
        private readonly IIlrFileNameQueryService _fileNameQueryService;

        public FileName5FileRule(IValidationFileErrorHandler validationFileErrorHandler, IIlrFileNameQueryService fileNameQueryService)
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
            return _fileNameQueryService.GetYearOfCollection(fileName) != IlrFileConstants.AcademicYear;
        }
    }
}