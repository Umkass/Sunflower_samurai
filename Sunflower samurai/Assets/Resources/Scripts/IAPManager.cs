using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
public class IAPManager : MonoBehaviour
{
    string noAds = "no_ads_1";
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == noAds)
        {
            PlayerPrefs.SetInt("noAds", 1);
            Debug.Log("Purchase completed");
        }
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {
        if (product.definition.id == noAds)
            Debug.Log("Purchase execution error due to" + reason);

    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("noAds"))
            PlayerPrefs.SetInt("noAds", 0);
    }
}
