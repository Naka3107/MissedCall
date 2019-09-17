using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : MonoBehaviour
{
    protected Vector3 posLastFrame;

    // Start is called before the first frame update
    void Start()
    {
        posLastFrame = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Moves object according to mouse movement
        //if (Input.GetMouseButtonDown(0))
        //{
        //    posLastFrame = Input.mousePosition;
        //}

        if (Input.GetMouseButton(0))
        {
            var delta = Input.mousePosition - posLastFrame;
            posLastFrame = Input.mousePosition;

            var axis = Quaternion.AngleAxis(90.0f, Vector3.forward) * delta;
            transform.rotation = Quaternion.AngleAxis(delta.magnitude * 0.5f, new Vector3(0.0f, 1.0f, 0.0f)) * transform.rotation;
        }
    }

}