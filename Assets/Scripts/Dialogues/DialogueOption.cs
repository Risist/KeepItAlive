using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DialogueOption",menuName ="Dialogues/DialogueOption")]
public class DialogueOption : ScriptableObject
{
    public string buttonTextCancel;
    public string buttonTextNext;
    public string buttonTextAction;
    public DialogueOption follow;
    public UnityEngine.Events.UnityEvent action;


    void SpawnDialogue(Canvas canvas,Vector2 worldCoords)
    {
        Vector2 relativePosition = worldCoords - (Vector2) canvas.transform.position;


    }
}
