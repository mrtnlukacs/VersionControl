using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.TeljesNev;
            
            button1.Text = Resource1.Add;
            button2.Text = Resource1.Fajlbatoltes;

            listBox1.DataSource = users;
            listBox1.DisplayMember = "Teljesnev";
            listBox1.ValueMember = "ID";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {

                Teljesev = textBox1.Text
            };
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                using (StreamWriter w = new StreamWriter(sfd.FileName,false, Encoding.Default ))
                {
                    foreach (var u in users)
                    {
                        w.Write(u.ID.ToString());
                        w.Write(";");
                        w.Write(u.Teljesev);
                        w.WriteLine();
                    }
                }
            }
        }
    }
}
