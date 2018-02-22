namespace DC.ILR.FileValidationService.Rules.Constants
{
    public static class IlrFileConstants
    {
        public const string IlrFileNameRegex = "^(ILR)-([1-9][0-9]{7})-(1718)-((20[0-9]{2})(0[1-9]|1[012])([123]0|[012][1-9]|31))-(([01][0-9]|2[0-3])([0-5][0-9])([0-5][0-9]))-(([1-9][0-9])|(0[1-9])).((XML)|(ZIP)|(xml)|(zip))$";
        public const string AcademicYear = "1718";
    }
}