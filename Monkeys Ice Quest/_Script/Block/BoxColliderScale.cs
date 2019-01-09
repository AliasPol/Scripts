using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderScale : MonoBehaviour {

    public static float scale=0.85f;



    public static void ScaleCollider(Collider2D colider2d, bool scale) {


        switch (colider2d.GetType().ToString()) {
            case "UnityEngine.PolygonCollider2D":
                if (scale)
                    PolygonColliderDetected(colider2d.gameObject.GetComponent<PolygonCollider2D>());
                else
                    PolygonColliderReturn(colider2d.gameObject.GetComponent<PolygonCollider2D>());
                break;
            case "UnityEngine.BoxCollider2D":
                if (scale)
                    BoxColliderDetected(colider2d.gameObject.GetComponent<BoxCollider2D>());  
                else
                    BoxColliderReturn((colider2d.gameObject.GetComponent<BoxCollider2D>()));
                break;
            case "UnityEngine.CircleCollider2D":
                if (scale)
                    CircleColliderDetected(colider2d.gameObject.GetComponent<CircleCollider2D>());
                else
                    CircleColliderReturn(colider2d.gameObject.GetComponent<CircleCollider2D>());
                break;
            default:
                Debug.Log(colider2d.GetType());
                break;
        }
        
    }

    public static void BoxColliderDetected(BoxCollider2D box2D) {
        Vector2 sizeOfCol = box2D.size;
        sizeOfCol.x = sizeOfCol.x * scale;
        sizeOfCol.y = sizeOfCol.y * scale;

        box2D.size = sizeOfCol;
    }
    public static void CircleColliderDetected(CircleCollider2D circle2D) {
        float circleRadius = circle2D.radius;
        circleRadius = circleRadius * scale;
        circle2D.radius = circleRadius;

    }
    public static void PolygonColliderDetected(PolygonCollider2D polygon2D) {
        Vector2[] pointsOfCollider = polygon2D.points;
        
        for(int i = 0; i < pointsOfCollider.Length; i++) {
            pointsOfCollider[i].x = pointsOfCollider[i].x * scale;
            pointsOfCollider[i].y = pointsOfCollider[i].y * scale;
        }

        polygon2D.SetPath(0, pointsOfCollider);
    }


    private static void BoxColliderReturn(BoxCollider2D box2D) {
        Vector2 sizeOfCol = box2D.size;
        sizeOfCol.x = sizeOfCol.x * ((100/scale)/100);
        sizeOfCol.y = sizeOfCol.y * ((100 / scale) / 100);

        box2D.size = sizeOfCol;
    }
    private static void CircleColliderReturn(CircleCollider2D circle2D) {
        float circleRadius = circle2D.radius;
        circleRadius = circleRadius * ((100 / scale) / 100);
        circle2D.radius = circleRadius;
    }
    private static void PolygonColliderReturn(PolygonCollider2D polygon2D) {
        Vector2[] pointsOfCollider = polygon2D.points;

        for (int i = 0; i < pointsOfCollider.Length; i++) {
            pointsOfCollider[i].x = pointsOfCollider[i].x * ((100 / scale) / 100);
            pointsOfCollider[i].y = pointsOfCollider[i].y * ((100 / scale) / 100);
        }

        polygon2D.points = pointsOfCollider;
    }
}
