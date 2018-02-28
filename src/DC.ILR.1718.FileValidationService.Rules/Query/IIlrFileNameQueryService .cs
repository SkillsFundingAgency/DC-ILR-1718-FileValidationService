using System;

namespace DC.ILR.FileValidationService.Rules.Query
{
    public interface IIlrFileNameQueryService
    {
        string GetYearOfCollection(string fileName);

        string GetSerialNumber(string fileName);

        DateTime? GetFileDateTime(string fileName);

        bool IsValidFileName(string fileName);

        long GetUkprn(string fileName);

        string GetFilePart(string fileName, int index);
    }
}