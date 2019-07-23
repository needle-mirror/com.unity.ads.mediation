#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace UnityEngine.Advertisements
{
    sealed class UnityAdsPurchasingPlatformAndroid : AndroidJavaProxy, IUnityAdsPurchasingPlatform, IPurchasingEventSender
    {
        readonly AndroidJavaClass m_UnityPurchasing;
        
        public UnityAdsPurchasingPlatformAndroid() : base("com.unity3d.ads.purchasing.IPurchasing")
        {
            m_UnityPurchasing = new AndroidJavaClass("com.unity3d.ads.purchasing.Purchasing");
        }
        
        public void Initialize(IStoreListener storeListener, ConfigurationBuilder builder)
        {
            UnityPurchasing.Initialize(storeListener, builder);
            m_UnityPurchasing.CallStatic("initialize", this);
            SetMetaData();
        }

        public void SetMetaData()
        {
            var metaDataObject = new AndroidJavaObject("com.unity3d.ads.metadata.MetaData", new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"));
            metaDataObject.Call("setCategory", "framework");
            metaDataObject.Call<bool>("set", "name", "Unity");
            metaDataObject.Call<bool>("set", "version", Application.version);
            metaDataObject.Call("commit");
        }

        void onPurchasingCommand(String eventString)
        {
            String result = PurchasingBridge.InitiatePurchasingCommand(eventString).ToString();
            int eventType = (int) PurchasingEvent.COMMAND;
            m_UnityPurchasing.CallStatic("dispatchReturnEvent", eventType, result);
        }

        void onGetPurchasingVersion()
        {
            String promoVersion = PurchasingBridge.GetPromoVersion();
            int eventType = (int) PurchasingEvent.VERSION;
            m_UnityPurchasing.CallStatic("dispatchReturnEvent", eventType, promoVersion);
        }

        void onGetProductCatalog()
        {
            String purchaseCatalog = PurchasingBridge.GetPurchasingCatalog();
            int eventType = (int) PurchasingEvent.CATALOG;
            m_UnityPurchasing.CallStatic("dispatchReturnEvent", eventType, purchaseCatalog);
        }

        void onInitializePurchasing()
        {
            String result = PurchasingBridge.Initialize().ToString();
            int eventType = (int) PurchasingEvent.INITIALIZATION;
            m_UnityPurchasing.CallStatic("dispatchReturnEvent", eventType, result);
        }

        public void SendPurchasingEvent(string payload)
        {
            int eventType = (int) PurchasingEvent.EVENT;
            m_UnityPurchasing.CallStatic("dispatchReturnEvent", eventType, payload);
        }
    }
}
#endif