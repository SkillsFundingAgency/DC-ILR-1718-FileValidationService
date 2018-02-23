using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using DC.ILR.FileValidationService.Rules.Query;

namespace DC.ILR.FileValidationService.Rules.FileName
{
    public class FileName_7Rule : AbstractRule, IRule<IlrFileData>
    {
        private readonly string _ruleName = "Filename_7";
        private readonly IIlrFileNameQueryService _fileNameQueryService;

        public FileName_7Rule(IValidationErrorHandler validationErrorHandler, IIlrFileNameQueryService fileNameQueryService)
            : base(validationErrorHandler)
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
            var serialNumber = _fileNameQueryService.GetSerialNumber(fileName);
            return string.IsNullOrEmpty(serialNumber) || serialNumber == "00";
        }
    }
}