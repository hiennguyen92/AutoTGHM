using POJO;
using SettingConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowNameChanger;

namespace TGHMAuto
{
    public partial class MainForm : Form
    {
        private Timer timer;
        private FolderBrowserDialog folderBrowserDialog;
        private string folderName;

        private BindingList<Account> accounts = new BindingList<Account>();
        private string accountSelected;

        PopedContainer popedContainer;
        PoperContainer poperContainer;


        public delegate void Callback();

        public MainForm()
        {
            InitializeComponent();
            this.folderBrowserDialog = new FolderBrowserDialog();
            this.folderBrowserDialog.Description =  "Select the directory that you want to use as the default.";
            this.folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            Size formSize = this.Size;
            int x = screen.Width - formSize.Width;
            int y = (screen.Height - formSize.Height) / 2;
            this.Location = new Point(x, y);

            popedContainer = new PopedContainer(this);
            poperContainer = new PoperContainer(popedContainer);

            DisplayListView();

            AppConfig config = SettingConfig.SettingConfig.LoadConfig();
            if(config != null && !string.IsNullOrEmpty(config.PathDir))
            {
                folderName = config.PathDir;
                txtPathFolder.Text = folderName;
                dataGridViewAccount.Enabled = true;
                btnAdd.Enabled = true;
                PromptForApp.Init(folderName);
            }

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick; ;
            timer.Start();
        }

        private void DisplayListView()
        {
            accounts.Clear();
            var files = getFilesBat();
            for (int i = 0; i < files.Count; i++)
            {
                accounts.Add(new Account { File = files[i], Status = "", Name = "" });
            }

            dataGridViewAccount.DataSource = accounts;
            if (dataGridViewAccount.Columns.Count < 4)
            {
                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                {
                    Name = "Action",
                    HeaderText = "Action",
                    Text = "➤",
                    UseColumnTextForButtonValue = true
                };
                dataGridViewAccount.Columns.Add(buttonColumn);
                
                dataGridViewAccount.Columns[0].Width = 120;
                dataGridViewAccount.Columns[1].Width = 30;
                dataGridViewAccount.Columns[2].Width = 80;
                dataGridViewAccount.Columns[3].Width = 37;
                
                dataGridViewAccount.CellDoubleClick += DataGridViewAccount_CellDoubleClick;
                dataGridViewAccount.CellStateChanged += DataGridViewAccount_CellStateChanged;
                dataGridViewAccount.SelectionChanged += DataGridViewAccount_SelectionChanged;
                dataGridViewAccount.CellContentClick += DataGridViewAccount_CellContentClick;
                dataGridViewAccount.CellPainting += DataGridViewAccount_CellPainting;
            }
            
            
        }

        private void DataGridViewAccount_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridViewAccount.SelectedCells)
            {
                if (dataGridViewAccount.Columns[cell.ColumnIndex].Name == "Name" || dataGridViewAccount.Columns[cell.ColumnIndex].Name == "Action")
                {
                    cell.Selected = false;
                }
                else
                {
                    object fileValue = dataGridViewAccount.Rows[cell.RowIndex].Cells[1].Value;
                    accountSelected = fileValue.ToString();
                    btnUpdate.Enabled = cell.Selected;
                }
            }
        }

        private void DataGridViewAccount_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (dataGridViewAccount.Columns[e.Cell.ColumnIndex].Name == "Name" || dataGridViewAccount.Columns[e.Cell.ColumnIndex].Name == "Action")
            {
                e.Cell.Selected = false;
            }
        }

        private void DataGridViewAccount_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewAccount.Columns[e.ColumnIndex].Name == "File")
            {
                object cellValue = dataGridViewAccount.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                Task.Run(async () =>
                {
                    string exePath = String.Format(@"{0}\{1}.bat", AppDomain.CurrentDomain.BaseDirectory, cellValue.ToString());
                    Process.Start(exePath);
                    await Task.Delay(1000);
                    while (true)
                    {
                        WindowInfo windowInfo = WindowNameChanger.WindowNameChanger.GetWindowInfo("The GioiHoan My");
                        if(windowInfo != null)
                        {
                            WindowNameChanger.WindowNameChanger.ChangeWindowTitle("The GioiHoan My", cellValue.ToString());
                            break;
                        }
                        await Task.Delay(1000);
                    }
                });
            }
            if (dataGridViewAccount.Columns[e.ColumnIndex].Name == "Status")
            {
                object fileValue = dataGridViewAccount.Rows[e.RowIndex].Cells[1].Value;
                
                DialogResult result = MessageBox.Show($"Cập nhật lại tiêu đề [{fileValue}]?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    WindowNameChanger.WindowNameChanger.ChangeWindowTitle(fileValue.ToString(), fileValue.ToString());
                }
            }
        }

        private void DataGridViewAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewAccount.Columns[e.ColumnIndex].Name == "Action")
            {
                object fileValue = dataGridViewAccount.Rows[e.RowIndex].Cells[1].Value;
                var account = accounts.Where(x => x.File == fileValue.ToString()).FirstOrDefault();
                if(account.HWnd != IntPtr.Zero)
                {
                    popedContainer.SetAccount(account);
                    poperContainer.Show(dataGridViewAccount, new Point(Width - (popedContainer.Width + 45) , dataGridViewAccount.Rows[e.RowIndex].Height * (e.RowIndex + 1)));
                }

            }
        }

        private void DataGridViewAccount_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //Disable button Action
            if (dataGridViewAccount.Columns[e.ColumnIndex].Name == "Action")
            {
                object fileValue = dataGridViewAccount.Rows[e.RowIndex].Cells[1].Value;
                var account = accounts.Where(x => x.File == fileValue.ToString()).FirstOrDefault();
                if (account.HWnd == IntPtr.Zero)
                {
                    e.PaintBackground(e.CellBounds, true);
                    ButtonRenderer.DrawButton(e.Graphics, e.CellBounds, System.Windows.Forms.VisualStyles.PushButtonState.Disabled);
                    TextRenderer.DrawText(e.Graphics, "➤", e.CellStyle.Font, e.CellBounds, SystemColors.GrayText, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    e.Handled = true;
                }
            }
        }


        private List<string> getFilesBat()
        {
            var allFilenames = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory).Select(p => Path.GetFileName(p));
            var candidates = allFilenames.Where(fn => Path.GetExtension(fn) == ".bat")
                                         .Select(fn => Path.GetFileNameWithoutExtension(fn));


            var files = candidates.ToList();
            files.Sort(new AlphaNumericComparer());
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
                    dataGridViewAccount.Enabled = true;
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
            DisplayListView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            PromptForApp.ShowDialogUpdate(accountSelected, DialogAdd_FormClosed);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                var account = accounts[i];
                WindowInfo windowInfo = WindowNameChanger.WindowNameChanger.GetWindowInfo(account.File);
                if (windowInfo != null) 
                {
                    account.HWnd = windowInfo.HWnd;
                    account.Status = "⚡";
                    //Get Account Name
                    account.Name = string.Empty;
                    account.IsHP = true;
                }
                else
                {
                    account.HWnd = IntPtr.Zero;
                    account.Status = string.Empty;
                    account.Name = string.Empty;
                }
            }
        }
    }
}
