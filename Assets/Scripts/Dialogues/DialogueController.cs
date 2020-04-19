using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueController : MonoBehaviour
{

    public DialogueRoot tree;
    public Canvas canvas;
    public GameObject player;
    public float triggerDist;

    public DialogueNextPanel nextPanelPrefab;
    public DialogueSplitPanel splitPanelPrefab;
    public DialogueActionPanel actionPanelPrefab;

    [SerializeField] Color textcolor;
    [SerializeField] Color bgcolor;

    public Color textColor { get { return textcolor; } }  
    public Color background { get { return bgcolor; } }

    GameObject currentlyShown;
    void Start() {
        tree.createSubtree(this, background, textColor, nextPanelPrefab, splitPanelPrefab, actionPanelPrefab);
    }
    // Update is called once per frame
    void Update()
    {
        if (currentlyShown)
            currentlyShown.SetActive(((Vector2)player.transform.position - (Vector2)transform.position).magnitude < triggerDist);
    }
    public void Attach(DialoguePanel dp) {
        dp.transform.SetParent(canvas.transform,false);
        dp.transform.localPosition = Vector3.zero;
    }
    public void Show(DialoguePanel next)
    {
        currentlyShown = next.gameObject;
    }

    internal void Discard(DialoguePanel panel)
    {
        Destroy(panel.gameObject);
    }

    internal void Cancel()
    {
        GetComponentsInChildren<DialoguePanel>().Select(q => { Destroy(q.gameObject); return true; });

    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, triggerDist);
    }
}
