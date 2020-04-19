using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Mechanism))]
public class TripWire : MonoBehaviour
{
    public GameObject portStart;
    public GameObject portEnd;
    public LineRenderer lineRenderer;
    public bool tripped = false;
    public LayerMask maskLayers;
    Mechanism mechanism;

    public void Start() {
        lineRenderer.positionCount = 2;
        mechanism = GetComponent<Mechanism>();

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
                mechanism.trip();
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
