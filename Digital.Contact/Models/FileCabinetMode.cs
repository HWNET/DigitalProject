using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.Models
{
    public class FileFolderMode
    {
        public string FolderName { get; set; }
        public string FolderPath { get; set; }
        public long FolderSize { get; set; }
        public DateTime FolderDate { get; set; }
        public List<FilesMode> FileList { get; set; }
    }
    public class FilesMode
    {
        public string FolderName { get; set; }
        public string FileName { get; set; }
        public string FileExtensionName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public DateTime FileDate { get; set; }
    }
}
