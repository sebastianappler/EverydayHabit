using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EverydayHabit.XamarinApp.Common.ViewModels
{
    public class BasePageViewModel : INotifyPropertyChanged
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= _mediator = Startup.ServiceProvider.GetRequiredService<IMediator>();

        public BasePageViewModel()
        {
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<TData>(ref TData storage, TData value, [CallerMemberName] string propertyName = "")
        {
            if (storage.Equals(value))
                return;

            storage = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
