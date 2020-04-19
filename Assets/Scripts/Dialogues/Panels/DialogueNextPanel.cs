using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueNextPanel : DialoguePanel
{
    public Button next;
    public Button cancel;
    public Text textArea;

    public DialogueNextPanel preset(DialoguePanel next,DialogueController controller,Color color,Color textColor,string text) {
        DialogueNextPanel panel = Instantiate(this,Vector3.zero,Quaternion.identity,controller.transform);
        panel.gameObject.SetActive(false);
        controller.Attach(panel);
        panel.next.onClick.AddListener(delegate
        {
            controller.Discard(panel);
            controller.Show(next);
        });
        panel.cancel.onClick.AddListener(delegate
        {
            controller.Cancel();
        });
        panel.textArea.text = text;
        panel.textArea.color = textColor;
        Image[] images = panel.GetComponentsInChildren<Image>(true);
        foreach (Image i in images)
        {
            i.material.color = color;
        }

        return panel;
    }


}
