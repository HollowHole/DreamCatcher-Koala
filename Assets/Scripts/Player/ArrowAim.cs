using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAim : MonoBehaviour
{
    public Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
    }


void Update()
    {
        Vector3 fixedEndPoint=transform.parent.position;

        Vector3 mousePos_s = Input.mousePosition;
 
        Vector3 mousePos_w = Camera.main.ScreenToWorldPoint(mousePos_s);
        mousePos_w.z=fixedEndPoint.z;

        dir = (mousePos_w - fixedEndPoint).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle < 0f)
            angle += 360f;
        else if(angle>=360f)
            angle-=360f;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
