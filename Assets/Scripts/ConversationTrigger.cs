using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTrigger : MonoBehaviour {

    public GameObject GameManager;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    void OnTriggerEnter (Collider other) {
        GameManager.GetComponent<GameManager> ().AdvanceConversation ();
    }

    void OnTriggerStay (Collider other) {
        GameManager.GetComponent<GameManager> ().CheckConversationProgress ();
    }
}