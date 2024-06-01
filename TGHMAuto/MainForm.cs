using SettingConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowNameChanger;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TGHMAuto
{
    public partial class MainForm : Form
    {

        private FolderBrowserDialog folderBrowserDialog;
        private string folderName;
        private ListViewItem itemSelected;


        public delegate void Callback();

        public MainForm()
        {
            InitializeComponent();
            this.folderBrowserDialog = new FolderBrowserDialog();
            this.folderBrowserDialog.Description =  "Select the directory that you want to use as the default.";
            this.folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

            lvAccount.View = View.Details;
            lvAccount.CheckBoxes = true;
            lvAccount.FullRowSelect = true;
            lvAccount.GridLines = true;
            lvAccount.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            lvAccount.MouseDoubleClick += LvAccount_MouseDoubleClick;
            lvAccount.ItemSelectionChanged += LvAccount_ItemSelectionChanged; ;

            displayListView();

            AppConfig config = SettingConfig.SettingConfig.LoadConfig();
            if(config != null && !string.IsNullOrEmpty(config.PathDir))
            {
                folderName = config.PathDir;
                txtPathFolder.Text = folderName;
                lvAccount.Enabled = true;
                btnAdd.Enabled = true;
                PromptForApp.Init(folderName);
            }
        }

        private void LvAccount_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            itemSelected = e.IsSelected ? e.Item : null;
            btnUpdate.Enabled = (itemSelected != null);
        }


        private void LvAccount_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(folderName))
            {
                ListViewHitTestInfo info = lvAccount.HitTest(e.X, e.Y);
                ListViewItem item = info.Item;
                if (item != null && item.Checked)
                {
                    //MessageBox.Show("You double-clicked on: " + item.Text);
                    string exePath = String.Format(@"{0}\{1}.bat", AppDomain.CurrentDomain.BaseDirectory, item.Text);
                    Process.Start(exePath);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    WindowListItem itemWindow = WindowNameChanger.WindowNameChanger.getWindow();
                    WindowNameChanger.WindowNameChanger.SetWindowText(itemWindow.HWnd, item.Text);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    itemWindow = WindowNameChanger.WindowNameChanger.getWindow();
                    WindowNameChanger.WindowNameChanger.SetWindowText(itemWindow.HWnd, item.Text);
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    itemWindow = WindowNameChanger.WindowNameChanger.getWindow();
                    WindowNameChanger.WindowNameChanger.SetWindowText(itemWindow.HWnd, item.Text);
                }
            }
        }

        private void displayListView()
        {
            lvAccount.Items.Clear();
            var files = getFilesBat();
            for (int i = 0; i < files.Count; i++)
            {
                lvAccount.Items.Add(new ListViewItem(new string[] { files[i], "1", "100" }));
            }
        }

        private List<string> getFilesBat()
        {
            var allFilenames = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory).Select(p => Path.GetFileName(p));
            var candidates = allFilenames.Where(fn => Path.GetExtension(fn) == ".bat")
                                         .Select(fn => Path.GetFileNameWithoutExtension(fn));
            var files = candidates.ToList();
            return files;
        }

        private void btnSelectedFolder_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
                {
                    folderName = folderBrowserDialog.SelectedPath;
                    txtPathFolder.Text = folderName;
                    lvAccount.Enabled = true;
                    btnAdd.Enabled = true;
                    PromptForApp.Init(folderName);
                    SettingConfig.SettingConfig.SaveConfig(new SettingConfig.AppConfig { PathDir = folderName });
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PromptForApp.ShowDialogAdd(DialogAdd_FormClosed);
        }

        private void DialogAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            displayListView();
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            PromptForApp.ShowDialogUpdate(itemSelected.Text, DialogAdd_FormClosed);
        }
    }
}
