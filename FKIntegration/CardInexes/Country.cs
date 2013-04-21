using System;
using FKIntegration.Repositories.FKP;

namespace FKIntegration.CardInexes
{
    public class Country
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool CountryUE { get; set; }

        internal Country(IItgCommon itgCountries)
        {
            FillCountryBody(itgCountries);
        }

        internal Country()
        {
        }

        private void FillCountryBody(IItgCommon itgCountry)
        {
            Id = (int)itgCountry["Id"];
            Code = (string)itgCountry["kod"];
            Name = (string)itgCountry["nazwa"];
            CountryUE = Convert.ToBoolean(itgCountry["krajUE"]);
        }        

        internal void FillSyncroBody(IItgCommon itgCountry)
        {
            itgCountry["id"] = Id;
            itgCountry["kod"] = Code;
            itgCountry["nazwa"] = Name;
            itgCountry["krajUE"] = CountryUE;
        }
    }
}