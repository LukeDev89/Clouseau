using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Response
{
    public class FileResponse
    {
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileBytes { get; set; }
    }
}
