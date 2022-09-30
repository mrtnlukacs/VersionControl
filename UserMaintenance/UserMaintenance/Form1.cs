﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            listBox1.DataSource = users;
            listBox1.DisplayMember = "Teljesnev";
            listBox1.ValueMember = "ID";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {

                Keresztnev = textBox1.Text,
                Vezeteknev = textBox2.Text
            };
            users.Add(u);
        }
    }
}
