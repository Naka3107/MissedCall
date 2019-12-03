using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phrase {
    private string Data;

    public Phrase (string Data) {
        this.Data = Data;
    }

    public string ToString () {
        return Data;
    }
}

public class Node {
    private Phrase Trigger, Consequence;
    private List<Node> Children;
    private bool final;

    public Node (Phrase Trigger, Phrase Consequence, bool final = false) {
        this.Trigger = Trigger;
        this.Consequence = Consequence;
        this.Children = new List<Node> ();
        this.final = final;
    }

    public Phrase GetTrigger () {
        return Trigger;
    }

    public Phrase GetConsequence () {
        return Consequence;
    }

    public void AddSon (Node son) {
        Children.Add (son);
    }

    public Node GetSon (int i) {
        return Children[i];
    }

    public List<Node> GetAllSons () {
        return Children;
    }

    public bool isFinal () {
        return final;
    }

    public string ToString () {
        return "Trigger: " + Trigger.ToString () +
            "\n" + "Consequence: " + Consequence.ToString () +
            "\n" + "Children: " + Children.Count.ToString ();
    }
}

public class Conversation {
    private Node root;
    private Node currentMessage;
    private bool active;
    private bool hasEnded;

    public Conversation (Node root) {
        this.root = root;
        this.currentMessage = root;
        this.active = false;
        this.hasEnded = false;
    }

    public Node Start () {
        active = true;
        return currentMessage;
    }

    public bool IsActive () {
        return active;
    }

    public bool HasEnded () {
        return hasEnded;
    }

    public Node Update () {
        if (!active) return null;

        if (Input.GetKeyDown ("0")) {
            currentMessage = currentMessage.GetSon (0);
        } else if (Input.GetKeyDown ("1")) {
            currentMessage = currentMessage.GetSon (1);
        } else if (Input.GetKeyDown ("2")) {
            currentMessage = currentMessage.GetSon (2);
        } else {
            return null;
        }

        if (currentMessage.isFinal ()) {
            active = false;
            hasEnded = true;
        }

        return currentMessage;
    }
}