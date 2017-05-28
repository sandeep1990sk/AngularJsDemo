using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace myCIIEmployee
{
    public class TodoListPageCS : ContentPage
    {
        public TodoListPageCS()
        {
            Title = "TodoList Page";
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "Todo list data goes here",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
        }
    }
}
