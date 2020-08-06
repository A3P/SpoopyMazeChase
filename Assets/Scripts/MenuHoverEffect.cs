using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuHoverEffect : MonoBehaviour
{
    public float duration;
    public TMP_Text text;
    public Color normalColor;
    public Color hoverColor;
    public AudioClip soundEffect;
    public float volume;

    private Image image;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        audio = gameObject.GetComponent<AudioSource>();
    }

    public void OnHover()
    {
        DOTween.To(()=> image.fillAmount, x=> image.fillAmount = x, 1, duration);
        text.color = hoverColor;
        audio.PlayOneShot(soundEffect, volume);
    }

    public void OnExit()
    {
        DOTween.To(() => image.fillAmount, x => image.fillAmount = x, 0, duration);
        text.color = normalColor;
    }
}
