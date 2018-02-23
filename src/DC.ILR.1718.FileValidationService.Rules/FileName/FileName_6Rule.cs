using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using DC.ILR.FileValidationService.Rules.Query;
using System.Text.RegularExpressions;

namespace DC.ILR.FileValidationService.Rules.FileName
{
    public class FileName_6Rule : AbstractRule, IRule<IlrFileData>
    {
        private readonly string _ruleName = "Filename_6";
        private readonly IIlrFileNameQueryService _fileNameQueryService;
        private readonly Regex _serialNumberRegex = new Regex("^[0-9]{2}$", RegexOptions.Compiled);

        public FileName_6Rule(IValidationErrorHandler validationErrorHandler, IIlrFileNameQueryService fileNameQueryService)
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
            return string.IsNullOrEmpty(serialNumber) ||
                   !_serialNumberRegex.IsMatch(serialNumber);
        }
    }
}