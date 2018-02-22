using ESFA.DC.ILR.Model.Interface;

namespace DC.ILR.FileValidationService.FileProcessor
{
    public interface IFileProcessor
    {
        IMessage ParseXMLFile(string fileContent);
    }
}