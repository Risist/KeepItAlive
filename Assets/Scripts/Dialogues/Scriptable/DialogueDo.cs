using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName ="ActionNode#",menuName ="Dialogue/DoNode")]
public class DialogueDo : DialogueNode
{
    public string text;
    public UnityEvent action;
    public override DialoguePanel createSubtree(DialogueController controller, Color color, Color textColor, DialogueNextPanel dnp, DialogueSplitPanel dsp, DialogueActionPanel dap)
    {
        return dap.preset(action, controller, color, textColor, text);
    }
}
