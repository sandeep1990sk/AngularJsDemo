﻿
using Xamarin.Forms;

namespace myCIIEmployee
{
    public class ContactsPageCS : ContentPage
    {
        public ContactsPageCS()
        {
            Title = "Contacts Page";
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "Contacts data goes here",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
        }
    }
}
