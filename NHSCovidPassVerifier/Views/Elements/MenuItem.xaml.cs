using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views.Elements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuItem : Grid
    {
        public MenuItem()
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
            else if (propertyName == CommandProperty.PropertyName)
            {
                CommandGestureRecognizer.Command = Command;
            }
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(TextProperty), typeof(string), typeof(Grid), null, BindingMode.OneWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(CommandProperty), typeof(ICommand), typeof(Grid), null, BindingMode.OneWay);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}