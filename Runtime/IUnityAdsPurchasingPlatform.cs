#if UNITY_PURCHASING
using UnityEngine.Purchasing;

namespace UnityEngine.Advertisements {
    public interface IUnityAdsPurchasingPlatform
    {
        void Initialize(IStoreListener storeListener, ConfigurationBuilder builder);
        void SetMetaData();
    }
}
#endif