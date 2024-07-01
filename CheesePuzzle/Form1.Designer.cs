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
            LearningRateSlider = new TrackBar();
            EpsilonSlider = new TrackBar();
            LearningRateLabel = new Label();
            EpsilonLabel = new Label();
            DelayLabel = new Label();
            DelaySlider = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)LearningRateSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)EpsilonSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DelaySlider).BeginInit();
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
            // LearningRateSlider
            // 
            LearningRateSlider.Location = new Point(794, 39);
            LearningRateSlider.Name = "LearningRateSlider";
            LearningRateSlider.Size = new Size(304, 69);
            LearningRateSlider.TabIndex = 2;
            LearningRateSlider.Scroll += LearningRateSlider_Scroll;
            // 
            // EpsilonSlider
            // 
            EpsilonSlider.Location = new Point(794, 121);
            EpsilonSlider.Name = "EpsilonSlider";
            EpsilonSlider.Size = new Size(304, 69);
            EpsilonSlider.TabIndex = 3;
            EpsilonSlider.Scroll += EpsilonSlider_Scroll;
            // 
            // LearningRateLabel
            // 
            LearningRateLabel.AutoSize = true;
            LearningRateLabel.Location = new Point(794, 12);
            LearningRateLabel.Name = "LearningRateLabel";
            LearningRateLabel.Size = new Size(114, 25);
            LearningRateLabel.TabIndex = 4;
            LearningRateLabel.Text = "LearningRate";
            // 
            // EpsilonLabel
            // 
            EpsilonLabel.AutoSize = true;
            EpsilonLabel.Location = new Point(794, 93);
            EpsilonLabel.Name = "EpsilonLabel";
            EpsilonLabel.Size = new Size(69, 25);
            EpsilonLabel.TabIndex = 5;
            EpsilonLabel.Text = "Epsilon";
            // 
            // DelayLabel
            // 
            DelayLabel.AutoSize = true;
            DelayLabel.Location = new Point(794, 176);
            DelayLabel.Name = "DelayLabel";
            DelayLabel.Size = new Size(56, 25);
            DelayLabel.TabIndex = 6;
            DelayLabel.Text = "Delay";
            // 
            // DelaySlider
            // 
            DelaySlider.Location = new Point(794, 204);
            DelaySlider.Name = "DelaySlider";
            DelaySlider.Size = new Size(304, 69);
            DelaySlider.TabIndex = 7;
            DelaySlider.Scroll += DelaySlider_Scroll;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1255, 734);
            Controls.Add(DelaySlider);
            Controls.Add(DelayLabel);
            Controls.Add(EpsilonLabel);
            Controls.Add(LearningRateLabel);
            Controls.Add(EpsilonSlider);
            Controls.Add(LearningRateSlider);
            Controls.Add(mono1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)LearningRateSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)EpsilonSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)DelaySlider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Mono mono1;
        private TrackBar LearningRateSlider;
        private TrackBar EpsilonSlider;
        private Label LearningRateLabel;
        private Label EpsilonLabel;
        private Label DelayLabel;
        private TrackBar DelaySlider;
    }
}
