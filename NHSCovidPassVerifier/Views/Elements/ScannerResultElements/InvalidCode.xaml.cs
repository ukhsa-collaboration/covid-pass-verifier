using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views.Elements.ScannerResultElements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvalidCode : Grid
    {
        public InvalidCode()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == InvalidStatusTextProperty.PropertyName)
                InvalidStatus.Text = InvalidStatusText;
        }

        public static readonly BindableProperty InvalidStatusTextProperty =
            BindableProperty.Create(nameof(InvalidStatusTextProperty), typeof(string), typeof(Grid), null, BindingMode.OneWay);

        public string InvalidStatusText
        {
            get => (string)GetValue(InvalidStatusTextProperty);
            set => SetValue(InvalidStatusTextProperty, value);
        }
    }
}