# Promo mediation adaptor 
## Overview
This package provides a purchase adapter for Unity developers that use mediated ads with [Unity IAP](https://docs.unity3d.com/Manual/UnityIAP.html), who wish to use [IAP Promos](https://docs.unity3d.com/Manual/IAPPromo.html) or [Personalized Placements](https://unityads.unity3d.com/help/unity/personalized-placements-unity). The adaptor allows the Unity Ads SDK to receive purchase and conversion events while in mediation.
​
### Requirements
The Promo Mediation Adapter requires the following:
​
* Your game is built in Unity, using version 2017.1 or higher.
* Your game uses Unity Ads in a [mediation](https://unityads.unity3d.com/help/resources/mediation) stack (verified compatible with AdMob, MoPub, and ironSource; see section on **Compatibility**, below).
* Your game uses Unity IAP version 1.16 or higher.
​
## Using the adapter
First, integrate Unity Ads as specified by your mediation partner. For more information on mediated integrations for Unity, [click here](https://unityads.unity3d.com/help/resources/mediation), or reference the following quick links:
​
* [MoPub integration](https://developers.mopub.com/docs/mediation/networks/unityads/)
* [AdMob integration](https://developers.google.com/admob/unity/mediation/unity)
* [ironSource integration](https://developers.ironsrc.com/ironsource-mobile/unity/unityads-mediation-guide/#step-1)
​
Next, initialize the SDK with the adapter’s new method signature:
​
```
public static void Initialize (IStoreListener listener, ConfigurationBuilder builder)
```
​
To use this method, you must implement an [`IStoreListener`](https://docs.unity3d.com/2017.3/Documentation/ScriptReference/Purchasing.IStoreListener.html) interface, and create a [`ConfigurationBuilder`](https://docs.unity3d.com/2017.3/Documentation/ScriptReference/Purchasing.ConfigurationBuilder.html) object. Both are requirements for Unity IAP, and covered in the [Unity IAP initialization documentation](https://docs.unity3d.com/Manual/UnityIAP.html). For example:

```
UnityAdsPurchasing.Initialize (myListener, myBuilder);
```
​
**Note**: When using the adapter, the `UnityAdsPurchasing.Initialize` method replaces the Unity IAP `UnityPurchasing.Initialize` method you would otherwise use. 

## Compatibility
The Promo Mediation Adapter is verified as compatible with AdMob, MoPub, and ironSource. However, as ironSource repackages Unity’s native binaries and renames the files, ironSource customers building to iOS must follow these additional steps:
​
1. Open the _UnityAdsPurchasingWrapper.mm_ file via your Project’s file folder, or the Unity Editor’s **Project** window (_Plugins/UnityAdsMediationAdapter/UnityAdsPurchaseWrapper.mm_).
2. Delete the top two imports (`UnityAds/UADSPurchasing.h` and `UnityAds/UADSMetaData.h`), and replace them with the following:
​
```
#import <ISUnityAdsAdaptor/UASDPurchasing.h>
#import <ISUnityAdsAdaptor/UADSMetaData.h>
```
​
## Known issues
Do not import both the Asset Store package and the Package Manager package in your Project, as it will cause duplicate definition errors. Any use of this adapter in a custom manner is not supported, and could result in errors or bugs in your integration.
​
If you experience problems or have questions, please [contact Unity Ads support](mailto:unityads-support@unity3d.com).