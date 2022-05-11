using System.Collections.Generic;
using System.IO;

namespace IoManagDoubleTagsChek
{
    static public class TagsReader
    {
        public static List<Tags> AddTags(string[] file, string fname)
        {
            List<Tags> mytags = new List<Tags>();
            string Topic = "";
            foreach (var item in file)
            {
                if (item.Contains("Topic=") && Topic.Length == 0)
                {
                    Topic = item.Substring(6);
                }
                string[] temp = item.Split(';');
                if (temp.Length > 2)
                {
                    Tags huhu = new Tags($"{Topic}.{temp[0].Replace('"',' ').Trim()}", fname);
                    mytags.Add(huhu);
                }
            }
            return mytags;
        }

    }

    public class Tags
    {
        public string IOTag { get; private set; }
        public string FileName { get; private set; }
        public Tags(string tag, string filename)
        {
            IOTag = tag;
            FileName = filename;
        }
    }
}
