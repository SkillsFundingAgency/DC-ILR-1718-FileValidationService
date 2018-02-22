using ESFA.DC.ILR.Model.Interface;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DC.ILR.FileValidationService.FileProcessor
{
    public class FileProcessor : IFileProcessor
    {
        public IMessage ParseXMLFile(string fileContent)
        {
            IMessage message;

            using (var reader = XmlReader.Create(new StringReader(fileContent)))
            {
                var serializer = new XmlSerializer(typeof(IMessage));
                message = serializer.Deserialize(reader) as IMessage;
            }

            return message;
        }

        public void StoreJSON(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}