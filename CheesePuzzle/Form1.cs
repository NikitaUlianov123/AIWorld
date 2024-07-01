namespace CheesePuzzle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

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
    }
}