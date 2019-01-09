using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

	public enum CollectibleType { Bullet, Fuel, hotDog};

    public CollectibleType typeOf;


    private void OnTriggerEnter(Collider other)
    {


        switch (typeOf) {
            case CollectibleType.Bullet:
                ShootCounter.ShootValue++;
                Destroy(gameObject);
                break;
            case CollectibleType.Fuel:
                FindObjectOfType<FuelSlider>().Refile();
                MenagerAudio.Instance.PlaySoundEffect(Sound.EffectList.FuelCollectV2);
                Destroy(gameObject);
                break;
            case CollectibleType.hotDog:
                HotDogCounter.HotDogValue++;
                MenagerAudio.Instance.PlaySoundEffect(Sound.EffectList.AlienCollectHotDog);
                Destroy(gameObject);
                break;
        }
    }
}
