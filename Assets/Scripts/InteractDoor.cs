using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : MonoBehaviour
{
    public float ySensitivity = 300f;
    public float openLowerLimit = 180;
    public float openUpperLimit = 250;

    public GameObject frontDoorCollider;
    public GameObject backDoorCollider;

    bool moveDoor = false;
    DoorCollision doorCollision = DoorCollision.NONE;
    float yRot = 0;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(doorMover());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 2.0f))
            {
                if (hitInfo.collider.gameObject == frontDoorCollider)
                {
                    moveDoor = true;
                    //Debug.Log("Front door hit");
                    doorCollision = DoorCollision.FRONT;
                }
                else if (hitInfo.collider.gameObject == backDoorCollider)
                {
                    moveDoor = true;
                    //Debug.Log("Back door hit");
                    doorCollision = DoorCollision.BACK;
                }
                else
                {
                    doorCollision = DoorCollision.NONE;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            moveDoor = false;
            //Debug.Log("Mouse up");
        }
    }

    IEnumerator doorMover()
    {
        bool stoppedBefore = false;

        while (true)
        {
            if (moveDoor)
            {
                stoppedBefore = false;
                //Debug.Log("Moving Door");

                yRot = transform.localEulerAngles.y;
                //Debug.Log("YRotation: " + yRot);
                //Debug.Log("Mouse: " + Input.GetAxis("Mouse X"));

                //Check if this is front door or back
                if (doorCollision == DoorCollision.FRONT)
                {
                    //Debug.Log("Pull Up(PUSH AWAY)");
                    if ((Input.GetAxis("Mouse X") < 0 && yRot < openUpperLimit) || (Input.GetAxis("Mouse X") > 0 && yRot > openLowerLimit))
                    {
                        yRot -= Input.GetAxis("Mouse X") * ySensitivity * Time.deltaTime;
                        //Debug.Log(yRot);
                        transform.localEulerAngles = new Vector3(0, yRot, 0);
                    }

                }
                else if (doorCollision == DoorCollision.BACK)
                {
                    //Debug.Log("Pull Up(PUSH AWAY)");
                    if ((Input.GetAxis("Mouse X") > 0 && yRot < openUpperLimit)||(Input.GetAxis("Mouse X") < 0 && yRot > openLowerLimit))
                    {
                        yRot += Input.GetAxis("Mouse X") * ySensitivity * Time.deltaTime;
                        //Debug.Log(yRot);
                        transform.localEulerAngles = new Vector3(0, yRot, 0);
                    }
                    
                } 
            }
            else
            {
                if (!stoppedBefore)
                {
                    stoppedBefore = true;
                    //Debug.Log("Stopped Moving Door");
                }
            }

            yield return null;
        }

    }


    enum DoorCollision
    {
        NONE, FRONT, BACK
    }


}
