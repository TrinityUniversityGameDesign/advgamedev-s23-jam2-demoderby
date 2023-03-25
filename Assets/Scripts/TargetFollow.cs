using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    public GameObject target;
    public bool followTarget = true;

    private Vector3 offsetFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        offsetFromTarget = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget)
        {
            // Calculate the camera's new position
            Vector3 newPosition = target.transform.position - offsetFromTarget;

            // Move the camera to the new position
            transform.position = newPosition;

            transform.LookAt(target.transform);
        }
    }
}
