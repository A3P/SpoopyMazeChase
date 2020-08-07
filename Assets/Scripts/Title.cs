using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public Transform flashLight;
    public Image spotLight;
    public float flashLightDuration;
    public float spotLightOffset;
    public float flashLightRotation;

    public Image ghost;
    public float ghostOffset;
    public float ghostScale;

    // Start is called before the first frame update
    void Start()
    {
        EnterGhost();
        EnterFlashLight();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void EnterFlashLight()
    {
        Vector3 targetPosition = new Vector3(spotLight.transform.position.x, spotLight.transform.position.y + spotLightOffset, spotLight.transform.position.z);
        DOTween.To(() => flashLight.position, x => flashLight.position = x, targetPosition, flashLightDuration);
        spotLight.DOFade(1f, flashLightDuration);
        Vector3 rotation = new Vector3(0f, 0f, flashLightRotation);
        flashLight.DORotate(rotation, flashLightDuration, RotateMode.FastBeyond360);
    }

    private void EnterGhost()
    {
        float targetOffset = ghost.transform.position.x + ghostOffset;
        ghost.DOFade(1.0f, flashLightDuration);
        ghost.transform.DOMoveX(targetOffset, flashLightDuration);
        ghost.transform.DOScale(ghostScale, flashLightDuration);
    }
}
