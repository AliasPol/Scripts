using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace Shop {

    public class Purchaser : MonoBehaviour, IStoreListener {
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

        public static string product_150_Diamonds = "diamonds150";
        public static string product_300_Diamonds = "diamonds300";
        public static string product_600_Diamonds = "1200diamonds";
        public static string product_1200_Diamonds = "1200diamonds2";
        public static string product_2000_Diamonds = "2000diamonds";
        public static string product_2500_Diamonds = "diamonds2500";
        public static string product_3000_Diamonds = "diamonds3000";
        public static string product_4000_Diamonds = "diamonds4000";


        public Text[] textDiamondsCost;

        private void Start() {

            if (m_StoreController == null) {
                // Begin to configure our connection to Purchasing
                InitializePurchasing();

                SetPrices();
                
            }
        }

        public void InitializePurchasing() {
            // If we have already connected to Purchasing ...
            if (IsInitialized()) {
                // ... we are done here.
                return;
            }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct(product_150_Diamonds, ProductType.Consumable);
            builder.AddProduct(product_300_Diamonds, ProductType.Consumable);
            builder.AddProduct(product_600_Diamonds, ProductType.Consumable);
            builder.AddProduct(product_1200_Diamonds, ProductType.Consumable);
            builder.AddProduct(product_2000_Diamonds, ProductType.Consumable);
            builder.AddProduct(product_2500_Diamonds, ProductType.Consumable);
            builder.AddProduct(product_3000_Diamonds, ProductType.Consumable);
            builder.AddProduct(product_4000_Diamonds, ProductType.Consumable);


            UnityPurchasing.Initialize(this, builder);

            
        }


        private bool IsInitialized() {
            // Only say we are initialized if both the Purchasing references are set.
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }


        public void Buy150Diamonds() {
            
            BuyProductID(product_150_Diamonds);
        }
        public void Buy300Diamonds() {

            BuyProductID(product_300_Diamonds);
        }
        public void Buy600Diamonds() {

            BuyProductID(product_600_Diamonds);
        }
        public void Buy1200Diamonds() {

            BuyProductID(product_1200_Diamonds);
        }
        public void Buy2000Diamonds() {

            BuyProductID(product_2000_Diamonds);
        }
        public void Buy2500Diamonds() {

            BuyProductID(product_2500_Diamonds);
        }
        public void Buy3000Diamonds() {

            BuyProductID(product_3000_Diamonds);
        }
        public void Buy4000Diamonds() {

            BuyProductID(product_4000_Diamonds);
        }



        void BuyProductID(string productId) {
            
            if (IsInitialized()) {
                
                Product product = m_StoreController.products.WithID(productId);
                
                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase) {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                    // asynchronously.
                    m_StoreController.InitiatePurchase(product);
                }
                // Otherwise ...
                else {
                    // ... report the product look-up failure situation  
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            // Otherwise ...
            else {
                // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
                // retrying initiailization.
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
            // Purchasing has succeeded initializing. Collect our Purchasing references.
            Debug.Log("OnInitialized: PASS");

            // Overall Purchasing system, configured with products for this application.
            m_StoreController = controller;
            // Store specific subsystem, for accessing device-specific store features.
            m_StoreExtensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error) {
            // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {

            if (String.Equals(args.purchasedProduct.definition.id, product_150_Diamonds, StringComparison.Ordinal)) {
                Debug.Log("Wlasnie kupiles 150 diamentow");
                DiamondsMengerMenu.Instance.ChangeGems(150);
            }
            else if (String.Equals(args.purchasedProduct.definition.id, product_300_Diamonds, StringComparison.Ordinal)) {
                Debug.Log("Wlasnie kupiles 300 diamentow");
                DiamondsMengerMenu.Instance.ChangeGems(300);

            }
            else if (String.Equals(args.purchasedProduct.definition.id, product_600_Diamonds, StringComparison.Ordinal)) {
                Debug.Log("Wlasnie kupiles 600 diamentow");
                DiamondsMengerMenu.Instance.ChangeGems(600);

            }
            else if (String.Equals(args.purchasedProduct.definition.id, product_1200_Diamonds, StringComparison.Ordinal)) {
                Debug.Log("Wlasnie kupiles 1200 diamentow");
                DiamondsMengerMenu.Instance.ChangeGems(1200);

            }
            else if (String.Equals(args.purchasedProduct.definition.id, product_2000_Diamonds, StringComparison.Ordinal)) {
                Debug.Log("Wlasnie kupiles 2000 diamentow");
                DiamondsMengerMenu.Instance.ChangeGems(2000);

            }
            else if (String.Equals(args.purchasedProduct.definition.id, product_2500_Diamonds, StringComparison.Ordinal)) {
                Debug.Log("Wlasnie kupiles 2500 diamentow");
                DiamondsMengerMenu.Instance.ChangeGems(2500);

            }
            else if (String.Equals(args.purchasedProduct.definition.id, product_3000_Diamonds, StringComparison.Ordinal)) {
                Debug.Log("Wlasnie kupiles 3000 diamentow");
                DiamondsMengerMenu.Instance.ChangeGems(3000);

            }
            else if (String.Equals(args.purchasedProduct.definition.id, product_4000_Diamonds, StringComparison.Ordinal)) {
                Debug.Log("Wlasnie kupiles 4000 diamentow");
                DiamondsMengerMenu.Instance.ChangeGems(4000);

            }
            // Or ... an unknown product has been purchased by this user. Fill in additional products here....
            else {
                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
            }

            
            return PurchaseProcessingResult.Complete;
        }


        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
            // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
            // this reason with the user to guide their troubleshooting actions.
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }


        public void SetPrices() {
            switch (Application.systemLanguage.ToString()) {
                case "Polish":
                    PolishLenguage();
                    break;
                default:
                    DefaultLenguage();
                    break;
            }
        }


        private void PolishLenguage() {
            textDiamondsCost[0].text = "2.49 zł";
            textDiamondsCost[1].text = "4.69 zł";
            textDiamondsCost[2].text = "8.99 zł";
            textDiamondsCost[3].text = "16.99 zł";
            textDiamondsCost[4].text = "27.99 zł";
            textDiamondsCost[5].text = "34.99 zł";
            textDiamondsCost[6].text = "40.99 zł";
            textDiamondsCost[7].text = "49.99 zł";
        }

        private void DefaultLenguage() {
            textDiamondsCost[0].text = "0.99 €";
            textDiamondsCost[1].text = "1.89 €";
            textDiamondsCost[2].text = "3.40 €";
            textDiamondsCost[3].text = "6.50 €";
            textDiamondsCost[4].text = "10.50 €";
            textDiamondsCost[5].text = "13.00 €";
            textDiamondsCost[6].text = "15.20 €";
            textDiamondsCost[7].text = "20.00 €";
        }

    }
}