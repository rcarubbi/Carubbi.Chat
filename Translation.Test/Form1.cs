using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Carubbi.Google.Translation;

namespace Translation.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = CultureInfo.GetCultures(CultureTypes.AllCultures);
            comboBox2.DataSource = CultureInfo.GetCultures(CultureTypes.AllCultures);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = TranslationHelper.Translate(textBox1.Text, new CultureInfo("pt-BR"), new CultureInfo("en-US"));

        }
    }
}
