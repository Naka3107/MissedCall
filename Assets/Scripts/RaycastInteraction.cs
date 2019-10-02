using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteraction : MonoBehaviour {
  public float HitDistance = 1.0f;
  public Material OutlineMaterial;
  public GameObject InteractUI;

  private new Renderer renderer;
  private Material[] originalMaterials;
  private GameObject inspectedObject;
  private GameObject uiObject;
  private GameObject UIBackgoundCanvas;
  private float renderingDistance = 0.2f;

  bool m_IsExamining = false;
  bool isHit = false;

  // Start is called before the first frame update
  void Start () {
    renderer = null;

    UIBackgoundCanvas = InteractUI.transform.Find ("BackgroundCanvas").gameObject;
  }

  // Update is called once per frame
  void Update () {
    RaycastHit hit;

    // If hit interactables (Layer 8)
    if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit, HitDistance, 1 << 8)) {

      // Get the materials of the object and highlight it.
      Renderer currentRenderer = hit.collider.gameObject.GetComponent<Renderer> ();
      Material[] original = currentRenderer.materials;
      Material[] materialsWithHighlight = new Material[original.Length + 1];
      HighlightBorder (currentRenderer, original, materialsWithHighlight);

      // If it's a collectible, then allow examinating it.
      string tag = hit.collider.gameObject.tag;
      if (tag == "Collectible" || tag == "Readable") {
        Examinate (hit, original);
      }

      isHit = true;
      return;
    }

    // If you're no longer examining an object, reset the highlight of the object.
    if (isHit && !m_IsExamining) {
      isHit = false;
      renderer.materials = originalMaterials;
      renderer = null;
      return;
    }
  }

  void HighlightBorder (Renderer currentRenderer, Material[] original, Material[] highlight) {
    if (renderer == currentRenderer)
      return;

    if (currentRenderer && currentRenderer != renderer && renderer)
      renderer.materials = originalMaterials;

    if (currentRenderer)
      renderer = currentRenderer;
    else
      return;

    originalMaterials = original;
    for (int i = 0; i < original.Length; i++) {
      highlight[i] = original[i];
    }
    highlight[original.Length] = OutlineMaterial;
    renderer.sharedMaterials = highlight;
  }

  void Examinate (RaycastHit hit, Material[] original) {
    if (!m_IsExamining) {
      inspectedObject = hit.collider.gameObject;

      if (Input.GetMouseButtonDown (0)) {
        m_IsExamining = true;
        GetComponentInParent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;

        // Show backgound panel
        InteractUI.SetActive (true);

        // Generate an instance of the inspected object and set its material to the original one
        uiObject = Instantiate (inspectedObject);

        // Sets object in front of camera and aligns position and rotation
        uiObject.transform.SetParent (UIBackgoundCanvas.transform, true);
        uiObject.transform.SetPositionAndRotation (transform.position + transform.TransformDirection (Vector3.forward) * renderingDistance, transform.rotation);

        uiObject.GetComponent<Interactable> ().enabled = true;
      }
    } else {
      if (Input.GetKeyDown (KeyCode.Q)) {
        m_IsExamining = false;
        GetComponentInParent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
        Destroy (uiObject);

        // Hide backgound panel
        InteractUI.SetActive (false);

        InteractUI.transform.Find ("ForegroundCanvas").gameObject
          .transform.Find ("ReadPanel").gameObject
          .SetActive (false);
      }
    }
  }
}