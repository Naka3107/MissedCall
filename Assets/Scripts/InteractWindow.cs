using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWindow : MonoBehaviour
{
    public float ySensitivity = 300f;
    public float openLowerLimit;
    public float openUpperLimit;

    public GameObject WindowCollider;

    bool moveWindow = false;
    WindowCollision windowCollision = WindowCollision.NONE;
    float xPos = 0;
    float yPos = 0;
    float zPos = 0;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WindowMover());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 2.0f))
            {
                if (hitInfo.collider.gameObject == WindowCollider)
                {
                    moveWindow = true;
                    //Debug.Log("Window hit");
                    windowCollision = WindowCollision.FRONT;
                }
                else
                {
                    windowCollision = WindowCollision.NONE;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            moveWindow = false;
            //Debug.Log("Mouse up");
        }
    }

    IEnumerator WindowMover()
    {
        bool stoppedBefore = false;

        while (true)
        {
            if (moveWindow)
            {
                Camera.main.GetComponentInParent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
                stoppedBefore = false;
               // Debug.Log("Moving Window");

                xPos = transform.localPosition.x;
                yPos = transform.localPosition.y;
                zPos = transform.localPosition.z;
                //Debug.Log("YPosition: " + yPos);
                //Debug.Log("Mouse: " + Input.GetAxis("Mouse Y"));

                //Check if this is front Window or back
                if (windowCollision == WindowCollision.FRONT)
                {
                   // Debug.Log("Pull Up(PUSH AWAY)");
                    if ((Input.GetAxis("Mouse Y") > 0 && yPos < openUpperLimit) || (Input.GetAxis("Mouse Y") < 0 && yPos > openLowerLimit))
                    {
                        yPos += Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
                        if (yPos > openUpperLimit)
                        {
                            yPos = openUpperLimit;
                        }
                        else if (yPos < openLowerLimit)
                        {
                            yPos = openLowerLimit;
                        }
                        //Debug.Log(yPos);
                        transform.localPosition = new Vector3 (xPos, yPos, zPos);
                    }
                }
            }
            else
            {
                if (!stoppedBefore)
                {
                    stoppedBefore = true;
                    Camera.main.GetComponentInParent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
                    //Debug.Log("Stopped Moving Window");
                }
            }

            yield return null;
        }

    }


    enum WindowCollision
    {
        NONE, FRONT
    }


}
