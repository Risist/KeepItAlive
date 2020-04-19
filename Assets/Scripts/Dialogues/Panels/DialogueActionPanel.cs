using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueActionPanel : DialoguePanel
{
    public Button next;
    public Button cancel;
    public Text textArea;

    public DialogueActionPanel preset(UnityEvent action,DialogueController controller, Color color, Color textColor, string text)
    {
        DialogueActionPanel panel = Instantiate(this, Vector3.zero, Quaternion.identity, controller.transform);
        panel.gameObject.SetActive(false);
        controller.Attach(panel);

        panel.next.onClick.AddListener(delegate
        {
            controller.Discard(panel);
            action.Invoke();
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