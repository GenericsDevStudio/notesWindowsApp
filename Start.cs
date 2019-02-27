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
    public partial class Start : Form
    {
        private TransferPackage transferPackage;
        public Start()
        {
            InitializeComponent();
            textBox3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!textBox3.Visible)
            {
                using (var client = new WebClient())
                {
                    try
                    {
                        var values = new NameValueCollection();
                        values["login"] = textBox1.Text;
                        values["password"] = textBox2.Text;
                        var response = client.UploadValues("http://vasylko.zzz.com.ua/index.php/api/fd", values);
                        var responseString = Encoding.Default.GetString(response);
                        transferPackage = JsonConvert.DeserializeObject<TransferPackage>(responseString);
                        Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Wrong email or password", "Warning");
                        textBox2.Text = "";
                    }      
                }

            }
            else
            {
                textBox3.Visible = false;
            }
            
        }

        public TransferPackage getTrPack()
        {
            return this.transferPackage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox3.Visible)
            {
                if(textBox2.Text != textBox3.Text)
                {
                    MessageBox.Show("Make your password the same!", "Info");
                    textBox2.Text = "";
                    textBox3.Text = "";
                    return;
                }
                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["login"] = textBox1.Text;
                    values["password"] = textBox2.Text;

                    var response = client.UploadValues("http://vasylko.zzz.com.ua/index.php/api/adduser", values);
                    var responseEnter = client.UploadValues("http://vasylko.zzz.com.ua/index.php/api/fd", values);
                    var responseString = Encoding.Default.GetString(responseEnter);
                    transferPackage = JsonConvert.DeserializeObject<TransferPackage>(responseString);
                    Close();
                }
            }
            else
            {
                textBox3.Visible = true;
            }
        }
    }
}
