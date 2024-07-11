using System;
using System.ComponentModel;

namespace POJO
{
    public class Account : INotifyPropertyChanged
    {
        private string file;
        private string status;
        private string name;

        [Browsable(false)]
        public IntPtr HWnd { get; set; }

        [Browsable(false)]
        public bool IsHP { get; set; }


        [Browsable(false)]
        public int PercentHP { get; set; }

        [Browsable(false)]
        public string KeyHP { get; set; }


        [Browsable(false)]
        public bool IsMP { get; set; }

        [Browsable(false)]
        public int PercentMP { get; set; }

        [Browsable(false)]
        public string KeyMP { get; set; }


        [Browsable(false)]
        public bool IsFollowingKey { get; set; }

        [Browsable(false)]
        public bool IsJumpFollowing { get; set; }



        public string File
        {
            get => file;
            set
            {
                if (file != value)
                {
                    file = value;
                    OnPropertyChanged("File");
                }
            }
        }
        public string Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
