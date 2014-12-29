using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital.Contact.Models
{
    public enum FilesType
    {
        document,
        audio,
        image,
        video
    }
    public class FileFolderMode
    {
        public string FolderName { get; set; }
        public string FolderPath { get; set; }
        public string FolderSize { get; set; }
        public string FolderDate { get; set; }
    }
    public class FilesMode
    {
        public string FolderParent { get; set; }
        public string FolderName { get; set; }
        public string FileName { get; set; }
        public string FileExtensionName { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public string FileDate { get; set; }
    }
}
