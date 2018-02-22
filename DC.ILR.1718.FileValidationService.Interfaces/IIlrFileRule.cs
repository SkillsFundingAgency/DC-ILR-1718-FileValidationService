using DC.ILR._1718.FileValidationService.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.ILR._1718.FileValidationService.Interfaces
{
    public interface IIlrFileRule
    {
        void Validate(IIlrFile fileToValidate);
    }
}
