using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoManagDoubleTagsChek
{
    class myFiles
    {
        public List<myFile> Files { get; set; } = new List<myFile>();

        public void AddFileToList(string path)
        {
            FileInfo fi = new FileInfo(path);
            myFile temp = new myFile();

            temp.FileName = fi.Name;
            temp.FilePath = fi.FullName;
            Files.Add(temp);

        }
        public void ClearItems()
        {
            Files.Clear();
        }

        public class myFile
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }

        }
    }
}
