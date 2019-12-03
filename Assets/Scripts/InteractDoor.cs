using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : MonoBehaviour {
    [SerializeField] private float ySensitivity = 300f;
    [SerializeField] private float openLowerLimit = 180;
    [SerializeField] private float openUpperLimit = 250;
    [SerializeField] private bool isOpen = false;

    [SerializeField] private GameObject frontDoorCollider;
    [SerializeField] private GameObject backDoorCollider;

    [SerializeField] private AudioClip m_Locked;
    [SerializeField] private AudioClip m_Open;
    [SerializeField] private AudioClip m_Close;
    private AudioSource m_AudioSource;

    private bool moveDoor = false;
    private DoorCollision doorCollision = DoorCollision.NONE;
    private float yRot = 0;

    void PlaySound (AudioClip sound) {
        m_AudioSource.loop = false;
        m_AudioSource.clip = sound;
        m_AudioSource.pitch = Random.Range (0.8f, 1.2f);
        m_AudioSource.Play ();
    }

    void PlayLoopedSound (AudioClip sound) {
        m_AudioSource.loop = true;
        m_AudioSource.clip = sound;
        m_AudioSource.pitch = Random.Range (0.8f, 1.2f);
        m_AudioSource.Play ();
    }

    void UpdateDoorStatus () {
        isOpen = GameManager.hasKey || isOpen;
    }

    // Use this for initialization
    void Start () {
        m_AudioSource = GetComponent<AudioSource> ();
        StartCoroutine (doorMover ());
    }

    // Update is called once per frame
    void Update () {
        UpdateDoorStatus ();

        if (Input.GetMouseButtonDown (0)) {
            RaycastHit[] hits;

            hits = Physics.RaycastAll (Camera.main.ScreenPointToRay (Input.mousePosition), 2.0f);

            for (int i = 0; i < hits.Length; i++) {
                RaycastHit hitInfo = hits[i];

                if (hitInfo.collider.gameObject == frontDoorCollider) {
                    moveDoor = true;
                    doorCollision = DoorCollision.FRONT;

                    if (!isOpen) PlaySound (m_Locked);
                    else if (!m_AudioSource.isPlaying) PlayLoopedSound (m_Open);

                } else if (hitInfo.collider.gameObject == backDoorCollider) {
                    moveDoor = true;
                    doorCollision = DoorCollision.BACK;

                    if (!isOpen) PlaySound (m_Locked);
                    else if (!m_AudioSource.isPlaying) PlayLoopedSound (m_Open);

                } else {
                    doorCollision = DoorCollision.NONE;
                }
            }
        }

        if (Input.GetMouseButtonUp (0)) {
            moveDoor = false;
            if (m_AudioSource.clip == m_Open) m_AudioSource.Pause ();
        }
    }

    IEnumerator doorMover () {
        bool stoppedBefore = false;

        while (true) {
            if (moveDoor && isOpen) {
                Camera.main.GetComponentInParent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
                stoppedBefore = false;

                yRot = transform.localEulerAngles.y;
                float yMov = Input.GetAxis ("Mouse X") * ySensitivity * Time.deltaTime;

                //Check if this is front door or back
                if (doorCollision == DoorCollision.FRONT) {
                    if ((Input.GetAxis ("Mouse X") < 0 && yRot < openUpperLimit) || (Input.GetAxis ("Mouse X") > 0 && yRot > openLowerLimit)) {
                        yRot -= yMov;

                        if (yRot > openUpperLimit) {
                            yRot = openUpperLimit;
                        } else if (yRot < openLowerLimit) {
                            yRot = openLowerLimit;
                            PlaySound (m_Close);
                        } else if (yMov < 1.0 && m_AudioSource.isPlaying) m_AudioSource.Pause ();
                        else if (!m_AudioSource.isPlaying) m_AudioSource.UnPause ();

                        transform.localEulerAngles = new Vector3 (0, yRot, 0);

                    }
                } else if (doorCollision == DoorCollision.BACK) {
                    if ((Input.GetAxis ("Mouse X") > 0 && yRot < openUpperLimit) || (Input.GetAxis ("Mouse X") < 0 && yRot > openLowerLimit)) {
                        yRot += yMov;

                        if (yRot > openUpperLimit) {
                            yRot = openUpperLimit;
                        } else if (yRot < openLowerLimit) {
                            yRot = openLowerLimit;
                            PlaySound (m_Close);
                        } else if (yMov < 1.0 && m_AudioSource.isPlaying) m_AudioSource.Pause ();
                        else if (!m_AudioSource.isPlaying) m_AudioSource.UnPause ();

                        transform.localEulerAngles = new Vector3 (0, yRot, 0);

                    }
                }
            } else {
                if (!stoppedBefore) {
                    Camera.main.GetComponentInParent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
                    stoppedBefore = true;
                }
            }

            yield return null;
        }

    }

    enum DoorCollision {
        NONE,
        FRONT,
        BACK
    }

}