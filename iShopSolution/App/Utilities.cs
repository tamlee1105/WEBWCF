// Copyright: 2012 
// Author: Minh Vu - YoungJ
// File name: Utils.cs
// Solution: iShopSolution
// Project: App
// Time: 9:58 AM 23/04/2012

using System.Windows.Forms;

namespace App
{
    public class Utilities
    {

        /*  path to service folder
         *  It important to identify path of image to application
         */

        public const string fileConfig = "config.ishop";

        private static string _path;
        public static string Path
        {

            get
            {
                return string.IsNullOrEmpty(_path) ? @"../../../Service" : _path;
            }

            set { _path = value; }
        }


        public static void ShowMessage(string msg)
        {
            MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ShowMessageError(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static DialogResult ShowQuestionMessage(string msg)
        {
            return MessageBox.Show(msg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
        }
    }
}