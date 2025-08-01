using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using PodiumRNGRemover.Utils;

namespace PodiumRNGRemover
{
    public partial class PodiumRNGSettings : UserControl
    {
        private Label lblTitle;
        private Label lblKey1;
        private Label lblKey2;
        private Label lblKey3;
        private Label lblKey4;
        private Label lblInfo;
        private Label lblDescription;
        private Label lblCredit;
        private TextBox txtKey1;
        private TextBox txtKey2;
        private TextBox txtKey3;
        private TextBox txtKey4;
        
        public event EventHandler SettingsChanged;
        
        public bool HookEnabled
        {
            get { return true; }
            set { }
        }
        
        public Keys Key1 
        { 
            get 
            { 
                Keys key = ParseKeyFromDisplay(txtKey1.Text);
                return key != Keys.None ? key : Constants.DefaultKeys.KEY1;
            }
            set { txtKey1.Text = GetKeyDisplayName(value); }
        }
        
        public Keys Key2 
        { 
            get 
            { 
                Keys key = ParseKeyFromDisplay(txtKey2.Text);
                return key != Keys.None ? key : Constants.DefaultKeys.KEY2;
            }
            set { txtKey2.Text = GetKeyDisplayName(value); }
        }
        
        public Keys Key3 
        { 
            get 
            { 
                Keys key = ParseKeyFromDisplay(txtKey3.Text);
                return key != Keys.None ? key : Constants.DefaultKeys.KEY3;
            }
            set { txtKey3.Text = GetKeyDisplayName(value); }
        }
        
        public Keys Key4 
        { 
            get 
            { 
                Keys key = ParseKeyFromDisplay(txtKey4.Text);
                return key != Keys.None ? key : Constants.DefaultKeys.KEY4;
            }
            set { txtKey4.Text = GetKeyDisplayName(value); }
        }
        
        public float Value1 => Constants.GOOD_PODIUM_VALUE;
        public float Value2 => Constants.MEDIUM_PODIUM_VALUE;
        public float Value3 => Constants.BAD_PODIUM_VALUE;

        public PodiumRNGSettings()
        {
            InitializeComponent();
            InitializeDefaults();
            SetupKeyCapture();
        }

        private void InitializeDefaults()
        {
            txtKey1.Text = GetKeyDisplayName(Constants.DefaultKeys.KEY1);
            txtKey2.Text = GetKeyDisplayName(Constants.DefaultKeys.KEY2);
            txtKey3.Text = GetKeyDisplayName(Constants.DefaultKeys.KEY3);
            txtKey4.Text = GetKeyDisplayName(Constants.DefaultKeys.KEY4);
        }

        private void SetupKeyCapture()
        {
            txtKey1.Click += (s, e) => StartKeyCapture(txtKey1);
            txtKey2.Click += (s, e) => StartKeyCapture(txtKey2);
            txtKey3.Click += (s, e) => StartKeyCapture(txtKey3);
            txtKey4.Click += (s, e) => StartKeyCapture(txtKey4);
            
            txtKey1.ReadOnly = true;
            txtKey2.ReadOnly = true;
            txtKey3.ReadOnly = true;
            txtKey4.ReadOnly = true;
            
            txtKey1.BackColor = SystemColors.Window;
            txtKey2.BackColor = SystemColors.Window;
            txtKey3.BackColor = SystemColors.Window;
            txtKey4.BackColor = SystemColors.Window;
            
            txtKey1.Cursor = Cursors.Hand;
            txtKey2.Cursor = Cursors.Hand;
            txtKey3.Cursor = Cursors.Hand;
            txtKey4.Cursor = Cursors.Hand;
            
        }

        private void StartKeyCapture(TextBox textBox)
        {
            using (var keyCapture = new KeyCaptureForm())
            {
                if (keyCapture.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = GetKeyDisplayName(keyCapture.CapturedKey);
                    OnSettingsChanged();
                }
            }
        }
        
        private void OnSettingsChanged()
        {
            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }


