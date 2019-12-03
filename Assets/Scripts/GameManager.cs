using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject m_ConversationUI;
    [SerializeField] private GameObject m_ReceivedMessage;
    [SerializeField] private GameObject m_SentMessage;

    private Conversation conversation;
    private GameObject player;

    public static bool hasKey = false;

    // Start is called before the first frame update
    void Start () {
        conversation = new Conversation ();
        player = GameObject.Find ("/Player").gameObject;
    }

    // Update is called once per frame
    void Update () {
        conversation.Update ();
    }

    public void AdvanceConversation () {
        if (!conversation.Activate ()) {
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
            player.GetComponentInChildren<RaycastInteraction> ().enabled = false;

            m_ConversationUI.SetActive (true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void CheckConversationProgress () {
        if (!conversation.isActive ()) {
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
            player.GetComponentInChildren<RaycastInteraction> ().enabled = true;

            m_ConversationUI.SetActive (false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}