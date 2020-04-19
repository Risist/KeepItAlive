using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSplitPanel : DialoguePanel
{
    public Button optionA;
    public Button optionB;

    public Button cancel;
    public Text textArea;

    public DialogueSplitPanel preset(   DialoguePanel nextA,
                                        DialoguePanel nextB,
                                        DialogueController controler,
                                        Color color,
                                        Color textColor, 
                                        string text,
                                        string textA,
                                        string textB
    )
    {
        DialogueSplitPanel panel = Instantiate(this,Vector3.zero,Quaternion.identity,controler.transform);
        panel.gameObject.SetActive(false);
        controler.Attach(panel);
        panel.optionA.onClick.AddListener(delegate
        {
            controler.Discard(panel);
            controler.Show(nextA);

        });

        panel.optionB.onClick.AddListener(delegate
        {
            controler.Discard(panel);
            controler.Show(nextB);


        });
        panel.cancel.onClick.AddListener(delegate
        {
            controler.Cancel();

        });
        panel.optionA.GetComponentInChildren<Text>().text = textA;
        panel.optionA.GetComponentInChildren<Text>().color = textColor;
        panel.optionB.GetComponentInChildren<Text>().text = textB;
        panel.optionB.GetComponentInChildren<Text>().color = textColor;

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
