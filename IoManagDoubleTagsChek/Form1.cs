using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IoManagDoubleTagsChek
{
    public partial class Form1 : Form
    {
        private myDictionary mytagslist = new myDictionary();
        private myFiles myfiles = new myFiles();
        public Form1()
        {
            InitializeComponent();
            //dataGridView2.Columns.Add("Tag", "Tag");
            //dataGridView2.Columns.Add("FileName", "FileName");
        }
        private void btSelectFiles_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (string filee in openFileDialog1.FileNames)
                {
                    myfiles.AddFileToList(filee);
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = myfiles.Files;
                dataGridView1.Refresh();
            }
        }

        private void bt_clearfiles_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            myfiles.ClearItems();
        }

        private void bcCheck_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                toolStripProgressBar1.Maximum = myfiles.Files.Count - 1;
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Calculation in progress....");
            }

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            mytagslist.CreadDictionarry();

            for (int i = 0; i < myfiles.Files.Count; i++)
            {
                mytagslist.BuildDictionarry(myfiles.Files[i].FilePath);
                backgroundWorker1.ReportProgress(i);
            }
        }



        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            toolStripProgressBar1.Value = e.ProgressPercentage;
            // Set the text.
           // this.Text = e.ProgressPercentage.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            toolStripStatusLabel2.Text = mytagslist.TagsDuplicated.Count.ToString(); //dataGridView1.Rows[rowindex].Cells["FilePath"].Value.ToString();
            toolStripStatusLabel6.Text = mytagslist.TagsOK.Count.ToString(); //dataGridView1.Rows[rowindex].Cells["FilePath"].Value.ToString();
            toolStripStatusLabel4.Text = mytagslist.AllTags.ToString();

            if (mytagslist.TagsDuplicated.Count < 3000)
            {

                var takietam = from row in mytagslist.TagsDuplicated select new { Tag = row.Key, FileName = row.Value };

                //foreach (KeyValuePair<string, string> item in mytagslist.TagsDuplicated)
                //{
                //    dataGridView2.Rows.Add(item.Key, item.Value);
                //}
                //dataGridView2.DataSource = mytagslist.TagsDuplicated;
                dataGridView2.DataSource = takietam.ToArray();
            }
            else
            {
                MessageBox.Show($"Found: {mytagslist.TagsDuplicated.Count} more than 3000 tags, can't be displayed. please save it to txt file");
            }

        }

        private void bt_write_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string name = saveFileDialog1.FileName;

            using (StreamWriter writer = new StreamWriter(name))
            {
                foreach (KeyValuePair<string, string> item in mytagslist.TagsDuplicated)
                {
                    writer.WriteLine($"Tag: {item.Key} ;File: {item.Value}");
                }

            }
        }
    }
}
