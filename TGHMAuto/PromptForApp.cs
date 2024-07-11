using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TGHMAuto
{
    public static class PromptForApp
    {
        private static string folderName;

        public static void Init(string folderName)
        {
            PromptForApp.folderName = folderName;
        }

        private static List<string> getFilesBat()
        {
            var allFilenames = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory).Select(p => Path.GetFileName(p));
            var candidates = allFilenames.Where(fn => Path.GetExtension(fn) == ".bat")
                                         .Select(fn => Path.GetFileNameWithoutExtension(fn));
            var files = candidates.ToList();
            return files;
        }

        public static void save(int index, string title, string username, string password)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, String.Format("{0}-{1}.bat", index, title));
            try
            {
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    writer.WriteLine(String.Format(@"cd /d {0}\element\x64", folderName));
                    writer.WriteLine(String.Format("start elementclient_64.exe startbypatcher game:cpw console:1 nocheck user:{0} pwd:{1}", username, password));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static AccountInfo readAccountInfo(string fileName)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, String.Format("{0}.bat", fileName));
            try
            {
                AccountInfo accountInfo = new AccountInfo();
                var values = fileName.Split('-');
                if(values.Length >= 2)
                {
                    accountInfo.Index = int.Parse(values[0]);
                    accountInfo.Title = fileName.Replace(String.Format("{0}-", values[0]), "");
                }
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string patternUser = @"user:([^\s]+)";
                        Match match = Regex.Match(line, patternUser);
                        if (match.Success)
                        {
                            string userValue = match.Groups[1].Value;
                            accountInfo.Username = userValue;
                        }
                        string patternPwd = @"pwd:([^\s]+)";
                        match = Regex.Match(line, patternPwd);
                        if (match.Success)
                        {
                            string pwdValue = match.Groups[1].Value;
                            accountInfo.Password = pwdValue;
                        }
                    }
                }
                return accountInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }


        public static void ShowDialogAdd(FormClosedEventHandler formClosed)
        {
            var files = getFilesBat();
            Form prompt = new Form();
            prompt.Width = 280;
            prompt.Height = 300;
            prompt.Text = "Add Account";

            Label lblTitle = new Label() { Left = 10, Top = 10, Width = 240, Text = "Title" };
            TextBox txtTitle = new TextBox() { Left = 10, Top = 35, Width = 240, TabIndex = 0, TabStop = true };
            txtTitle.BorderStyle = BorderStyle.FixedSingle;
            txtTitle.Text = "";

            Label lblUsername = new Label() { Left = 10, Top = 70, Width = 240, Text = "Username" };
            TextBox txtUsername = new TextBox() { Left = 10, Top = 95, Width = 240, TabIndex = 0, TabStop = true };
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Text = "";

            Label lblPassword = new Label() { Left = 10, Top = 130, Width = 240, Text = "Password" };
            TextBox txtPassword = new TextBox() { Left = 10, Top = 155, Width = 240, TabIndex = 0, TabStop = true, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password };
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Text = "";

            Label selLabel = new Label() { Left = 10, Top = 190, Width = 88, Text = "Option" };
            ComboBox cmbx = new ComboBox() { Left = 106, Top = 190, Width = 144 };
            cmbx.Items.Add("Dark Grey");
            cmbx.Items.Add("Orange");
            cmbx.Items.Add("None");

            Button confirmation = new Button() { Text = "Save", Left = 10, Width = 80, Top = 225, TabIndex = 1, TabStop = true };
            confirmation.Click += (sender, e) => {
                save(files.Count + 1, txtTitle.Text, txtUsername.Text, txtPassword.Text);
                prompt.Close();
            };

            Button cancel = new Button() { Text = "Cancel", Left = 175, Width = 80, Top = 225, TabIndex = 3, TabStop = true };
            cancel.Click += (sender, e) =>
            {
                prompt.Close();
            };
            prompt.FormBorderStyle = FormBorderStyle.FixedSingle;
            prompt.MaximizeBox = false;
            prompt.MinimizeBox = false;
            prompt.ShowInTaskbar = false;
            prompt.Controls.Add(lblTitle);
            prompt.Controls.Add(txtTitle);
            prompt.Controls.Add(lblUsername);
            prompt.Controls.Add(txtUsername);
            prompt.Controls.Add(lblPassword);
            prompt.Controls.Add(txtPassword);

            prompt.Controls.Add(selLabel);
            prompt.Controls.Add(cmbx);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            prompt.Controls.Add(cancel);
            prompt.CancelButton = cancel;

            prompt.StartPosition = FormStartPosition.CenterParent;
            prompt.FormClosed += formClosed;
            prompt.ShowDialog();
        }

        public static void ShowDialogUpdate(string fileName, FormClosedEventHandler formClosed)
        {
            var accountInfo = readAccountInfo(fileName);
            if (accountInfo != null)
            {
                Form prompt = new Form();
                prompt.Width = 280;
                prompt.Height = 300;
                prompt.Text = "Update Account";

                Label lblTitle = new Label() { Left = 10, Top = 10, Width = 240, Text = "Title" };
                TextBox txtTitle = new TextBox() { Left = 10, Top = 35, Width = 240, TabIndex = 0, TabStop = true };
                txtTitle.BorderStyle = BorderStyle.FixedSingle;
                txtTitle.Text = accountInfo.Title;

                Label lblUsername = new Label() { Left = 10, Top = 70, Width = 240, Text = "Username" };
                TextBox txtUsername = new TextBox() { Left = 10, Top = 95, Width = 240, TabIndex = 0, TabStop = true };
                txtUsername.BorderStyle = BorderStyle.FixedSingle;
                txtUsername.Text = accountInfo.Username;

                Label lblPassword = new Label() { Left = 10, Top = 130, Width = 240, Text = "Password" };
                TextBox txtPassword = new TextBox() { Left = 10, Top = 155, Width = 240, TabIndex = 0, TabStop = true, UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password };
                txtPassword.BorderStyle = BorderStyle.FixedSingle;
                txtPassword.Text = accountInfo.Password;

                Label selLabel = new Label() { Left = 10, Top = 190, Width = 88, Text = "Option" };
                ComboBox cmbx = new ComboBox() { Left = 106, Top = 190, Width = 144 };
                cmbx.Items.Add("Dark Grey");
                cmbx.Items.Add("Orange");
                cmbx.Items.Add("None");

                Button confirmation = new Button() { Text = "Save", Left = 10, Width = 80, Top = 225, TabIndex = 1, TabStop = true };
                confirmation.Click += (sender, e) =>
                {
                    File.Delete(String.Format(@"{0}.bat", fileName));
                    save(accountInfo.Index, txtTitle.Text, txtUsername.Text, txtPassword.Text);
                    prompt.Close();
                };
                Button btnDelete = new Button() { Text = "Delete", Left = 92, Width = 80, Top = 225, TabIndex = 2, TabStop = true };
                btnDelete.Click += (sender, e) =>
                {
                    DialogResult result = MessageBox.Show($"Xóa tài khoản [{fileName}]?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        File.Delete(String.Format(@"{0}.bat", fileName));
                        prompt.Close();
                    }
                };
                Button cancel = new Button() { Text = "Cancel", Left = 175, Width = 80, Top = 225, TabIndex = 3, TabStop = true };
                cancel.Click += (sender, e) =>
                {
                    prompt.Close();
                };

                prompt.FormBorderStyle = FormBorderStyle.FixedSingle;
                prompt.MaximizeBox = false;
                prompt.MinimizeBox = false;
                prompt.ShowInTaskbar = false;
                prompt.Controls.Add(lblTitle);
                prompt.Controls.Add(txtTitle);
                prompt.Controls.Add(lblUsername);
                prompt.Controls.Add(txtUsername);
                prompt.Controls.Add(lblPassword);
                prompt.Controls.Add(txtPassword);

                prompt.Controls.Add(selLabel);
                prompt.Controls.Add(cmbx);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;
                prompt.Controls.Add(btnDelete);
                prompt.Controls.Add(cancel);
                prompt.CancelButton = cancel;

                prompt.StartPosition = FormStartPosition.CenterParent;
                prompt.FormClosed += formClosed;
                prompt.ShowDialog();
            }
        }

    }

    public class AccountInfo
    {
        public int Index;
        public string Title;
        public string Username;
        public string Password;

        public override string ToString()
        {
            return Title;
        }
    }

}
