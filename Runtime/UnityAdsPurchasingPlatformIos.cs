#if UNITY_IOS
using System.Runtime.InteropServices;
using UnityEngine.Purchasing;
using AOT;

namespace UnityEngine.Advertisements
{
    public class UnityAdsPurchasingPlatformIos : IUnityAdsPurchasingPlatform, IPurchasingEventSender
    {
        delegate void unityAdsPurchasingDidInitiatePurchasingCommand(string eventString);
        delegate void unityAdsPurchasingGetProductCatalog();
        delegate void unityAdsPurchasingGetPurchasingVersion();
        delegate void unityAdsPurchasingInitialize();
        
        [DllImport("__Internal")]
        static extern void InitializeUnityAdsPurchasingWrapper();
        
        [DllImport("__Internal")]
        static extern void UnityAdsPurchasingDispatchReturnEvent(long eventType, string payload);

        [DllImport("__Internal")]
        static extern void UnityAdsSetDidInitiatePurchasingCommandCallback(unityAdsPurchasingDidInitiatePurchasingCommand callback);

        [DllImport("__Internal")]
        static extern void UnityAdsSetGetProductCatalogCallback(unityAdsPurchasingGetProductCatalog callback);

        [DllImport("__Internal")]
        static extern void UnityAdsSetGetVersionCallback(unityAdsPurchasingGetPurchasingVersion callback);

        [DllImport("__Internal")]
        static extern void UnityAdsSetInitializePurchasingCallback(unityAdsPurchasingInitialize callback);

        [DllImport("__Internal")]
        static extern void UnityAdsSetMetaData(string category, string data);

        public void Initialize(IStoreListener storeListener, ConfigurationBuilder builder)
        {
            InitializeUnityAdsPurchasingWrapper();
            UnityAdsSetDidInitiatePurchasingCommandCallback(UnityAdsDidInitiatePurchasingCommand);
            UnityAdsSetGetProductCatalogCallback(UnityAdsPurchasingGetProductCatalog);
            UnityAdsSetGetVersionCallback(UnityAdsPurchasingGetPurchasingVersion);
            UnityAdsSetInitializePurchasingCallback(UnityAdsPurchasingInitialize);
            
            UnityPurchasing.Initialize(storeListener, builder);

            SetMetaData();
        }

        public void SetMetaData()
        {
            var metaData = "{\"name\":\"Unity\",\"version\":\"" + Application.unityVersion +  "\"}";
            UnityAdsSetMetaData("framework", metaData);
        }

        public void SendPurchasingEvent(string payload)
        {
            UnityAdsPurchasingDispatchReturnEvent((long) PurchasingEvent.EVENT, payload);
        }

        [MonoPInvokeCallback(typeof(unityAdsPurchasingDidInitiatePurchasingCommand))]
        static void UnityAdsDidInitiatePurchasingCommand(string eventString)
        {
            string result = PurchasingBridge.InitiatePurchasingCommand(eventString).ToString();
            UnityAdsPurchasingDispatchReturnEvent((long) PurchasingEvent.COMMAND, result);
        }

        [MonoPInvokeCallback(typeof(unityAdsPurchasingGetProductCatalog))]
        static void UnityAdsPurchasingGetProductCatalog()
        {
            string result = PurchasingBridge.GetPurchasingCatalog();
            UnityAdsPurchasingDispatchReturnEvent((long) PurchasingEvent.CATALOG, result);
        }

        [MonoPInvokeCallback(typeof(unityAdsPurchasingGetPurchasingVersion))]
        static void UnityAdsPurchasingGetPurchasingVersion()
        {
            string result = PurchasingBridge.GetPromoVersion();
            UnityAdsPurchasingDispatchReturnEvent((long) PurchasingEvent.VERSION, result);
        }

        [MonoPInvokeCallback(typeof(unityAdsPurchasingInitialize))]
        static void UnityAdsPurchasingInitialize()
        {
            string result = PurchasingBridge.Initialize().ToString();
            UnityAdsPurchasingDispatchReturnEvent((long) PurchasingEvent.INITIALIZATION, result);
        }
    }
}
#endif