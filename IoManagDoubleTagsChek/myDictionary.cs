using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IoManagDoubleTagsChek
{
    internal class myDictionary
    {

        //public List<Tags> TagsOK { get; set; } = new List<Tags>();
        //public List<Tags> TagsDuplicated { get; set; } = new List<Tags>();
        public IDictionary<string, string> TagsOK { get; set; } = new Dictionary<string, string>();
        public IDictionary<string, string> TagsDuplicated { get; set; } = new Dictionary<string, string>();
        public int AllTags { get; set; }
        public void BuildDictionarry(string filepath)
        {

            string[] temp = filepath.Split('\\');
            string filename = temp[temp.Length - 1];
            string[] lines = File.ReadAllLines(filepath, Encoding.UTF8);
            List<Tags> aa = TagsReader.AddTags(lines, filename);
            AddToDick(aa);
        }

        public void CreadDictionarry()
        {
            TagsOK.Clear();
            TagsDuplicated.Clear();
            AllTags = 0;
        }

        private void AddToDick(List<Tags> mytags)
        {
            foreach (var item in mytags)
            {
                // Tags temp = new Tags(item.IOTag, item.FileName);
                AllTags = AllTags + 1;
                if (!TagsOK.ContainsKey(item.IOTag))
                {
                    TagsOK.Add(item.IOTag, item.FileName);
                }
                else
                {
                    if (!TagsDuplicated.ContainsKey(item.IOTag))
                    {
                        TagsDuplicated.Add(item.IOTag, item.FileName);
                    }
                }
            }

        }
    }
}