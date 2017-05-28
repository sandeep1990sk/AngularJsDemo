using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace myCIIEmployee
{
    public class ReminderPageCS : ContentPage
    {
        public ReminderPageCS()
        {
            Title = "Reminder Page";
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "Reminder data goes here",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
        }
    }
}
