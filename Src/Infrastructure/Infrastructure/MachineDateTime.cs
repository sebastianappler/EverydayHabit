using EverydayHabit.Application.Common.Interfaces;
using System;

namespace EverydayHabit.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
