namespace CheesePuzzle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LearningRateSlider.Value = 1;
            EpsilonSlider.Value = 3;
        }

        private void LearningRateSlider_Scroll(object sender, EventArgs e)
        {
            var Slider = (TrackBar)sender;

            mono1.LearningRate = Slider.Value * 0.1f;
        }

        private void EpsilonSlider_Scroll(object sender, EventArgs e)
        {
            var Slider = (TrackBar)sender;

            mono1.Epsilon = Slider.Value * 0.1f;
        }

        private void DelaySlider_Scroll(object sender, EventArgs e)
        {
            var Slider = (TrackBar)sender;

            mono1.delay = TimeSpan.FromMilliseconds(Slider.Value * 100);
        }

        private void QBox_CheckedChanged(object sender, EventArgs e)
        {
            mono1.ShowQ = ((CheckBox)sender).Checked;
        }
    }
}