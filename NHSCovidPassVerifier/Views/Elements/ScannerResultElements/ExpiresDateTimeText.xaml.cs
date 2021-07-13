using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views.Elements.ScannerResultElements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpiresDateTimeText : Grid
    {
        public ExpiresDateTimeText()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ExpiresDateTextProperty.PropertyName)
                ExpiresDate.Text = ExpiresDateText;
            
            else if (propertyName == ExpiresTimeTextProperty.PropertyName)
                ExpiresTime.Text = ExpiresTimeText;
            
        }

        public static readonly BindableProperty ExpiresDateTextProperty =
            BindableProperty.Create(nameof(ExpiresDateTextProperty), typeof(string), typeof(Grid), null, BindingMode.OneWay);

        public string ExpiresDateText
        {
            get => (string)GetValue(ExpiresDateTextProperty);
            set => SetValue(ExpiresDateTextProperty, value);
        }

        public static readonly BindableProperty ExpiresTimeTextProperty =
            BindableProperty.Create(nameof(ExpiresTimeTextProperty), typeof(string), typeof(Grid), null, BindingMode.OneWay);

        public string ExpiresTimeText
        {
            get => (string)GetValue(ExpiresTimeTextProperty);
            set => SetValue(ExpiresTimeTextProperty, value);
        }
    }
}