using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private static PlayerController instance;
    public static PlayerController Instance {
        get
        {
            if(instance == null) {
                instance = FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }


    public bool buttonUp = false;
    public Image image;

    private bool canMove = true;
    private void Update() {
        
        if (canMove) {
            if (Input.GetMouseButtonDown(0)) {//|| Input.GetTouch(0).phase == TouchPhase.Began) {
                Debug.LogWarning("CLICK");
                GameObject clickedObject = ClickSelect();
                buttonUp = false;
                if (clickedObject != null) {
                    Power power = GameManager.Instance.currentPower;
                    BlockBehavior.Instance.StartScript(power, clickedObject);
                }
            }

            if (Input.GetMouseButtonUp(0)) {
                buttonUp = true;
            }
        }
    }

    public void YouUseMove() {
       // StartCoroutine(WaitForNextMove());
    }

    private IEnumerator WaitForNextMove() {
        canMove = false;
        float time = 0f;
        image.fillAmount = 0f;
        while(time < 1f) {

            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
            image.fillAmount = time;
        }
        image.fillAmount = 1f;
        canMove = true;
    }


    private GameObject ClickSelect() {
        //Converting Mouse Pos to 2D (vector2) World Pos
        //Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

        //if (hit && hit.transform.gameObject.layer == LayerMask.NameToLayer("Block")) {
        //    Debug.Log(hit.transform.name);
        //    return hit.transform.gameObject;
        //}
        //else {
        //    Debug.LogError("NONE");
        //    return null;
        //}

        Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D[] hits = Physics2D.RaycastAll(rayPos, Vector2.zero, 0f);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.layer == LayerMask.NameToLayer("UI"))
                return null;
        }
        for (int i = 0; i < hits.Length; i++)
        { 
            if (hits[i].collider != null && hits[i].transform.gameObject.layer == LayerMask.NameToLayer("Block"))
            {
                Debug.Log(hits[i].transform.name);
                return hits[i].transform.gameObject;
            }
        }
        Debug.LogError("NONE");
        return null;
    }
}
