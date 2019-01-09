using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootCounter : MonoBehaviour {

    private static int shootCounter = -1;

    private static GameObject ImagePrefab;
    private static List<GameObject> bulletSpawn;
    private static Transform thisObject;

    public static int ShootValue
    {
        get
        {
            return shootCounter;
        }
        set
        {
            shootCounter = value;
            if(bulletSpawn.Count != shootCounter) {
                if(bulletSpawn.Count > shootCounter) {
                    int remove = bulletSpawn.Count - shootCounter;
                    RemoveBulletImageUI(remove);
                }
                else {
                    int add = shootCounter - bulletSpawn.Count;
                    SpawnBulletImageUI(add);
                }
            }
 //           textCounterView.text = shootCounter.ToString();
        }
    }

    private void Awake()
    {
        if (ShootValue == -1) {
            ImagePrefab = Resources.Load("UIBulletImage") as GameObject;
            bulletSpawn = new List<GameObject>();
            thisObject = this.transform;
            ShootValue = 5;
        }
        else {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if(thisObject == null) {
            thisObject = this.transform;
            bulletSpawn.Clear();
            ShootValue = ShootValue;
        }
        
    }

    private static void SpawnBulletImageUI(int i)
    {
        for(int count = 1; count <= i; count++) {
            GameObject temp = Instantiate(ImagePrefab, thisObject);
            bulletSpawn.Add(temp);
        }
        Debug.Log(bulletSpawn.Count);
    }

    private static void RemoveBulletImageUI(int i)
    {
        for (int count = 1; count <= i; count++) {
            bulletSpawn.RemoveAt(0);
            Transform temp = thisObject.GetChild(0);
            Destroy(temp.gameObject);
            
        }
        Debug.Log(bulletSpawn.Count);
    }

    public void Shoot()
    {
        PlayerController.Instance.GetSwipe(Swipe.shoot);
    }
}
