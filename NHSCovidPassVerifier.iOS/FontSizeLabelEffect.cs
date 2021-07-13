using System;
using NHSCovidPassVerifier.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("NHSCovidPassVerifier")]
[assembly: ExportEffect(typeof(FontSizeLabelEffect), nameof(FontSizeLabelEffect))]
namespace NHSCovidPassVerifier.iOS
{
    public class FontSizeLabelEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var label = (UILabel)Control;
            label.AdjustsFontSizeToFitWidth = true;
            label.Lines = 2;
            label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
            label.LineBreakMode = UILineBreakMode.Clip;
        }

        protected override void OnDetached()
        {
        }
    }
}
