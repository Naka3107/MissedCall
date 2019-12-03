using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour {

    private const float MESSAGE_HEIGHT = -100.0f;

    public enum Sender {
        PLAYER,
        NPC
    }

    private GameObject viewport;
    private GameObject received;
    private GameObject sent;
    private float currentHeight;

    public ConversationManager (GameObject viewport, GameObject received, GameObject sent) {
        this.viewport = viewport;
        this.received = received;
        this.sent = sent;
        currentHeight = MESSAGE_HEIGHT;
    }

    public void ShowMessage (Sender who, Phrase message) {
        GameObject instance = null;

        switch (who) {
            case Sender.PLAYER:
                instance = Instantiate (sent) as GameObject;
                break;

            case Sender.NPC:
                instance = Instantiate (received) as GameObject;
                break;
        }

        instance.transform.gameObject.SetActive (true);
        instance.transform.SetParent (viewport.transform, false);
        instance.transform.position = new Vector3 (instance.transform.position.x, instance.transform.position.y + currentHeight, instance.transform.position.z);

        Text txt = instance.transform.gameObject.GetComponentInChildren<Text> (true);
        txt.text = message.ToString ();

        currentHeight += MESSAGE_HEIGHT;
    }
}

public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject m_ConversationUI;
    [SerializeField] private GameObject m_Viewport;
    [SerializeField] private GameObject m_ReceivedMessage;
    [SerializeField] private GameObject m_SentMessage;
    [SerializeField] private AudioClip m_Notification;

    private ConversationManager conversationManager;
    private Conversation conversation;
    Node currentMessage;
    private GameObject player;
    private AudioSource audioSource;

    public static bool hasKey = false;

    // Start is called before the first frame update
    void Start () {
        conversationManager = new ConversationManager (m_Viewport, m_ReceivedMessage, m_SentMessage);
        conversation = null;
        player = GameObject.Find ("/Player").gameObject;
        audioSource = player.GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update () {
        if (conversation != null && conversation.IsActive ()) {
            Node posible = conversation.Update ();

            if (posible != null) {
                currentMessage = posible;

                PlayNotification ();
                conversationManager.ShowMessage (ConversationManager.Sender.NPC, currentMessage.GetConsequence ());
                int i = 0;
                foreach (Node son in currentMessage.GetAllSons ()) {
                    conversationManager.ShowMessage (ConversationManager.Sender.PLAYER, son.GetTrigger ());
                    i++;
                }
            }
        }
    }

    public void StartConversation (Conversation conversation) {
        this.conversation = conversation;
        currentMessage = conversation.Start ();

        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
        player.GetComponentInChildren<RaycastInteraction> ().enabled = false;

        m_ConversationUI.SetActive (true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        PlayNotification ();
        conversationManager.ShowMessage (ConversationManager.Sender.NPC, currentMessage.GetConsequence ());
        int i = 0;
        foreach (Node son in currentMessage.GetAllSons ()) {
            conversationManager.ShowMessage (ConversationManager.Sender.PLAYER, son.GetTrigger ());
            i++;
        }
    }

    public void CheckConversationProgress () {
        if (!conversation.IsActive ()) {
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
            player.GetComponentInChildren<RaycastInteraction> ().enabled = true;

            m_ConversationUI.SetActive (false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public bool HasConversationEnded () {
        return conversation.HasEnded ();
    }

    void PlayNotification () {
        audioSource.clip = m_Notification;
        audioSource.Play ();
    }
}