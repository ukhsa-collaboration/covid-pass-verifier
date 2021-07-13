using NHSCovidPassVerifier.Configuration;
using NHSCovidPassVerifier.ViewModels;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NHSCovidPassVerifier.Views.Elements
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomSwitch : ContentView
    {
        public CustomSwitch()
        {
            InitializeComponent();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
        }

        public event EventHandler SelectionChanged;

        protected virtual void OnSelectionChanged(CustomSwitchEventArgs e)
        {
            SelectionChanged?.Invoke(this, e);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == StateProperty.PropertyName)
            {
                UpdateSwitchState(State);
            }
        }

        private double valueX, valueY;
        private bool IsTurnX, IsTurnY;

        public void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var x = e.TotalX;
            var y = e.TotalY;

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    Debug.WriteLine("Started");
                    break;
                case GestureStatus.Running:
                    Debug.WriteLine("Running");

                    if ((x >= 5 || x <= -5) && !IsTurnX && !IsTurnY)
                    {
                        IsTurnX = true;
                    }

                    if ((y >= 5 || y <= -5) && !IsTurnY && !IsTurnX)
                    {
                        IsTurnY = true;
                    }

                    if (IsTurnX && !IsTurnY)
                    {
                        if (x <= valueX)
                        {
                            if (!State)
                            {
                                UpdateUI();
                            }
                        }

                        if (x >= valueX)
                        {
                            if (State)
                            {
                                UpdateUI();
                            }
                        }
                    }
                    OnSelectionChanged(new CustomSwitchEventArgs(State));


                    break;
                case GestureStatus.Completed:
                    Debug.WriteLine("Completed");

                    valueX = x;
                    valueY = y;

                    IsTurnX = false;
                    IsTurnY = false;

                    break;
                case GestureStatus.Canceled:
                    Debug.WriteLine("Canceled");
                    break;


            }
        }

        public void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            State = !State;
        }

        private void UpdateSwitchState(bool state)
        {
            State = state;
            UpdateUI();
            OnSelectionChanged(new CustomSwitchEventArgs(State));
        }

        private void UpdateUI()
        {
            if (State)
            {
                switchThumb.TranslateTo(switchThumb.X + 23, 0, 120);
                switchThumb.BorderColor = Color.FromHex("#ffffff");
                switchTrack.BackgroundColor = Color.FromHex("#007f3b");

            }
            else
            {
                switchThumb.TranslateTo(switchThumb.X, 0, 120);
                switchThumb.BorderColor = Color.FromHex("#8f9ca6");
                switchTrack.BackgroundColor = Color.FromHex("#768692");
            }
        }

        public static readonly BindableProperty StateProperty =
            BindableProperty.Create(nameof(State), typeof(bool), typeof(Grid), false, BindingMode.TwoWay);

        public bool State
        {
            get => (bool)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }


        void OnSwitchTapped(object sender, EventArgs e)
        {
            State = !State;
        }

        public class CustomSwitchEventArgs : EventArgs
        {
            public bool Selected { get; set; }

            public CustomSwitchEventArgs(bool selected)
            {
                Selected = selected;
            }
        }
    }
}