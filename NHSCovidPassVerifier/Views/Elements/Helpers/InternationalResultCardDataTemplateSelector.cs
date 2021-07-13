using System;
using NHSCovidPassVerifier.Models.International.Cards;
using Xamarin.Forms;

namespace NHSCovidPassVerifier.Views.Elements.Helpers
{
    public class InternationalResultCardDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate VaccinationCardTemplate { get; set; }
        public DataTemplate RecoveryCardTemplate { get; set; }
        public DataTemplate TestResultCardTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var type = item.GetType();
            if (type == typeof(VaccinationCard)) return VaccinationCardTemplate;
            if (type == typeof(RecoveryCard)) return RecoveryCardTemplate;
            if (type == typeof(TestResultCard)) return TestResultCardTemplate;

            throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}