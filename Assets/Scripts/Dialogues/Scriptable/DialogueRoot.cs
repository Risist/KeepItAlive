using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "RootNode#", menuName = "Dialogue/Tree")]
public class DialogueRoot : DialogueNode
{
    public DialogueNode nextNode;
    public override DialoguePanel createSubtree(DialogueController controller, Color color, Color textColor, DialogueNextPanel dnp, DialogueSplitPanel dsp, DialogueActionPanel dap)
    {
        DialoguePanel panel = nextNode.createSubtree(controller, color, textColor, dnp, dsp, dap);
        controller.Show(panel);

        return panel;
    }
}
