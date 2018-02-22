using System;
using System.Text.RegularExpressions;
using DC.ILR.FileValidationService.Rules.Constants;

namespace DC.ILR.FileValidationService.Rules.Query
{
    public class IlrFileNameQueryService : IIlrFileNameQueryService
    {
        private readonly Regex _fileNameRegex = new Regex(IlrFileConstants.IlrFileNameRegex, RegexOptions.Compiled);
        
        public DateTime? GetFileDateTime(string fileName)
        {
            throw new NotImplementedException();
        }

        public int? GetSerialNumber(string fileName)
        {
            throw new NotImplementedException();
        }

        public string GetYearOfCollection(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || !_fileNameRegex.IsMatch(fileName))
            {
                return null;
            }

            var fileParts = fileName.Split('-');
            if (fileParts.Length>2)
            {
                return fileParts[2];
            }

            return null;
        }

        public bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) )
            {
                return false;
            }
            return _fileNameRegex.IsMatch(fileName);
        }
    }
}
