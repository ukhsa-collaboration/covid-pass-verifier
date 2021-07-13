using NHSCovidPassVerifier.ViewModels.Base;
using I18NPortable;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHSCovidPassVerifier.ViewModels
{
    public class AccessiblityViewModel : BaseViewModel
    {
        public string AccessiblityFirstParagraphText { get; set; }
        public string AccessiblitySecondParagraphText { get; set; }

        public string AccessibilityTitle { get; set; }
        public string AccessibilityParagraph1 { get; set; }
        public string AccessibilityTitle2 { get; set; }
        public string AccessibilityParagraph2 { get; set; }
        public string AccessibilityParagraph2Bullet1 { get; set; }
        public string AccessibilityParagraph2Bullet2 { get; set; }
        public string AccessibilityServiceTitle { get; set; }
        public string AccessibilityServiceParagraph { get; set; }
        public string AccessibilityServiceBullet1 { get; set; }
        public string AccessibilityServiceBullet2 { get; set; }
        public string AccessibilityServiceBullet3 { get; set; }
        public string AccessibilityComplianceTitle { get; set; }
        public string AccessibilityComplianceParagraph { get; set; }
        public string AccessibilityReortingTitle { get; set; }
        public string AccessibilityReortingParagraph { get; set; }
        public string AccessibilityEnforcementTitle { get; set; }
        public string AccessibilityEnforcementParagraph1 { get; set; }
        public string AccessibilityEnforcementParagraph1Link { get; set; }
        public string AccessibilityEnforcementParagraph2 { get; set; }
        public string AccessibilityTestedTitle { get; set; }
        public string AccessibilityTestedParagraph { get; set; }
        public string AccessibilityImproveTitle { get; set; }
        public string AccessibilityImproveParagraph { get; set; }

        public AccessiblityViewModel() {
            InitText();
        }

        

        private void InitText()
        {
            AccessibilityTitle = "ACCESSIBILITY_TITLE".Translate();
            AccessibilityParagraph1 = "ACCESSIBILITY_PARAGRAPH_1".Translate();
            AccessibilityTitle2 = "ACCESSIBILITY_TITLE_2".Translate();
            AccessibilityParagraph2 = "ACCESSIBILITY_PARAGRAPH_2".Translate();
            AccessibilityParagraph2Bullet1 = "ACCESSIBILITY_PARAGRAPH_2_BULLET_1".Translate();
            AccessibilityParagraph2Bullet2 = "ACCESSIBILITY_PARAGRAPH_2_BULLET_2".Translate();
            AccessibilityServiceTitle = "ACCESSIBILITY_ACCESSING_THE_SERVICE_TITLE".Translate();
            AccessibilityServiceParagraph = "ACCESSIBILITY_ACCESSING_THE_SERVICE_PARAGRAPH".Translate();
            AccessibilityServiceBullet1 = "ACCESSIBILITY_ACCESSING_THE_SERVICE_BULLET_1".Translate();
            AccessibilityServiceBullet2 = "ACCESSIBILITY_ACCESSING_THE_SERVICE_BULLET_2".Translate();
            AccessibilityServiceBullet3 = "ACCESSIBILITY_ACCESSING_THE_SERVICE_BULLET_3".Translate();
            AccessibilityComplianceTitle = "ACCESSIBILITY_COMPLIANCE_STATUS_TITLE".Translate();
            AccessibilityComplianceParagraph = "ACCESSIBILITY_COMPLIANCE_STATUS_PARAGRAPH".Translate();
            AccessibilityReortingTitle = "ACCESSIBILITY_REPORTING_TITLE".Translate();
            AccessibilityReortingParagraph = "ACCESSIBILITY_REPORTING_PARAGRAPH".Translate();
            AccessibilityEnforcementTitle = "ACCESSIBILITY_ENFORCEMENT_TITLE".Translate();
            AccessibilityEnforcementParagraph1 = "ACCESSIBILITY_ENFORCEMENT_PARAGRAPH_1".Translate();
            AccessibilityEnforcementParagraph1Link = "ACCESSIBILITY_ENFORCEMENT_PARAGRAPH_1_LINK".Translate();
            AccessibilityEnforcementParagraph2 = "ACCESSIBILITY_ENFORCEMENT_PARAGRAPH_2".Translate();
            AccessibilityTestedTitle = "ACCESSIBILITY_HOWWETESTED_TITLE".Translate();
            AccessibilityTestedParagraph = "ACCESSIBILITY_HOWWETESTED_PARAGRAPH".Translate();
            AccessibilityImproveTitle = "ACCESSIBILITY_IMPROVE_TITLE".Translate();
            AccessibilityImproveParagraph = "ACCESSIBILITY_IMPROVE_PARAGRAPH".Translate();
        }
    }
}
