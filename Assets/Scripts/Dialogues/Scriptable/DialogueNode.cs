using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueNode : ScriptableObject
{

    public abstract DialoguePanel createSubtree(DialogueController controller, Color color, Color textColor,DialogueNextPanel dnp, DialogueSplitPanel dsp, DialogueActionPanel dap);

}
