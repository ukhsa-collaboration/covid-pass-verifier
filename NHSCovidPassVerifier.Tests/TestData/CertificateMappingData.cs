using System.Collections.Generic;

namespace NHSCovidPassVerifier.Tests.TestData
{
    public static class CertificateMappingData
    {
        public static IDictionary<string, string> GetVaccineTypes()
        {
            return new Dictionary<string, string>
            {
                {"1119349007", "SARS-CoV2 mRNA vaccine"},
                {"1119305005", "SARS-CoV2 antigen vaccine"},
                {"J07BX03", "covid-19 vaccines"}
            };
        }

        public static IDictionary<string, string> GetVaccineManufacturers()
        {
            return new Dictionary<string, string>
            {
                {"ORG-100001699", "AstraZeneca AB"},
                {"ORG-100030215", "Biontech Manufacturing GmbH"},
                {"ORG-100001417", "Janssen-Cilag International"},
                {"ORG-100031184", "Moderna Biotech Spain S.L."},
                {"ORG-100006270", "Curevac AG"},
                {"ORG-100013793", "CanSino Biologics"},
                {"ORG-100020693", "China Sinopharm International Corp. - Beijing location"},
                {"ORG-100010771", "Sinopharm Weiqida Europe Pharmaceutical s.r.o. - Prague location"},
                {"ORG-100024420", "Sinopharm Zhijun (Shenzhen) Pharmaceutical Co. Ltd. - Shenzhen location"},
                {"ORG-100032020", "Novavax CZ AS"},
                {"Gamaleya-Research-Institute", "Gamaleya Research Institute"},
                {"Vector-Institute", "Vector Institute"},
                {"Sinovac-Biotech", "Sinovac Biotech"},
                {"Bharat-Biotech", "Bharat Biotech"},
                {"ORG-100002068", "Valneva Sweden AB"}
            };
        }

        public static IDictionary<string, string> GetDiseasesTargeted()
        {
            return new Dictionary<string, string>
            {
                {"840539006", "COVID-19"}
            };
        }

        public static IDictionary<string, string> GetVaccineNames()
        {
            return new Dictionary<string, string>
            {
                {"EU/1/20/1528", "Comirnaty"},
                {"EU/1/20/1507", "COVID-19 Vaccine Moderna"},
                {"EU/1/21/1529", "Vaxzevria"},
                {"EU/1/20/1525", "COVID-19 Vaccine Janssen"},
                {"CVnCoV", "CVnCoV"},
                {"NVX-CoV2373": "NVX-CoV2373",
                {"Sputnik-V", "Sputnik-V"},
                {"Convidecia", "Convidecia"},
                {"EpiVacCorona", "EpiVacCorona"},
                {"BBIBP-CorV", "BBIBP-CorV"},
                {"Inactivated-SARS-CoV-2-Vero-Cell", "Inactivated SARS-CoV-2 (Vero Cell)"},
                {"CoronaVac", "CoronaVac"},
                {"Covaxin", "Covaxin (also known as BBV152 A, B, C)"}
            };
        }

        public static IDictionary<string, string> GetReadableVaccineNames()
        {
            return new Dictionary<string, string>
            {
                {"EU/1/20/1528", "Pfizer/BioNTech COVID-19 vaccine"},
                {"EU/1/21/1529", "COVID-19 Vaccine AstraZeneca"}
            };
        }

        public static IDictionary<string, string> GetTestTypes()
        {
            return new Dictionary<string, string>
            {
                {"LP6464-4", "Nucleic acid amplification with probe detection"},
                {"LP217198-3", "Rapid immunoassay"}
            };
        }

        public static IDictionary<string, string> GetTestResults()
        {
            return new Dictionary<string, string>
            {
                {"260415000", "Not detected"},
                {"260373001", "Detected"}
            };
        }

        public static IDictionary<string, string> GetTestManufacturers()
        {
            return new Dictionary<string, string>
            {
                { "1833", "AAZ-LMB, COVID-VIRO" },
                { "1232", "Abbott Rapid Diagnostics, Panbio COVID-19 Ag Rapid Test"},
                { "1468", "ACON Laboratories, Inc, Flowflex SARS-CoV-2 Antigen rapid test"},
                { "1304", "AMEDA Labordiagnostik GmbH, AMP Rapid Test SARS-CoV-2 Ag"}
            };
        }
    }
}