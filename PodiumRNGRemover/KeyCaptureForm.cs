using System;
using System.Drawing;
using System.Windows.Forms;

namespace PodiumRNGRemover
{
    public partial class KeyCaptureForm : Form
    {
        public Keys CapturedKey { get; private set; } = Keys.None;

        public KeyCaptureForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            this.Text = "Press a key";
            this.Size = new Size(300, 150);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            
            var label = new Label
            {
                Text = "Press any key or key combination\n(ESC to cancel)",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular)
            };
            
            this.Controls.Add(label);
            this.ResumeLayout(false);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return true;
            }
            
            Keys key = keyData & Keys.KeyCode;
            if (key != Keys.None && key != Keys.ControlKey && key != Keys.ShiftKey && key != Keys.Alt)
            {
                CapturedKey = keyData;
                this.DialogResult = DialogResult.OK;
                this.Close();
                return true;
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Activate();
            this.Focus();
        }
    }
}