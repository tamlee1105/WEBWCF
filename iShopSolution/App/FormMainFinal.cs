using System;
using System.Linq;
using System.Windows.Forms;
using App.MyUsrCtrl;
using Business.Entity;

namespace App
{
    public partial class FormMainFinal : Form
    {
        public FormMainFinal()
        {
            InitializeComponent();
        }
        #region AddMyUserControl
        private void AddMyUserControl(string title, MyUserControl uc)
        {
            if (IsAddTab(title)) return;
            var tab = new TabPage(title);
            uc.Dock = DockStyle.Fill;
            tab.Controls.Add(uc);
            myTabControl.TabPages.Add(tab);
            myTabControl.SelectTab(tab);
            uc.PropertyChanged += (sender, e) =>
            {
                if (!e.PropertyName.Equals("Remove") || !uc.IsRemove) return;
                myTabControl.TabPages.Remove(tab);
                myTabControl.SelectedIndex = myTabControl.TabPages.Count - 1;
            };
        }
        private bool IsAddTab(string titleTab)
        {
            foreach (var tabPage in myTabControl.TabPages.Cast<TabPage>().Where(tabPage => tabPage.Text.Equals(titleTab)))
            {
                myTabControl.SelectTab(tabPage);
                return true;
            }
            return false;
        }
        #endregion

        private void configToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //AddMyUserControl("System Config", new UsrCtrlAppConfig());
            var frm = new FormAppConfig();
            frm.ShowDialog();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Close();
            Application.Exit();
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMyUserControl("Category Manager", new UsrCtrlCategoryManager());
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var uc = new UsrCtrlProductManager { Dock = DockStyle.Fill };
            uc.PropertyChanged += (sender1, e1) =>
                                      {
                                          switch (e1.PropertyName)
                                          {
                                              case "AddProduct":
                                                  var ucAdd = new UsrCtrlDetailsProduct();
                                                  ucAdd.PropertyChanged += (sender2, e2) =>
                                                                               {
                                                                                   if (e2.PropertyName.Equals("Remove"))
                                                                                       uc.RefreshList();
                                                                               };
                                                  AddMyUserControl("Add product", ucAdd);
                                                  break;
                                              case "SelectedProduct":
                                                  var item = uc.SelectedItem;
                                                  if (item == null) return;
                                                  var ucEdit = new UsrCtrlDetailsProduct(item);
                                                  AddMyUserControl("Edit " + item.Name, ucEdit);
                                                  break;

                                          }
                                      };
            AddMyUserControl("Product Manager", uc);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var index = myTabControl.SelectedIndex;
            myTabControl.TabPages.RemoveAt(index);
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myTabControl.TabPages.Clear();
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var index = myTabControl.SelectedIndex;
            for (var i = myTabControl.TabPages.Count - 1; i > -1; i--)
            {
                if (i == index) continue;
                myTabControl.TabPages.RemoveAt(i);
            }
        }


        private void deliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMyUserControl("Delivery Note Manager", new UsrCtrlManagerDeliveryNote());
        }

        private void receiptNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var uc = new UsrCtrlManagerReceiptNote();
            uc.PropertyChanged += (s1, e1) =>
            {
                switch (e1.PropertyName)
                {
                    case "SelectedItem":

                        var ucAdd = uc.SelectedItem == null
                                        ? new UsrCtrlAddReceiptNote(new ReceiptNote())
                                        : new UsrCtrlAddReceiptNote(uc.SelectedItem);

                        ucAdd.PropertyChanged += (s2, e2) =>
                        {
                            if (e2.PropertyName.Equals("Remove"))
                            {
                                uc.Reload();
                            }
                        };
                        AddMyUserControl(
                            uc.SelectedItem == null
                                ? "Add Receipt Note"
                                : "Update Receipt Note", ucAdd);
                        break;
                }
            };
            AddMyUserControl("Receipt Note Manager", uc);
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMyUserControl("Statistics",new UsrCtrlStatistics());
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddMyUserControl("Report",new UsrCtrlReport());
        }


    }
}
