using System;
using System.Reflection;
using UnityEngine;

namespace UnityEngine.Advertisements
{
    public static class PurchasingBridge
    {
        private static Boolean s_Initialized;
        private static Type s_PurchasingManagerType;
        private static MethodInfo s_PurchasingInitiatePurchaseMethodInfo;
        private static MethodInfo s_PurchasingGetPromoVersionMethodInfo;
        private static MethodInfo s_PurchasingGetPromoCatalogMethodInfo;
        private static string s_PurchasingManagerClassName = "UnityEngine.Purchasing.Promo,Stores";
        private static string s_PurchasingInitiatePurchaseMethodName = "InitiatePurchasingCommand";
        private static string s_PurchasingGetPromoVersionMethodName = "Version";
        private static string s_PurchasingGetPromoCatalogMethodName = "QueryPromoProducts";

        public static bool Initialize()
        {
            if (s_Initialized) return s_Initialized;

            try
            {
                s_PurchasingManagerType = Type.GetType(s_PurchasingManagerClassName, true);
                s_PurchasingInitiatePurchaseMethodInfo =
                    s_PurchasingManagerType.GetMethod(s_PurchasingInitiatePurchaseMethodName, new[] {typeof(string)});
                s_PurchasingGetPromoVersionMethodInfo =
                    s_PurchasingManagerType.GetMethod(s_PurchasingGetPromoVersionMethodName);
                s_PurchasingGetPromoCatalogMethodInfo =
                    s_PurchasingManagerType.GetMethod(s_PurchasingGetPromoCatalogMethodName);
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
                return false;
            }

            return s_Initialized = true;
        }

        public static bool InitiatePurchasingCommand(string eventString)
        {
            Boolean isCommandSuccessful = false;
            if (s_PurchasingInitiatePurchaseMethodInfo != null)
            {
                try
                {
                    isCommandSuccessful =
                        (Boolean) s_PurchasingInitiatePurchaseMethodInfo.Invoke(s_PurchasingManagerType,
                            new object[] {eventString});
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                    return false;
                }
            }

            return isCommandSuccessful;
        }

        public static string GetPurchasingCatalog()
        {
            String purchasingCatalog = "";
            if (s_PurchasingGetPromoCatalogMethodInfo != null)
            {
                try
                {
                    purchasingCatalog =
                        (String) s_PurchasingGetPromoCatalogMethodInfo.Invoke(s_PurchasingManagerType, null);
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }

            return purchasingCatalog ?? "NULL";
        }

        public static string GetPromoVersion()
        {
            String promoVersion = "";
            if (s_PurchasingGetPromoVersionMethodInfo != null)
            {
                try
                {
                    promoVersion = (String) s_PurchasingGetPromoVersionMethodInfo.Invoke(s_PurchasingManagerType, null);
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }

            return promoVersion ?? "NULL";
        }
    }
}