using NHSCovidPassVerifier.Models.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NHSCovidPassVerifier.Utils
{
    public class HyperlinkImage : Image
    {
        public static readonly BindableProperty UrlProperty = BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkSpan), null);

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public HyperlinkImage()
        {
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new AsyncCommand(async () => await Launcher.OpenAsync(Url))
            });
        }
    }
}
