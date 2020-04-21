using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerHud : MonoBehaviour
{
    public Image fillFireLitLeft;
    public Image fillFireLitRight;
    public Image fillFireLitHealth;
    GameObject player;

    LightFadeout[] fadeout;

    
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var hp = player.GetComponent<HealthController>();
        hp.onDamageCallback += (data) =>
        {
            fillFireLitHealth.fillAmount = hp.currentHealth/ hp.maxHealth;
        };

        fadeout = player.GetComponentsInChildren<LightFadeout>();
    }

    void Update()
    {
        fillFireLitLeft.fillAmount = fadeout[0].fadeoutPercent;
        fillFireLitRight.fillAmount = fadeout[1].fadeoutPercent;

        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
