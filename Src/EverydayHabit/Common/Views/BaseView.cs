using EverydayHabit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Common.Views
{
    public abstract class BaseView : ContentPage
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= _mediator = Startup.ServiceProvider.GetRequiredService<IMediator>();
    }
}
