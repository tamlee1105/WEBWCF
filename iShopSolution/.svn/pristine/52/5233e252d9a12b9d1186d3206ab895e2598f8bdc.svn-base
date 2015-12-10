using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App
{
    public partial class FormAppConfig : Form
    {
        public FormAppConfig()
        {
            InitializeComponent();
        }
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                txtAddress.Text = folderBrowser.SelectedPath;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Utilities.Path = txtAddress.Text;

            try
            {
                if (File.Exists(Utilities.fileConfig))
                    File.Delete(Utilities.fileConfig);

                using (var streamWriter = new StreamWriter(new FileStream(Utilities.fileConfig, FileMode.Create)))
                {
                    streamWriter.WriteLine(Utilities.Path);
                }

            }
            catch (Exception exception)
            {
                Utilities.ShowMessageError("Error: " + exception.Message);
                throw;
            }

            Close();
        }

        private void FormAppConfig_Load(object sender, EventArgs e)
        {
            if (File.Exists(Utilities.fileConfig))
            {
                using (var streamReader = File.OpenText(Utilities.fileConfig))
                {
                    Utilities.Path = streamReader.ReadLine();
                }
            }
            txtAddress.Text = Utilities.Path;
        }
    }
}
