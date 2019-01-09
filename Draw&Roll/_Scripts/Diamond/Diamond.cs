using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {

    bool isMinus;
    public Color[] colors;
    bool bonus = false;
    private Color selectedColor;

    public bool canBeRed = true;

    public void Awake() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (isMinus)
                GameMenager.Instance.AddDiamonds(-5);
            else if (bonus)
                GameMenager.Instance.AddDiamonds(6);
            else
                GameMenager.Instance.AddDiamonds(1);

            ParticleSystem part = Instantiate(Resources.Load<ParticleSystem>("Particles/DiamondsParticle"), this.transform.position, this.transform.rotation);
            part.startColor = selectedColor;
            Destroy(this.gameObject);
        }
    }


    public void DiamondSpawn() {
        gameObject.SetActive(true);

        int rnd = Random.Range(0, 102);
        if (canBeRed && rnd > 94 && rnd < 100)
            SwitchToRed();
        else
            SwitchToGreen(rnd);

    }

    private void SwitchToRed() {
        isMinus = true;

        gameObject.GetComponent<SpriteRenderer>().color = colors[1];
        selectedColor = colors[1];
    }

    private void SwitchToGreen(int value) {
        isMinus = false;
        if (value < 100) {
            gameObject.GetComponent<SpriteRenderer>().color = colors[0];
            bonus = false;
            selectedColor = colors[0];
        }
        else {
            gameObject.GetComponent<SpriteRenderer>().color = colors[2];
            bonus = true;
            selectedColor = colors[2];
        }
    }
}
