using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Card : MonoBehaviour
{
    [SerializeField] internal GameObject CardBackground;
    [SerializeField] private Animator CardAnimator;
    [SerializeField] private AudioSource CardAudio;
    [SerializeField] internal Image CardFace;

    internal int CardID;

    public void OnMouseDown()
    {
        if (!GameManager.GameManagerInstance.paused && !GameManager.GameManagerInstance.gameOver)
        {
            if (CardBackground.activeSelf)
            {
                CardAudio.Play();
                CardAnimator.Play("Card1");
                GameManager.GameManagerInstance.Game(this);
            }
        }
    }

    internal void UnReaveal()
    {
        CardAnimator.Play("Card2");
    }

    internal void SetInvisiable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
