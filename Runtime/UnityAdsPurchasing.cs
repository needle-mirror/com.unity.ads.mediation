#if UNITY_PURCHASING
using UnityEngine.Purchasing;

namespace UnityEngine.Advertisements
{
    public static class UnityAdsPurchasing
    {
        private static IUnityAdsPurchasingPlatform _platform;
        
        public static void Initialize(IStoreListener listener, ConfigurationBuilder builder)
        {
            #if UNITY_ANDROID
                _platform = new UnityAdsPurchasingPlatformAndroid();
            #elif UNITY_IOS
                _platform = new UnityAdsPurchasingPlatformIos();
            #else
                _platform = new UnityAdsPurchasingPlatformUnsupported();
            #endif
            
            _platform.Initialize(listener, builder);
        }
    }
}
#endif