<h1 align="center">
	<img
		width="210"
		alt="Logo"
		src="https://user-images.githubusercontent.com/47818141/122049366-4048a780-cdda-11eb-9f23-8a71d772fb1c.png"
        >
</h1>

<h1 align="center">
	NHS COVID Pass Verifier - Native Application<br><br>
	<img
		width="700"
		alt="Logo"
		src="https://user-images.githubusercontent.com/47818141/123129262-ba64d600-d443-11eb-9075-d4a6bcbae36e.png"
        >
</h1>

<p align="center"> The official NHS COVID Pass Verifier app is a secure way to scan an individual’s NHS COVID Pass and check that they have been fully vaccinated against COVID-19, had a negative test, or have recovered from COVID-19.</p>


<p align="center">NHS COVID Pass Verifier  - Native Application is a Xamarin application that can be used scan COVID-19 2D barcodes.</p>

<p align="center">
</p>

<p align="center">
	<img src="https://user-images.githubusercontent.com/47818141/122911997-c96a5c00-d34f-11eb-85b6-61ac7c410f67.png" width="180">
	<img src="https://user-images.githubusercontent.com/47818141/122912133-e9018480-d34f-11eb-86fe-62e9f78fd967.png" width="180">
	<img src="https://user-images.githubusercontent.com/47818141/122912205-fb7bbe00-d34f-11eb-8a13-ba40125022c4.png" width="180">
</p>



## Tech Overview
**Xamarin :** Mobile app uses Xamarin forms to build reusable UX for all platforms. It enables us fast development and shared functionality between Android and iOS native app. Xamarin provides cross platform development, having .net framework helps support reusability of libraries. .Net Framework also provides strong benefits of managed memory. Additionally, Xamarin forms UX are easier to load, works on all types of mobile resolutions and independent of Mobile OS versions

## How to Install

<details>
	<summary> Android </summary>
	
## Installation and Usage: Android

### Prerequisites  
The prerequisites needed to run this mobile application include:

- Visual Studio  
- Xamarin  
- Android Studio

