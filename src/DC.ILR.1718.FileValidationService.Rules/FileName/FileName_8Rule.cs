using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using DC.ILR.FileValidationService.Rules.Query;
using System;

namespace DC.ILR.FileValidationService.Rules.FileName
{
    public class FileName_8Rule : AbstractRule, IRule<IlrFileData>
    {
        private readonly string _ruleName = "Filename_8";
        private readonly IIlrFileNameQueryService _fileNameQueryService;

        public FileName_8Rule(IValidationErrorHandler validationErrorHandler, IIlrFileNameQueryService fileNameQueryService)
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
            var fileDateTime = _fileNameQueryService.GetFileDateTime(fileName);
            return fileDateTime == null ||
                   fileDateTime > DateTime.Now;
        }
    }
}