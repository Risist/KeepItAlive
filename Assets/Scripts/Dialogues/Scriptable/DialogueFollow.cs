using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "PassNode#", menuName = "Dialogue/PassNode")]
public class DialogueFollow : DialogueNode
{
    public string text;
    public DialogueNode nextNode;
    public override DialoguePanel createSubtree(DialogueController controller, Color color, Color textColor, DialogueNextPanel dnp, DialogueSplitPanel dsp, DialogueActionPanel dap)
    {
        DialoguePanel panel = nextNode.createSubtree(controller, color, textColor, dnp, dsp, dap);
        return dnp.preset(panel, controller, color, textColor, text);
    }
}  