For a guide on how to set up Xamarin/ Visual Studio, refer to [this link here.](https://www.javatpoint.com/installation-of-xamarin)


#### Running on a Mobile Device  
To run the application/branch you are on within Visual Studio on your personal device, you will firstly need it plugged into the laptop. Some devices may need Debugging to be allowed, which will differ depending on the device you are using. When this is done correctly, you will see a dropdown at the top of Visual Studio, click the expander then select your device and click the run button.  

#### Running on an Emulator  
If you do not have an android device, you are able to run the application on a Android emulator. To do this you will need to first download Android Studio.

You can find out more about Android device manager within Visual Studio and how to get set up by watching this short video following [this link here.](https://www.youtube.com/watch?v=Hkc-OgPTki0)
	
## Configure on-device developer options
For instructions on how to enable android debugging on mobile devices, [follow this link.](https://developer.android.com/studio/debug/dev-options)
	
</details>

<details>
	<summary> iOS </summary>

## Installation and Usage: iOS

### Prerequisites
> Note: This requires a MacOS device and an assigned NHS apple developers account. The apple developers account will give access to a signing certificate for your computer and optionally provisioned profile if you have an iOS device.
 1. Install [Xcode](https://developer.apple.com/xcode/) from the App store and [Visual Studio for Mac](https://docs.microsoft.com/en-us/visualstudio/mac/installation?view=vsmac-2019)
 2.  Request an apple developer account from [Amit Pore](https://github.com/AmitPore) and in XCode navigate to **XCode > Preferences** and under account add your apple ID.
 3. In Visual Studio download and install the [Mobile development with .NET](https://docs.microsoft.com/en-us/xamarin/get-started/installation/?pivots=windows) workload
 4. In Visual Studio load the project into visual studio by selecting the [solution file](https://github.com/test-and-trace/nhs-health-record-app/blob/develop/CovidPassport.sln)

#### Running on an Emulator  

 1. Clean and rebuild the solution, select from a selection of device emulators and click Run. Please refer to the [official documentation](https://docs.microsoft.com/en-us/xamarin/ios/deploy-test/debugging-in-xamarin-ios?tabs=macos)
 ![enter image description here](https://docs.microsoft.com/en-us/xamarin/ios/deploy-test/debugging-in-xamarin-ios-images/debugging7.png)

#### Running on a Mobile Device

 1. To run the application on an iOS find the [UUID](https://get.udid.io/) for your mobile device and request a provisioned profile from [Amit Pore](https://github.com/AmitPore) by sending your UUID.
 2. Through your web browser log into [apple developer portal](https://developer.apple.com/) and log into the given NHS Apple developers accounts, Find your provisioned profile and install it on your mobile device.
 3. Connect your device to your computer using a USB connection. Clean and rebuild the solution, select your device and click Run. Please refer to the [official documentation](https://docs.microsoft.com/en-us/xamarin/ios/deploy-test/debugging-in-xamarin-ios?tabs=macos).



> Please refer to the [Microsoft official documents](https://docs.microsoft.com/en-us/xamarin/ios/get-started/installation/device-provisioning/free-provisioning?tabs=windows) or contact any team member
	
</details>

## NHS COVID Pass Verifier Application
The section demonstrates the usage of the NHS COVID Pass Verifier and how it is used in conjunction with the COVID 2D barcodes that are issued via the web application.

<details>
	<summary> NHS COVID Pass Verifier Overview </summary>

### How to use the NHS COVID Pass Verifier

<p align="center">
<img src="https://user-images.githubusercontent.com/47818141/122911997-c96a5c00-d34f-11eb-85b6-61ac7c410f67.png" width="180">
<img src="https://user-images.githubusercontent.com/47818141/122912133-e9018480-d34f-11eb-86fe-62e9f78fd967.png" width="180">
</p>


Upon navigating to the NHS COVID Pass Verifier application, you will first be met with the initial landing page. This page is the first page in the application where you are always able to navigate back to or progress to the COVID Pass Verifier.

The ”Check a 2D barcode” button will navigate you to the COVID Pass Verifier where you will be able to direct the camera at a users 2D barcode. To do this the users 2D barcode, which will be either on their device or printed out, will need to be placed inside the four blue square guidelines as seen above.
	
The NHS COVID Pass Verifier is aligned with the 1.3.0 EU schema, where more information can be found [here](https://github.com/ehn-dcc-development/ehn-dcc-schema). 
	
### COVID Pass Verifier Results (International)
<p align="center">
<img src="https://user-images.githubusercontent.com/47818141/122913146-01be6a00-d351-11eb-8bd3-41e51095c533.png" width="180">
<img src="https://user-images.githubusercontent.com/47818141/122930849-3c7dcd80-d364-11eb-8839-896739473ee3.png" width="180">
</p>

Once the COVID Pass Verifier has been pointed at a 2D barcode, it will automatically be verified without the user needing to press anything. Above you can see 2 images and these represent the 2 International outcomes from scanning a QR code. The 2 images in order along with their meanings are:

* **International**: The service enables the user to scan COVID 2D barcodes, to see if they have obtained any immunity via vaccinations. Therefore, this will assist in the reopening of society, focusing on international travel to display if you are vaccinated.  The NHS COVID Pass Verifier follows the [EU Digital Green Certificate](https://github.com/ehn-dcc-development) standard which this application can interpret and represent the data regarding an individual on this application. The data represented will show the verifier the vaccinations taken by the 2D barcode holder, it will display either 1 or many cards where each card represent a dose of vaccination. Each card will hold information regarding a vaccination such as the dose number, the manufacturer, authority administering the vaccine and more.
* **Recovery**:  When a 2D barcode is scanned it can also display recovery results. This does not represent and individual’s vaccination(s), rather it represents an individual who has recovered from Covid-19. This data is generated and based on test results of the individual. The 2D barcode verifier will show information such as the country of the test, the issuer of the certificate, date of first positive result and more. Just like the International results the recovery results is based [on EU Digital Green Certificate](https://github.com/ehn-digital-green-development) standard.

Once the cards are shown the user can click Dismiss and return to the COVID Pass Verifier.

### COVID Pass Verifier Results (Domestic)

<p align="center">
<img src="https://user-images.githubusercontent.com/47818141/122912205-fb7bbe00-d34f-11eb-8a13-ba40125022c4.png" width="180">
<img src="https://user-images.githubusercontent.com/47818141/122925355-a8f5ce00-d35e-11eb-9e60-7c2fecd3dc4b.png" width="157">
<img src="https://user-images.githubusercontent.com/47818141/122913379-42b67e80-d351-11eb-88aa-9214865427dd.png" width="180">
</p>


Once the COVID Pass Verifier has been pointed at a 2D barcode, it will automatically be verified without the user needed to press anything. Above you can see 3 images and these represent the 3 outcomes from scanning a domestic 2D barcode. These 3 images in order along with their meanings are:

* **Valid** : The 2D barcode that has been scanned is valid and they have been verified. This, therefore, means they will be allowed to enter the sporting fixture, theatre or other public events.

* **Expired** : The 2D barcode that has been verified was once valid however, it has been too long since they have last obtained a COVID pass. This means that they are not allowed into the event as they are not able to provide recent evidence they are COVID free.

* **QR code not recognised** : The 2D barcode was not recognised by the COVID Pass Verifier. This could be due to parts of the 2D barcode being obscured thus not being able to recognise if the COVID pass is valid or expired.


The date and time displayed on each pass show when they are due to expire (20 Apr 2021 at 09:14).

If you need more time to look at the COVID Pass Verifier result, you can hold the screen. Doing this will pause the natural timer set in the application which is visualised via the white progress bar located above the displayed name. Upon releasing the screen press, this timer will continue to countdown, returning to the COVID Pass Verifier ready to scan the next 2D barcode.

</details>

	
## Privacy Policy

<details>
	<summary> Privacy Policy </summary>

### What is the purpose for the processing of personal data?
The NHS COVID Pass Verifier application reads QR codes which store personal data and allows the user to read this information, however this information is never stored or transmitted on the NHS COVID Pass Verifier application.

### The Personal Data we collect and how it is used
READ_EXTERNAL_STORAGE and WRITE_EXTERNAL_STORAGE permissions are used to securely store keys that are used to verify a 2D Barcode has been signed by a trusted authority. These permissions are not used to store any data related to the user or app usage. The storage used does not hold any personal data. 

### Camera Usage
Upon clicking the ”Check a 2D barcode” button on the landing screen, the user is asked to give permission to the application to use the camera. If the user denies these permissions, a screen will appear specifying that the permissions are required to proceed to the camera. If the user grants these persmissions, the app will proceed.

### Security
We use appropriate technical, organisational and administrative security measures to protect any information we hold in our records from loss, misuse, unauthorised access, disclosure, alteration and destruction. We have written procedures and policies which are regularly audited and reviewed at a senior level. 

### Changes to our policy
We keep our Privacy Notice under regular review, and we will make new versions available on our Privacy Notice page, which can be viewed [here.](https://www.nhsx.nhs.uk/covid-19-response/covid-pass-verifier-app-support/) This Privacy Notice was last updated on 21st of May 2021.

</details>

## Terms and Conditions

<details>
	<summary> Terms and Conditions </summary>

### 1. Introduction

1.1.	Welcome to the terms of use (“Terms”) for the NHS COVID Verifier App (“Verifier App”). The Department of Health and Social Care (‘DHSC’, ‘we’, ‘our’) has overall responsibility for the Verifier App, which has been developed by, and will be operated by, NHSX (a joint working arrangement between DHSC and NHS England).

1.2.	These Terms apply to the Verifier App only. There are separate terms of use that apply to the NHS COVID Pass Service (“Service”) – those terms can be viewed here: https://covid-status.service.nhsx.nhs.uk/help/TermsAndConditions/.
	
### 2. Purpose of the Verifier App
	
2.1.	The purpose of the Verifier App is to allow the device (onto which the Verifier App is downloaded) to scan and read a 2D barcode produced by the Service, which contains information associated with the Service user’s digital ‘COVID Pass’. This certificate confirms that the Service user has been fully vaccinated against COVID-19 or has met negative testing or natural immunity requirements. The certificate can be used by the Service user to demonstrate their COVID Pass for the purposes of international travel, or for domestic purposes (which the Government may approve from time to time).

2.2.	The Verifier App is only designed for use in conjunction with the Service, and must not be used for any other purpose.  

2.3.	The Verifier App is provided by DHSC free of charge, and it is expressly prohibited to attempt to sell or license use of the Verifier App to any person, or to include the Verifier App as part of a paid-for service or product. 

2.4.	International barcodes are only to be checked by travel operators for the purposes of verifying an individual's COVID Pass and determining if it meets the international destination’s requirements needed. The service must not be used for any other purpose. 

### 3. How to use the Verifier App
	
3.1.	To use the Verifier App, you must first download it from the Apple App store: https://apps.apple.com/us/app/nhs-covid-pass-verifier/id1546716320 or Google Play store: https://play.google.com/store/apps/details?id=uk.gov.dhsc.healthrecord. You should refer to the terms of use and privacy notices applicable to each app store when visiting those stores to download the Verifier App.

3.2.	Upon navigating to the 2D barcode verifier, you will first be met with the initial landing page. This page is the first page in the application where you are always able to navigate back to or progress to the 2D Barcode verifier. 

3.3.	The “Check 2D barcode” button will navigate you to the 2D Barcode verifier where you will be able to direct the camera at a user’s COVID certificate. To do this, the user's 2D barcode, which will be either on their device or printed out, will need to be placed inside the four blue square guidelines.

3.4.	International results: the international 2D Barcode Code represents the COVID Pass of an individual person, it follows a set international standard designed by eHealth Network (Part of the European Commission), and all developers who develop COVID related applications follow this set standard. It is intended to be used for international travel. 

3.5.	Domestic results: the Domestic results of a COVID-19 2D barcode can hold two possible statuses. The first being Valid which represents a valid vaccination certificate of an individual user, the second status is Expired showing an expired vaccination certificate of an individual user. When represented on the app the verifier will see the Name, Status, and date of expiry. 

3.6.	About your data: The NHS COVID Pass Verifier application reads 2D barcodes that store personal data and allows the verifier to read and display this information. However, this information gathered from the 2D barcode is never stored or transmitted on the NHS COVID Pass Verifier application. App permissions are used to securely store keys that are used to verify a 2D barcode has been signed by a trusted authority. These permissions are not used to store any data related to the user or app usage. The storage used does not hold any personal data. 

	
### 4. Your right to use the Verifier App

4.1.	The Verifier App was developed for DHSC, and it and its content belongs to DHSC or its respective licensors as appropriate, and it is protected by intellectual property laws. As long as you comply with these Terms, you have a personal, perpetual, non-exclusive, non-transferable, revocable, limited licence to use the Verifier App for your own personal use.  We reserve all other licence rights not expressly permitted under these Terms. 

4.2.	For the avoidance of doubt, any reproduction, representation, distribution, modification, adaptation or translation of the Verifier App and its content, in whole or in part, is prohibited, except within the limit of these Terms or with the express authorisation of DHSC.

### 5. Our liability to you
	
5.1.	The Verifier App is provided on an "as is" basis and, to the extent permitted by law, we make no representations, warranties or guarantees, whether express or implied that the Verifier App will function as intended. We also cannot guarantee that the Verifier App will be available on an uninterrupted basis, secure or error or virus free, or that defects will be corrected. 

5.2.	Without limitation, we will not be liable to you for:
	
 5.2.1.	any use of the Verifier App that does not comply with these Terms
	
5.2.2.	any business loss (including but not limited to loss of profits, revenue, contracts, anticipated savings, data, goodwill or wasted expenditure)
	
5.2.3.	any loss or damage arising from an inability to access and/or use the Verifier App
	
5.2.4.	any indirect or consequential losses that were not foreseeable to both you and us when you commenced using the Verifier App or 
	
5.2.5.	any loss or damage caused by a virus or other technologically harmful material that may infect your device or data due to your use of the Verifier App.

5.3.	Nothing in this clause 6 excludes or limits our liability for death or personal injury arising from our negligence, or our fraud or fraudulent misrepresentation, or any other liability that cannot be excluded or limited by law.

### Continuity
	
6.1.	We reserve the right to suspend, terminate or otherwise alter access to some or all of the Verifier App at any time and without notice.
	
### Security 
	
7.1.	If you discover a potential security vulnerability or suspect a security incident related to this Service, please report it to covid-status.security@nhs.net. 
	
### Miscellaneous
	
8.1.	We may revise these Terms at any time and your continued use of the Verifier App will be deemed acceptance of such revised Terms.  Any such revisions take effect when this page is published.

8.2.	These Terms are governed by the laws of England and Wales.

8.3.	These Terms do not provide rights to, and may not be enforced by, any third party.

8.4.	Each of the clauses in these Terms operates separately. If any part of these Terms is determined to be invalid or unenforceable then the remainder of these Terms will remain in full force and effect. 

Last updated: **21st June 2021** 
	
	
</details>	
		

## For More Information
For more information on the COVID Pass Verifier, including how we handle Data and Privacy, please visit: https://www.nhsx.nhs.uk/covid-19-response/covid-pass-verifier-app-support/


If you have further questions about the NHS COVID Pass Verifier please email covid-status.feedback@nhs.net

## COVID-19 advice and information
	
- Get the latest [advice about coronavirus](https://www.nhs.uk/conditions/coronavirus-covid-19/).
- Find out [how to share your COVID-19 vaccination status](https://www.gov.uk/guidance/demonstrating-your-covid-19-vaccination-status-when-travelling-abroad).
	
