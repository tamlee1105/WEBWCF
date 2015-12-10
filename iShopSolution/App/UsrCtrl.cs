using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace App
{
    public partial class UsrCtrl : UserControl
    {
        public UsrCtrl()
        {
            InitializeComponent();
        }

        public string A
        {
            get { return txtA.Text; }
            set { txtA.Text = value; }
        }

        public string B
        {
            get { return txtB.Text; }
            set { txtB.Text = value; }
        }


        public string Value
        {
            get
            {
                if (string.IsNullOrEmpty(A) || string.IsNullOrEmpty(B))
                   return string.Empty;
                var str = String.Format(@"<tr><td class='description'>{0}</td><td class='info'>{1}</td></tr>", A, B);
                return str;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                
                var str = Regex.Replace(value, "<tr>", string.Empty);
                var regex = new Regex("</td>");
                var arrStr = regex.Split(str);
                
                txtA.Text = Regex.Replace(arrStr[0], "<[^>]+>", string.Empty);
                txtB.Text = Regex.Replace(arrStr[1], "<[^>]+>", string.Empty);

            }
        }
    }
}
