using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class Line : MonoBehaviour {

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider2d;
    public SurfaceEffector2D srfEffector2d;
    public float waitToDestroy;

    List<Vector2> points;

    private void Awake() {
        SetSurfaceEffecktor();
    }

    

    private void Start() {
        edgeCollider2d.offset = new Vector2(5.89f, -3.38f);
        StartCoroutine(DestroyLine());
    }

    public void UpdateLine(Vector2 mousePosition) {

        if(points == null) {
            points = new List<Vector2>();
            SetPoint(mousePosition);
            return;
        }


        if (Vector2.Distance(points.Last(), mousePosition) > .1f)
            SetPoint(mousePosition);



    }

    void SetPoint(Vector2 point) {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);

        if (points.Count > 1) {

            //edgeCollider2d.points[0] = edgeCollider2d.transform.InverseTransformPoint(edgeCollider2d.points[0]);
            edgeCollider2d.points = points.ToArray();
        }
    }


    public bool ToManyPoints() {
        if (points.Count > 100)
            return true;
        else
            return false;
    }

    public virtual void SetSurfaceEffecktor() {
        float x1 = Camera.main.transform.position.x;
        float x;
        x = ((7 * 0.001f) * x1) + 3;

        if (x > 12)
            x = 12;

        srfEffector2d.speed = x;
    }

    private IEnumerator DestroyLine() {

        yield return new WaitForSeconds(waitToDestroy);
        
        while (lineRenderer.positionCount > 1) {
            for (int i = 0; i < lineRenderer.positionCount-1; i++) {
                lineRenderer.SetPosition(i, lineRenderer.GetPosition(i+1));
            }
            edgeCollider2d.points = points.ToArray();
            lineRenderer.positionCount = points.Count - 1;
            points.Remove(points[0]);
            
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);

    }
}
