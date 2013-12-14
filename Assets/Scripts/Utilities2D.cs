using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utilities2D
{

    public static Rect CameraBounds2D()
    {
        Rect rect = new Rect();

        float verticalDistance = Camera.main.orthographicSize;
        float horizontalDistance = Camera.main.orthographicSize * Camera.main.aspect;

        Vector3 pos = Camera.main.transform.position;
        rect.xMin = pos.x - horizontalDistance;
        rect.xMax = pos.x + horizontalDistance;
        rect.yMin = pos.y - verticalDistance;
        rect.yMax = pos.y + verticalDistance;

        return rect;
    }

}