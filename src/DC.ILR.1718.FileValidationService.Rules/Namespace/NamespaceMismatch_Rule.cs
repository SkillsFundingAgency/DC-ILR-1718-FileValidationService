using DC.ILR.FileValidationService.Interfaces;
using DC.ILR.FileValidationService.Rules.AbstractRules;
using System.Text.RegularExpressions;
using DC.ILR.FileValidationService.Rules.Constants;

namespace DC.ILR.FileValidationService.Rules.Namespace
{
    public class NamespaceMismatch_Rule : AbstractRule, IRule<string>
    {
        private readonly string _ruleName = "NamespaceMismatch";
        private readonly Regex _nameSpaceRegex = new Regex(IlrFileConstants.NamespaceRegex, RegexOptions.Compiled);

        public NamespaceMismatch_Rule(IValidationErrorHandler validationErrorHandler)
            : base(validationErrorHandler)
        {
        }

        public void Validate(string fileContent)
        {
            if (ConditionMet(fileContent))
            {
                HandleValidationError(_ruleName, null);
            }
        }

        public bool ConditionMet(string fileContent)
        {
            return string.IsNullOrWhiteSpace(fileContent) ||
                    !_nameSpaceRegex.IsMatch(fileContent);
        }
    }
}