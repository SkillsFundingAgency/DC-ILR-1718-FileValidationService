namespace DC.ILR.FileValidationService.Model.Interfaces
{
    public interface IIlrFile
    {
        string FileName { get; set; }
        long? Ukprn { get; set; }
    }
}