        private string GetKeyDisplayName(Keys key)
        {
            switch (key)
            {
                case Keys.D0: return "0";
                case Keys.D1: return "1";
                case Keys.D2: return "2";
                case Keys.D3: return "3";
                case Keys.D4: return "4";
                case Keys.D5: return "5";
                case Keys.D6: return "6";
                case Keys.D7: return "7";
                case Keys.D8: return "8";
                case Keys.D9: return "9";
                case Keys.NumPad0: return "Num 0";
                case Keys.NumPad1: return "Num 1";
                case Keys.NumPad2: return "Num 2";
                case Keys.NumPad3: return "Num 3";
                case Keys.NumPad4: return "Num 4";
                case Keys.NumPad5: return "Num 5";
                case Keys.NumPad6: return "Num 6";
                case Keys.NumPad7: return "Num 7";
                case Keys.NumPad8: return "Num 8";
                case Keys.NumPad9: return "Num 9";
                case Keys.Space: return "Space";
                case Keys.Enter: return "Enter";
                case Keys.Tab: return "Tab";
                case Keys.Back: return "Backspace";
                case Keys.Delete: return "Delete";
                case Keys.Insert: return "Insert";
                case Keys.Home: return "Home";
                case Keys.End: return "End";
                case Keys.PageUp: return "Page Up";
                case Keys.PageDown: return "Page Down";
                case Keys.Up: return "↑";
                case Keys.Down: return "↓";
                case Keys.Left: return "←";
                case Keys.Right: return "→";
                default:
                    if (key.HasFlag(Keys.Control) && key.HasFlag(Keys.Shift))
                    {
                        Keys baseKey = key & Keys.KeyCode;
                        return $"Ctrl+Shift+{baseKey}";
                    }
                    else if (key.HasFlag(Keys.Control))
                    {
                        Keys baseKey = key & Keys.KeyCode;
                        return $"Ctrl+{baseKey}";
                    }
                    else if (key.HasFlag(Keys.Shift))
                    {
                        Keys baseKey = key & Keys.KeyCode;
                        return $"Shift+{baseKey}";
                    }
                    else if (key.HasFlag(Keys.Alt))
                    {
                        Keys baseKey = key & Keys.KeyCode;
                        return $"Alt+{baseKey}";
                    }
                    return key.ToString();
            }
        }

        private Keys ParseKeyFromDisplay(string displayName)
        {
            switch (displayName)
            {
                case "0": return Keys.D0;
                case "1": return Keys.D1;
                case "2": return Keys.D2;
                case "3": return Keys.D3;
                case "4": return Keys.D4;
                case "5": return Keys.D5;
                case "6": return Keys.D6;
                case "7": return Keys.D7;
                case "8": return Keys.D8;
                case "9": return Keys.D9;
                case "Num 0": return Keys.NumPad0;
                case "Num 1": return Keys.NumPad1;
                case "Num 2": return Keys.NumPad2;
                case "Num 3": return Keys.NumPad3;
                case "Num 4": return Keys.NumPad4;
                case "Num 5": return Keys.NumPad5;
                case "Num 6": return Keys.NumPad6;
                case "Num 7": return Keys.NumPad7;
                case "Num 8": return Keys.NumPad8;
                case "Num 9": return Keys.NumPad9;
                case "Space": return Keys.Space;
                case "Enter": return Keys.Enter;
                case "Tab": return Keys.Tab;
                case "Backspace": return Keys.Back;
                case "Delete": return Keys.Delete;
                case "Insert": return Keys.Insert;
                case "Home": return Keys.Home;
                case "End": return Keys.End;
                case "Page Up": return Keys.PageUp;
                case "Page Down": return Keys.PageDown;
                case "↑": return Keys.Up;
                case "↓": return Keys.Down;
                case "←": return Keys.Left;
                case "→": return Keys.Right;
                default:
                    if (displayName.StartsWith("Ctrl+Shift+"))
                    {
                        string keyName = displayName.Substring(12);
                        if (Enum.TryParse(keyName, out Keys baseKey))
                            return Keys.Control | Keys.Shift | baseKey;
                    }
                    else if (displayName.StartsWith("Ctrl+"))
                    {
                        string keyName = displayName.Substring(5);
                        if (Enum.TryParse(keyName, out Keys baseKey))
                            return Keys.Control | baseKey;
                    }
                    else if (displayName.StartsWith("Shift+"))
                    {
                        string keyName = displayName.Substring(6);
                        if (Enum.TryParse(keyName, out Keys baseKey))
                            return Keys.Shift | baseKey;
                    }
                    else if (displayName.StartsWith("Alt+"))
                    {
                        string keyName = displayName.Substring(4);
                        if (Enum.TryParse(keyName, out Keys baseKey))
                            return Keys.Alt | baseKey;
                    }
                    
                    if (Enum.TryParse(displayName, out Keys result))
                        return result;
                    return Keys.None;
            }
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            
            XmlElement element;
            
            element = document.CreateElement("Key1");
            element.InnerText = Key1.ToString();
            parent.AppendChild(element);
            
            element = document.CreateElement("Key2");
            element.InnerText = Key2.ToString();
            parent.AppendChild(element);
            
            element = document.CreateElement("Key3");
            element.InnerText = Key3.ToString();
            parent.AppendChild(element);
            
            element = document.CreateElement("Key4");
            element.InnerText = Key4.ToString();
            parent.AppendChild(element);
            
            return parent;
        }

        public void SetSettings(XmlNode settings)
        {
            if (settings == null)
                return;
                
            var element = settings["Key1"];
            if (element != null)
            {
                if (Enum.TryParse(element.InnerText, out Keys key1))
                    Key1 = key1;
            }
                
            element = settings["Key2"];
            if (element != null)
            {
                if (Enum.TryParse(element.InnerText, out Keys key2))
                    Key2 = key2;
            }
                
            element = settings["Key3"];
            if (element != null)
            {
                if (Enum.TryParse(element.InnerText, out Keys key3))
                    Key3 = key3;
            }
                
            element = settings["Key4"];
            if (element != null)
            {
                if (Enum.TryParse(element.InnerText, out Keys key4))
                    Key4 = key4;
            }
        }
    }
}