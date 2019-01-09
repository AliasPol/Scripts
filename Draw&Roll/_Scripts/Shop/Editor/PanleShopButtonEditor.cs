using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Shop {
    [CustomEditor(typeof(ShopListCreator))]
    public class PanleShopButtonEditor : Editor {


        public override void OnInspectorGUI() {
            ShopListCreator shopBall = (ShopListCreator)target;

            DrawDefaultInspector();
            
            if (GUILayout.Button("Generate")) {
                shopBall.Generate();
            }

        }
        

    }
}