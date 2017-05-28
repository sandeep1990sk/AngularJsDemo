using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace myCIIEmployee
{
    public class SubLink : Button
    {
        public SubLink(string name)
        {
            Text = name;
            Command = new Command(o => App.MasterDetailPage.Detail.Navigation.PushAsync(new LinkPage(name)));
        }
    }
}
