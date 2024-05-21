using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class FileNullReference : Exception
    {
        public FileNullReference(string? message ):base(message) { }
        
    }
}
