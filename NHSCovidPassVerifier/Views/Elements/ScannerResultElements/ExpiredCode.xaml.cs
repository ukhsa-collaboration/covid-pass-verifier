using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views.Elements.ScannerResultElements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpiredCode : Grid
    {
        public ExpiredCode()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ExpiredTextProperty.PropertyName)
                Expired.Text = ExpiredText;
        }

        public static readonly BindableProperty ExpiredTextProperty =
    BindableProperty.Create(nameof(ExpiredTextProperty), typeof(string), typeof(Grid), null, BindingMode.OneWay);

        public string ExpiredText
        {
            get => (string)GetValue(ExpiredTextProperty);
            set => SetValue(ExpiredTextProperty, value);
        }
    }
}