using EverydayHabit.XamarinApp.Common.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EverydayHabit.XamarinApp.Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HabitSettings : BaseView
    {
        public HabitSettings()
        {
            InitializeComponent();
        }
    }
}