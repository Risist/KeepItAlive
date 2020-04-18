using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripWire : MonoBehaviour
{
    public GameObject portStart;
    public GameObject portEnd;
    public LineRenderer lineRenderer;
    public bool tripped = false;
    public LayerMask maskLayers;

    public void Start() {
        lineRenderer.positionCount = 2;
    }
    public void Update() {
        if (!tripped)
        {

            Vector3 origin = portStart.transform.position;
            Vector3 direction = portEnd.transform.position - portStart.transform.position;
            lineRenderer.SetPosition(0, portStart.transform.position);
            lineRenderer.SetPosition(1, portEnd.transform.position);
            if (Physics2D.Raycast(origin, direction, direction.magnitude,maskLayers))
            {
                tripped = true;

            }


        }
        else {
            Vector3 origin = portStart.transform.position;
            Vector3 direction = portEnd.transform.position - portStart.transform.position;
            lineRenderer.SetPosition(0, portStart.transform.position);
            lineRenderer.SetPosition(1, portStart.transform.position);
        }
    }

}
