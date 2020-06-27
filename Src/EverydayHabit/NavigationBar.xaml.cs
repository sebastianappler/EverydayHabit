using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EverydayHabit.XamarinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationBar : NavigationPage
    {
        public NavigationBar()
        {
            InitializeComponent();
        }

        public NavigationBar(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}