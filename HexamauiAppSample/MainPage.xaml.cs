namespace HexamauiAppSample
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            borderTest.Background = Colors.DarkRed;
            Thread.Sleep(10000);
            borderTest.Background = Color.FromArgb("#2B0B98");
        }
    }

}
