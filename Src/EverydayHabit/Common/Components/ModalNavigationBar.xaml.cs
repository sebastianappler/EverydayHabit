
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EverydayHabit.XamarinApp.Common.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalNavigationBar : ContentView, INotifyPropertyChanged
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(ModalNavigationBar), string.Empty);
        public static readonly BindableProperty OnCloseCommandProperty = BindableProperty.Create(nameof(OnCloseCommand), typeof(ICommand), typeof(ModalNavigationBar), null, defaultBindingMode: BindingMode.TwoWay);

        public ModalNavigationBar()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public ICommand OnCloseCommand
        {
            get => (ICommand) GetValue(OnCloseCommandProperty);
            set => SetValue(OnCloseCommandProperty, value);
        }
    }
}