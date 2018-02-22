
using ESFA.DC.ILR.Model.Interface;

namespace DC.ILR.FileValidationService.Model
{
    public class IlrFileData  
    {
        public string FileName { get; set; }
        public long? Ukprn { get; set; }
        public IMessage Message { get; set; }

    }
}