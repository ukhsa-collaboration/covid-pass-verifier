using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views.Elements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerVaccinationResultEntry : Grid
    {
        public ScannerVaccinationResultEntry()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == HeaderTextProperty.PropertyName)
            {
                Header.Text = HeaderText;
            }
            else if (propertyName == ContentTextProperty.PropertyName)
            {
                Content.Text = ContentText;
            }
            else if (propertyName == WithEndLineProperty.PropertyName)
                EndLine.IsVisible = WithEndLine;
        }
        
        public static readonly BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderTextProperty), typeof(string), typeof(Grid), null, BindingMode.OneWay);
        
        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }
        
        public static readonly BindableProperty ContentTextProperty =
                    BindableProperty.Create(nameof(ContentTextProperty), typeof(string), typeof(Grid), null, BindingMode.OneWay);
        
        public string ContentText
        {
            get => (string)GetValue(ContentTextProperty);
            set => SetValue(ContentTextProperty, value);
        }

        public static readonly BindableProperty WithEndLineProperty =
                    BindableProperty.Create(nameof(WithEndLineProperty), typeof(bool), typeof(Grid), true, BindingMode.OneWay);

        public bool WithEndLine
        {
            get => (bool)GetValue(WithEndLineProperty);
            set => SetValue(WithEndLineProperty, value);
        }
    }
}