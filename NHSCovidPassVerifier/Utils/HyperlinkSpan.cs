using System;
using NHSCovidPassVerifier.Models.Commands;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NHSCovidPassVerifier.Utils
{
    public class HyperlinkSpan : Span
    {
        public static readonly BindableProperty UrlProperty = BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkSpan), null);

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public HyperlinkSpan()
        {
            TextDecorations = TextDecorations.Underline;
            TextColor = NhsColour.NhsLinkColour.Color();
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new AsyncCommand(async () => await Launcher.OpenAsync(Url))
            });
        }
    }
}
