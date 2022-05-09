using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerSide.Components.VacationList
{
    // https://github.com/aspnet/AspNetCore/blob/master/src/Components/Web/src/Web/EventHandlers.cs
    // Mouse events
    [EventHandler("onmouseenter", typeof(MouseEventArgs))]
    [EventHandler("onmouseleave", typeof(MouseEventArgs))]
    public class EventHandlers
    {
    }
}
