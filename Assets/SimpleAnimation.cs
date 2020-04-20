using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimation : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Sprite[] animations;
    float timePerFrame;

    public void toAnimationFrame(int index) {
        sprite.sprite = animations[(animations.Length + (index %animations.Length)) % animations.Length];
    }

}
