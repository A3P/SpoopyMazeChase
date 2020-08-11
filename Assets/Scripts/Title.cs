using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Transform flashLight;
    public Image spotLight;
    public float animationDuration;
    public float spotLightOffset;
    public float flashLightRotation;
    public Image flashLightLight;
    public float lightTransparency;
    public AudioClip lightToggleAudio;

    public Image ghost;
    public float ghostOffset;
    public float ghostScale;
    public Sprite cryingGhost;
    public AudioClip cryAudio;
    public AudioClip laughAudio;
    public Image titleGhost;
    public float titleOffset;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EnterGhost();
        StartCoroutine(EnterFlashLight());
    }
    
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    private IEnumerator EnterFlashLight()
    {
        Vector3 targetPosition = new Vector3(spotLight.transform.position.x, spotLight.transform.position.y + spotLightOffset, spotLight.transform.position.z);
        DOTween.To(() => flashLight.position, x => flashLight.position = x, targetPosition, animationDuration).SetEase<Tween>(Ease.OutCubic);
        spotLight.DOFade(1f, animationDuration).SetEase<Tween>(Ease.InCubic);
        Vector3 rotation = new Vector3(0f, 0f, flashLightRotation);
        Tween flashLightTween = flashLight.DORotate(rotation, animationDuration, RotateMode.FastBeyond360).SetEase<Tween>(Ease.OutCubic);
        yield return flashLightTween.WaitForCompletion();

        audioSource.PlayOneShot(lightToggleAudio, 0.2f);
        flashLightLight.DOFade(lightTransparency, 0.5f).SetEase<Tween>(Ease.InBounce);
        //Color lightColor = flashLightLight.color;
        //lightColor.a = lightTransparency;
        //flashLightLight.color = lightColor;
        StartCoroutine(EnterTitle());
        yield break;
    }

    private void EnterGhost()
    {
        audioSource.PlayOneShot(laughAudio, 0.1f);
        float targetOffset = ghost.transform.position.x + ghostOffset;
        ghost.DOFade(1.0f, animationDuration).SetEase<Tween>(Ease.InCubic);
        ghost.transform.DOMoveX(targetOffset, animationDuration).SetEase<Tween>(Ease.InCubic);
        ghost.transform.DOScale(ghostScale, animationDuration).SetEase<Tween>(Ease.InCubic);
    }

    private IEnumerator EnterTitle()
    {
        float targetOffset = ghost.transform.position.x - ghostOffset;
        ghost.transform.DOMoveX(targetOffset, animationDuration).SetEase<Tween>(Ease.InCubic);
        ghost.sprite = cryingGhost;
        audioSource.PlayOneShot(cryAudio, 0.1f);
        Tween fadeTween = ghost.DOFade(0f, animationDuration).SetEase<Tween>(Ease.InCubic);

        yield return fadeTween.WaitForCompletion();

        float targetTitleOffset = titleGhost.transform.position.x + titleOffset;
        titleGhost.DOFade(1f, animationDuration).SetEase<Tween>(Ease.InCubic);
        titleGhost.transform.DOMoveX(targetTitleOffset, animationDuration).SetEase<Tween>(Ease.InCubic);

        yield break;
    }
}
