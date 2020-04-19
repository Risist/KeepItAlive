using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SplitNode#",menuName ="Dialogue/SplitNode")]
public class DialogueSplit : DialogueNode
{
    public string text;
    public DialogueNode optionA;
    public string optionAText;
    public DialogueNode optionB;
    public string optionBText;

    public override DialoguePanel createSubtree(DialogueController controller,Color color,Color textColor,DialogueNextPanel dnp, DialogueSplitPanel dsp, DialogueActionPanel dap)
    {
        DialoguePanel optionAPanel = optionA.createSubtree(controller, color, textColor, dnp, dsp, dap);
        DialoguePanel optionBPanel = optionB.createSubtree(controller, color, textColor, dnp, dsp, dap);

        return dsp.preset(optionAPanel, optionBPanel, controller, color, textColor, text, optionAText, optionBText);
    }
}
