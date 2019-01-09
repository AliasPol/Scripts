using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineCreator : MonoBehaviour {

    public GameObject linePrefab;
    Line activeLine;
    Line activeLine2;
	
	// Update is called once per frame
	void Update () {

#if (UNITY_EDITOR)
        UnityEditorRun();
#endif
#if (!UNITY_EDITOR)
        AndroidRun();
#endif

    }

    private void AndroidRun() {

        if (Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject()) {
            GameObject newLine = Instantiate(linePrefab);
            activeLine = newLine.GetComponent<Line>();
        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended) {
            activeLine = null;
        }


        if (activeLine != null) {

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            activeLine.UpdateLine(mousePos);
        }

        if (activeLine != null && activeLine.ToManyPoints()) {
            GameObject newLine = Instantiate(linePrefab);
            activeLine = newLine.GetComponent<Line>();


            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            activeLine.UpdateLine(mousePos);
        }


        //Second touch
        if (Input.GetTouch(1).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject()) {
            GameObject newLine = Instantiate(linePrefab);
            activeLine2 = newLine.GetComponent<Line>();
        }
        if (Input.GetTouch(1).phase == TouchPhase.Ended) {
            activeLine2 = null;
        }


        if (activeLine2 != null) {

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
            activeLine2.UpdateLine(mousePos);
        }

        if (activeLine2 != null && activeLine2.ToManyPoints()) {
            GameObject newLine = Instantiate(linePrefab);
            activeLine2 = newLine.GetComponent<Line>();


            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);
            activeLine2.UpdateLine(mousePos);
        }
    }

    private void UnityEditorRun() {


        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == null) {
            GameObject newLine = Instantiate(linePrefab);
            activeLine = newLine.GetComponent<Line>();
        }
        if (Input.GetMouseButtonUp(0)) {
            activeLine = null;
        }


        if (activeLine != null) {

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }

        if(activeLine != null && activeLine.ToManyPoints()) {
            GameObject newLine = Instantiate(linePrefab);
            activeLine = newLine.GetComponent<Line>();
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }
    }

}
