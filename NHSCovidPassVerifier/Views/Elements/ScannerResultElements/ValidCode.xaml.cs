using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace NHSCovidPassVerifier.Views.Elements.ScannerResultElements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ValidCode : Grid
    {
        public ValidCode()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ValidStatusTextProperty.PropertyName)
                ValidStatus.Text = ValidStatusText;
        }

        public static readonly BindableProperty ValidStatusTextProperty =
            BindableProperty.Create(nameof(ValidStatusTextProperty), typeof(string), typeof(Grid), null, BindingMode.OneWay);

        public string ValidStatusText
        {
            get => (string)GetValue(ValidStatusTextProperty);
            set => SetValue(ValidStatusTextProperty, value);
        }
    }
}