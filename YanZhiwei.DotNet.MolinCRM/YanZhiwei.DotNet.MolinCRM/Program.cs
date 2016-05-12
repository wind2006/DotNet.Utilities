using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using Molin_CRM.Helper;
using System;
using System.Windows.Forms;
using YanZhiwei.DotNet2.Utilities.WinForm;

namespace Molin_CRM
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ApplicationHelper.ApplyOnlyOneInstance(c =>
            {
                if (!c)
                {
                    DevMessageBoxHelper.ShowError("Molin-CRM客户端已经运行存着！");
                }
            });

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            Application.Run(new winMain());
        }
    }
}