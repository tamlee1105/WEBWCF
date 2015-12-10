using System;
using System.IO;
using System.Windows.Forms;

namespace App
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            //check in test
            var frm = new FormCategoryManager();
            frm.Show();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            var frm = new FormProductManager();
            frm.Show();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            var frm = new FormAppConfig();
            frm.ShowDialog();
        }

        /// <summary>
        /// method is get an address of service for loading image picture product
        /// </summary>
        private void GetAddreeService()
        {
            if (!File.Exists(Utilities.fileConfig)) return;
            using (var streamReader = File.OpenText(Utilities.fileConfig))
            {
                Utilities.Path = streamReader.ReadLine();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            GetAddreeService();
        }
    }
}
