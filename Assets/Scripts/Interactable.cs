using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
  public float Scale = 1.0f;
  protected Vector3 posLastFrame;
  private bool onInit;
  private float rotationSpeed = 5.0f;

  // Start is called before the first frame update
  void Start()
  {
    // Hides shadow now that the object is in inspection mode
    Renderer uiRender = GetComponent<Renderer>();
    uiRender.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

    // Sets material to non light responsive
    Material[] matArray = uiRender.materials;
    matArray[1] = null;
    uiRender.materials = matArray;
    uiRender.material.shader = Shader.Find("Unlit/Texture");

    transform.localScale *= Scale;

    // Makes object only a visible shape. Eliminates physical characteristics
    Rigidbody rb = GetComponent<Rigidbody>();
    Collider cd = GetComponent<Collider>();
    Destroy(rb);
    Destroy(cd);

    // Interaction lock boolean
    onInit = true;
  }

  // Update is called once per frame
  void Update()
  {
    // Locks interaction until first mouse click has been released
    if (onInit)
    {
      if (Input.GetMouseButtonUp(0))
      {
        onInit = false;
      }
      else
      {
        return;
      }
    }

    if (gameObject.tag == "Readable" && Input.GetKeyDown("space")) {
    }

    // Moves object according to mouse movement
    if (Input.GetMouseButtonDown(0))
    {
      posLastFrame = Input.mousePosition;

      // Unlocks mouse position
      Cursor.lockState = CursorLockMode.None;
    }

    if (Input.GetMouseButton(0))
    {
      float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
      float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;

      transform.Rotate(0.0f, -rotX, 0.0f, Space.Self);
      transform.Rotate(rotY, 0.0f, 0.0f, Space.Self);
    }

    if (Input.GetMouseButtonUp(0))
    {
      // Locks mouse position
      Cursor.lockState = CursorLockMode.Locked;
    }
  }
}
