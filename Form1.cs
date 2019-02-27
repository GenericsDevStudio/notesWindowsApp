using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace NotesCrossPlt
{
    public partial class Form1 : Form
    {
        public TransferPackage TransferPackage;
        private bool isEdit = false;
        public Form1()
        {
            InitializeComponent();
            button1.Visible = false;
            Start st = new Start();
            st.ShowDialog();
            TransferPackage = st.getTrPack();
            foreach (var note in TransferPackage.notes)
            {
                listBox1.Items.Add(note);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = ((Note)listBox1.Items[listBox1.SelectedIndex]).title;
            textBox2.Text = ((Note)listBox1.Items[listBox1.SelectedIndex]).content;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            isEdit = false;
            button1.Visible = true;
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            Start st = new Start();
            st.ShowDialog();

            TransferPackage = st.getTrPack();
            foreach (var note in TransferPackage.notes)
            {
                listBox1.Items.Add(note);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isEdit)
            {
                using (var client = new WebClient())
                {
                    int index = listBox1.SelectedIndex;
                    int id = ((Note)listBox1.Items[listBox1.SelectedIndex]).noteid;
                    var values = new NameValueCollection();
                    values["noteid"] = Convert.ToString(((Note)listBox1.Items[listBox1.SelectedIndex]).noteid);
                    values["title"] = textBox1.Text;
                    values["content"] = textBox2.Text;
                    var response = client.UploadValues("http://vasylko.zzz.com.ua/index.php/api/editnote", values);
                    listBox1.Items[index] = new Note() { noteid = id, content = textBox2.Text, title = textBox1.Text, lastChange = DateTime.Now};
                    /*((Note)listBox1.Items[listBox1.SelectedIndex]).title = textBox1.Text;
                    ((Note)listBox1.Items[listBox1.SelectedIndex]).content = textBox2.Text;
                    ((Note)listBox1.Items[listBox1.SelectedIndex]).lastChange = DateTime.Now;*/
                }
                isEdit = false;
                button1.Visible = false;
            }
            else
            {
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["userid"] = Convert.ToString(TransferPackage.userId);
                    values["title"] = textBox1.Text;
                    values["content"] = textBox2.Text;
                    var response = client.UploadValues("http://vasylko.zzz.com.ua/index.php/api/addnote", values);
                    var responseString = Encoding.Default.GetString(response);
                    int id = getIntegerfromQuery(responseString);
                    listBox1.Items.Add(new Note() { content = textBox2.Text, noteid = id, title = textBox1.Text, lastChange = DateTime.Now });
                    button1.Visible = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox1.Text = ((Note)listBox1.Items[listBox1.SelectedIndex]).title;
            textBox2.Text = ((Note)listBox1.Items[listBox1.SelectedIndex]).content;
            isEdit = true;
            button1.Visible = true;
        }

        private void deletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Info", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var client = new WebClient())
                {
                   if (listBox1.SelectedIndex >= 0)
                   {  
                        var values = new NameValueCollection();
                        int id = ((Note)listBox1.SelectedItem).noteid;
                        listBox1.Items.Remove(listBox1.SelectedItem);
                        listBox1.SelectedIndex = 0;
                        values["noteid"] = Convert.ToString(id);
                        var response = client.UploadValues("http://vasylko.zzz.com.ua/index.php/api/dellnote", values);
                        textBox1.Text = "";
                        textBox2.Text = "";
                   }
                }
            }
        }

        private int getIntegerfromQuery(string str)
        {
            int res = 0;
            int end = 0;
            for(int i = 0; i < str.Length; i++)
            {
                if(!Char.IsDigit(str[i]))
                {
                    end = i;
                    break;
                }
            }
            res = Convert.ToInt32(str.Substring(0, end));
            return res;
        }

    }
}
