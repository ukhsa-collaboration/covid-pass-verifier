using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NHSCovidPassVerifier.Models.Commands;
using NHSCovidPassVerifier.Utils;
using I18NPortable;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views.Elements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NhsHeader : Grid
    {
        public NhsHeader()
        {
            InitializeComponent();
            HeaderBackgroundColour = NhsColour.NhsBlue.Color();
            LeftButtonHeightRequest = 30;
            CenterImageImageSource = "nhs_logo_simple.png";
            CenterImageHeightRequest = 40;
            RightButtonHeightRequest = 30;

            AutomationProperties.SetIsInAccessibleTree(CenterImage, false);
            AutomationProperties.SetIsInAccessibleTree(LeftButton, false);
            AutomationProperties.SetIsInAccessibleTree(RightButton, false);
            LeftButtonAccessibilityText = "ACCESSIBILITY_BACK_BUTTON_HELP_TEXT".Translate();
            RightButtonAccessibilityText = "ACCESSIBILITY_LOGOUT_BUTTON_HELP_TEXT".Translate();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == HeaderBackgroundColourProperty.PropertyName)
            {
                HeaderGrid.BackgroundColor = HeaderBackgroundColour;
            }
            else if (propertyName == LeftButtonImageSourceProperty.PropertyName)
            {
                LeftButton.Source = LeftButtonImageSource;
                AutomationProperties.SetIsInAccessibleTree(LeftButton, LeftButtonImageSource != null);
            }
            else if (propertyName == LeftButtonCommandProperty.PropertyName)
            {
                LeftButton.Command = LeftButtonCommand;
            }
            else if (propertyName == LeftButtonHeightRequestProperty.PropertyName)
            {
                LeftButton.HeightRequest = LeftButtonHeightRequest;
            }
            else if (propertyName == LeftButtonAccessibilityTextProperty.PropertyName)
            {
                AutomationProperties.SetHelpText(LeftButton, LeftButtonAccessibilityText);
            }
            else if (propertyName == CenterImageImageSourceProperty.PropertyName)
            {
                CenterImage.Source = CenterImageImageSource;
            }
            else if (propertyName == CenterImageHeightRequestProperty.PropertyName)
            {
                CenterImage.HeightRequest = CenterImageHeightRequest;
            }
            else if (propertyName == RightButtonImageSourceProperty.PropertyName)
            {
                RightButton.Source = RightButtonImageSource;
                AutomationProperties.SetIsInAccessibleTree(RightButton, RightButtonImageSource != null);
            }
            else if (propertyName == RightButtonCommandProperty.PropertyName)
            {
                RightButton.Command = RightButtonCommand;
            }
            else if (propertyName == RightButtonHeightRequestProperty.PropertyName)
            {
                RightButton.HeightRequest = RightButtonHeightRequest;
            }
            else if (propertyName == RightButtonAccessibilityTextProperty.PropertyName)
            {
                AutomationProperties.SetHelpText(RightButton, RightButtonAccessibilityText);
            }
        }

        public static readonly BindableProperty HeaderBackgroundColourProperty =
            BindableProperty.Create(nameof(HeaderBackgroundColourProperty), typeof(Color), typeof(Grid), null, BindingMode.OneWay);

        public Color HeaderBackgroundColour
        {
            get
            {
                return (Color)GetValue(HeaderBackgroundColourProperty);
            }
            set
            {
                SetValue(HeaderBackgroundColourProperty, value);
            }
        }


        public static readonly BindableProperty LeftButtonImageSourceProperty =
            BindableProperty.Create(nameof(LeftButtonImageSourceProperty), typeof(ImageSource), typeof(ImageButton), null, BindingMode.OneWay);

        public ImageSource LeftButtonImageSource
        {
            get
            {
                return (ImageSource)GetValue(LeftButtonImageSourceProperty);
            }
            set
            {
                SetValue(LeftButtonImageSourceProperty, value);
            }
        }


        public static readonly BindableProperty LeftButtonCommandProperty =
            BindableProperty.Create(nameof(LeftButtonCommandProperty), typeof(AsyncCommand), typeof(ImageButton), null, BindingMode.OneWay);

        public AsyncCommand LeftButtonCommand
        {
            get
            {
                return (AsyncCommand)GetValue(LeftButtonCommandProperty);
            }
            set
            {
                SetValue(LeftButtonCommandProperty, value);
            }
        }


        public static readonly BindableProperty LeftButtonHeightRequestProperty =
            BindableProperty.Create(nameof(LeftButtonHeightRequestProperty), typeof(int), typeof(ImageButton), null, BindingMode.OneWay);

        public int LeftButtonHeightRequest
        {
            get
            {
                return (int)GetValue(LeftButtonHeightRequestProperty);
            }
            set
            {
                SetValue(LeftButtonHeightRequestProperty, value);
            }
        }

        public static readonly BindableProperty LeftButtonAccessibilityTextProperty =
            BindableProperty.Create(nameof(LeftButtonAccessibilityTextProperty), typeof(string), typeof(ImageButton), null, BindingMode.OneWay);

        public string LeftButtonAccessibilityText
        {
            get
            {
                return (string)GetValue(LeftButtonAccessibilityTextProperty);
            }
            set
            {
                SetValue(LeftButtonAccessibilityTextProperty, value);
            }
        }

        public static readonly BindableProperty CenterImageImageSourceProperty =
            BindableProperty.Create(nameof(CenterImageImageSourceProperty), typeof(string), typeof(Image), null, BindingMode.OneWay);

        public string CenterImageImageSource
        {
            get
            {
                return (string)GetValue(CenterImageImageSourceProperty);
            }
            set
            {
                SetValue(CenterImageImageSourceProperty, value);
            }
        }

        public static readonly BindableProperty CenterImageHeightRequestProperty =
            BindableProperty.Create(nameof(CenterImageHeightRequestProperty), typeof(int), typeof(Image), null, BindingMode.OneWay);

        public int CenterImageHeightRequest
        {
            get
            {
                return (int)GetValue(CenterImageHeightRequestProperty);
            }
            set
            {
                SetValue(CenterImageHeightRequestProperty, value);
            }
        }


        public static readonly BindableProperty RightButtonImageSourceProperty =
            BindableProperty.Create(nameof(RightButtonImageSourceProperty), typeof(ImageSource), typeof(ImageButton), null, BindingMode.OneWay);

        public ImageSource RightButtonImageSource
        {
            get
            {
                return (ImageSource)GetValue(RightButtonImageSourceProperty);
            }
            set
            {
                SetValue(RightButtonImageSourceProperty, value);
            }
        }


        public static readonly BindableProperty RightButtonCommandProperty =
            BindableProperty.Create(nameof(RightButtonCommandProperty), typeof(AsyncCommand), typeof(ImageButton), null, BindingMode.OneWay);

        public AsyncCommand RightButtonCommand
        {
            get
            {
                return (AsyncCommand)GetValue(RightButtonCommandProperty);
            }
            set
            {
                SetValue(RightButtonCommandProperty, value);
            }
        }


        public static readonly BindableProperty RightButtonHeightRequestProperty =
            BindableProperty.Create(nameof(RightButtonHeightRequestProperty), typeof(int), typeof(ImageButton), null, BindingMode.OneWay);

        public int RightButtonHeightRequest
        {
            get
            {
                return (int)GetValue(RightButtonHeightRequestProperty);
            }
            set
            {
                SetValue(RightButtonHeightRequestProperty, value);
            }
        }

        public static readonly BindableProperty RightButtonAccessibilityTextProperty =
            BindableProperty.Create(nameof(RightButtonAccessibilityTextProperty), typeof(string), typeof(ImageButton), null, BindingMode.OneWay);

        public string RightButtonAccessibilityText
        {
            get
            {
                return (string)GetValue(RightButtonAccessibilityTextProperty);
            }
            set
            {
                SetValue(RightButtonAccessibilityTextProperty, value);
            }
        }
    }
}