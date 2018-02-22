using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model.Interfaces;
using DC.ILR.FileValidationService.Rules.Constants;
using DC.ILR.FileValidationService.Rules.Query;

namespace DC.ILR.FileValidationService.Rules.FileName
{
    public class FileName_5Rule : AbstractIlrFileRule
    {
        private readonly string _ruleName = "Filename_5";
        private readonly IIlrFileNameQueryService _fileNameQueryService;

        public FileName_5Rule(IValidationErrorHandler validationErrorHandler, IIlrFileNameQueryService fileNameQueryService)
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
            return _fileNameQueryService.GetYearOfCollection(fileName) != IlrFileConstants.AcademicYear;
        }
    }
}