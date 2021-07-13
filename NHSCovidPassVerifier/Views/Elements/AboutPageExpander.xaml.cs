using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views.Elements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPageExpander : Frame
    {
        public AboutPageExpander()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                TextLabel.Text = Text;
            }
            else if (propertyName == TextTitleProperty.PropertyName)
            {
                TextTitleLabel.Text = TextTitle;
            }
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(TextProperty), typeof(string), typeof(Grid), null, BindingMode.OneWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty TextTitleProperty =
            BindableProperty.Create(nameof(TextTitleProperty), typeof(string), typeof(Grid), null, BindingMode.OneWay);

        public string TextTitle
        {
            get => (string)GetValue(TextTitleProperty);
            set => SetValue(TextTitleProperty, value);
        }
    }
}