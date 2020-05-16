using EverydayHabit.XamarinApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EverydayHabit.XamarinApp.Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HabitList : ContentPage 
    {
        public HabitList()
        {
            InitializeComponent();
        }
    }
}