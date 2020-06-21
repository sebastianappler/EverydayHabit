using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EverydayHabit.XamarinApp.Common.Components
{
    [DesignTimeVisible(true)]
    public partial class IconBubbleFrame : ContentView, INotifyPropertyChanged
    {
        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(IconBubbleFrame), string.Empty);

        public IconBubbleFrame()
        {
            InitializeComponent();
        }

        public string Icon
        {
            get => (string)GetValue(IconBubbleFrame.IconProperty);
            set => SetValue(IconBubbleFrame.IconProperty, value);
        }
    }
}