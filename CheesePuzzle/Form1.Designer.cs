namespace CheesePuzzle
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mono1 = new Mono();
            button1 = new Button();
            SuspendLayout();
            // 
            // mono1
            // 
            mono1.Location = new Point(12, 12);
            mono1.MouseHoverUpdatesOnly = false;
            mono1.Name = "mono1";
            mono1.Size = new Size(776, 710);
            mono1.TabIndex = 0;
            mono1.Text = "mono1";
            // 
            // button1
            // 
            button1.Location = new Point(796, 260);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1014, 734);
            Controls.Add(button1);
            Controls.Add(mono1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Mono mono1;
        private Button button1;
    }
}
