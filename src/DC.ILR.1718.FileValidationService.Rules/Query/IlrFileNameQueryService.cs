using DC.ILR.FileValidationService.Rules.Constants;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DC.ILR.FileValidationService.Rules.Query
{
    public class IlrFileNameQueryService : IIlrFileNameQueryService
    {
        private readonly Regex _fileNameRegex = new Regex(IlrFileConstants.IlrFileNameRegex, RegexOptions.Compiled);
        private readonly int _yearOfCollectionIndex = 2;
        private readonly int _serialNumberIndex = 5;
        private readonly int _ukprnIndex = 1;
        private readonly int _fileDateIndex = 3;
        private readonly int _fileTimeIndex = 4;
        private readonly string _fileDateTimeFormat = "yyyyMMddHHmmss";

        public DateTime? GetFileDateTime(string fileName)
        {
            var fileDatePart = GetFilePart(fileName, _fileDateIndex);
            var fileTimePart = GetFilePart(fileName, _fileTimeIndex);
            if (String.IsNullOrEmpty(fileTimePart) || String.IsNullOrEmpty(fileDatePart))
            {
                return null;
            }

            if (DateTime.TryParseExact($"{fileDatePart}{fileTimePart}", _fileDateTimeFormat, null, DateTimeStyles.None, out var filePrepDateTime))
            {
                return filePrepDateTime;
            }

            return null;
        }

        public string GetSerialNumber(string fileName)
        {
            return GetFilePart(fileName, _serialNumberIndex);
        }

        public string GetYearOfCollection(string fileName)
        {
            return GetFilePart(fileName, _yearOfCollectionIndex);
        }

        public long GetUkprn(string fileName)
        {
            var result = GetFilePart(fileName, _ukprnIndex);
            return result == null ? 0 : long.Parse(result);
        }

        public bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }
            return _fileNameRegex.IsMatch(fileName);
        }

        public string GetFilePart(string fileName, int index)
        {
            if (!IsValidFileName(fileName))
            {
                return null;
            }

            fileName = fileName.Substring(0, fileName.Length - 4);
            var fileParts = fileName.Split('-');
            if (fileParts.Length > index)
            {
                return fileParts[index];
            }

            return null;
        }
    }
}