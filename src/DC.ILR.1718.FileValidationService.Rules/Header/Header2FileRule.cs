using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Model;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using ESFA.DC.ILR.Model.Interface;
using System;

namespace DC.ILR.FileValidationService.Rules.Header
{
    public class Header2FileRule : AbstractFileRule, IRule<IlrFileData>
    {
        private readonly string _ruleName = "Header_2";

        public Header2FileRule(IValidationFileErrorHandler validationFileErrorHandler)
            : base(validationFileErrorHandler)
        {
        }

        public void Validate(IlrFileData headerToValidate)
        {
            if (ConditionMet(headerToValidate.Message.HeaderEntity.CollectionDetailsEntity))
            {
                HandleValidationError(_ruleName, headerToValidate.FileName, null);
            }
        }

        public bool ConditionMet(ICollectionDetails headerDetails)
        {
            return headerDetails == null ||
                   DateTime.Now.Date < headerDetails.FilePreparationDate.Date;
        }
    }
}