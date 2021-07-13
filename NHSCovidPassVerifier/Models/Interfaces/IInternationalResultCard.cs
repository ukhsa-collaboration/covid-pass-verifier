using System;

namespace NHSCovidPassVerifier.Models.Interfaces
{
    public interface IInternationalResultCard : IComparable<IInternationalResultCard>, IComparable
    {
        public DateTime? GetSortByDate();
    }
}