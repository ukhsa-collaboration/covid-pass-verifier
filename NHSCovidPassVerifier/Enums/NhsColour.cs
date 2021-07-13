using System;
using Xamarin.Forms;

namespace NHSCovidPassVerifier.Utils
{
    public enum NhsColour
    {
        NhsLinkColour,
        NhsErrorColour,

        NhsButtonColour,
        NhsButtonShadowColour,

        NhsBackgroundColour,
        NhsDarkBackground,

        NhsWhite,
        NhsBlue,
        NhsButtonGreen,
        NhsTextBlack,

        TextColourRed,
        BorderColour,
        BackGroundColour,
    }

    public static class NhsColourExtensions
    {
        public static Color Color(this NhsColour nhsColour)
        {
            string colourString = Enum.GetName(typeof(NhsColour), nhsColour);
            Color? colour = Application.Current.Resources[colourString] as Color?;
            return colour ?? Xamarin.Forms.Color.White;
        }
    }

}
