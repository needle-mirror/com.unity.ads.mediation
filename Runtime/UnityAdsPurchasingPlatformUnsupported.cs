#if UNITY_PURCHASING
using UnityEngine.Purchasing;

namespace UnityEngine.Advertisements
{
    public class UnityAdsPurchasingPlatformUnsupported : IUnityAdsPurchasingPlatform
    {
        public void Initialize(IStoreListener storeListener, ConfigurationBuilder builder)
        {
            
        }

        public void SetMetaData()
        {
            
        }
    }
}
#endif