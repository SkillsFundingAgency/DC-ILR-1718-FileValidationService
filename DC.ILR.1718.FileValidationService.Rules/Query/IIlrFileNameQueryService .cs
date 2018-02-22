using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.ILR._1718.FileValidationService.Rules.Query
{
    public interface IIlrFileNameQueryService
    {
        string GetYearOfCollection(string fileName);
        int? GetSerialNumber(string fileName);
        DateTime? GetFileDateTime(string fileName);
        bool IsValidFileName(string fileName);

    }
}
