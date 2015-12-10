using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App.MyUsrCtrl
{
    public partial class MyUserControl : UserControl, INotifyPropertyChanged
    {
        public MyUserControl()
        {
            InitializeComponent();
        }
        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        #endregion

        private bool isRemove;
        public bool IsRemove
        {
            get { return isRemove; }
            set
            {
                if (value == isRemove) return;
                isRemove = value;
                OnPropertyChanged("Remove");
            }
        }
    }
}
