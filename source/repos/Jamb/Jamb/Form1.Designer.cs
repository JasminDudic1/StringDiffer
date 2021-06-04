namespace Jamb
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "dice1.bmp");
            this.imageList1.Images.SetKeyName(1, "dice1.bmp");
            this.imageList1.Images.SetKeyName(2, "dice2.bmp");
            this.imageList1.Images.SetKeyName(3, "dice3.bmp");
            this.imageList1.Images.SetKeyName(4, "dice4.bmp");
            this.imageList1.Images.SetKeyName(5, "dice5.bmp");
            this.imageList1.Images.SetKeyName(6, "dice6.bmp");
            this.imageList1.Images.SetKeyName(7, "dice1.bmp");
            this.imageList1.Images.SetKeyName(8, "dice2.bmp");
            this.imageList1.Images.SetKeyName(9, "dice3.bmp");
            this.imageList1.Images.SetKeyName(10, "dice4.bmp");
            this.imageList1.Images.SetKeyName(11, "dice5.bmp");
            this.imageList1.Images.SetKeyName(12, "dice6.bmp");
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1240, 587);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
    }
}

