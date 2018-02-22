using DC.ILR.FileValidationService.Model.Interfaces;

namespace DC.ILR.FileValidationService.Interfaces
{
    public interface IIlrFileRule
    {
        void Validate(IIlrFile fileToValidate);
    }
}
