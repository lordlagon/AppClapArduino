using Xamarin.Forms;

namespace Core
{
    public partial class MainPage : ContentPage
    {
        MainViewModel ViewModel => BindingContext as MainViewModel;
        public MainPage()
        {
            InitializeComponent();
        }
        
    }
}
