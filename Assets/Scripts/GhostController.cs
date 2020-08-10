using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

namespace InfusionEdutainment.Controllers
{
    public class GhostController : MonoBehaviour
    {
        public float moveSpeed;
        public float deathDistance;
        public float scareDistance;
        public float phaseIntervalTime;
        public float phaseTime;
        public AudioClip laughAudioClip;
        public AudioClip cryAudioClip;
        public Texture[] faces;
        public float stunDuration;
        public float maxHoverDistance;
        public float hoverDuration;
        public float headShakeDuration;
        public float headShakeDegree;
        public float headShakeDelta;

        private int faceState = 3;
        private AudioSource audioSource;
        private Vector3 originalPosition;
        private FirstPersonController player;
        private MeshRenderer meshRenderer;
        private float nextPhaseTime;
        private Coroutine currentCoroutine;

        // Start is called before the first frame update
        void Start()
        {
            originalPosition = transform.position;
            player = FirstPersonController.Instance;
            meshRenderer = GetComponent<MeshRenderer>();
            audioSource = GetComponent<AudioSource>();
            SetGhostFace(3);
        }

        // Update is called once per frame
        void Update()
        {
            if (faceState != 2)
            {
                transform.LookAt(FirstPersonController.Instance.transform);
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance > deathDistance)
                {
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    CheckScareDistance(distance);
                }
                else
                {
                    GameController.Instance.GameOver();
                }
            }
        }

        public void StunGhost()
        {
            if (faceState != 2)
            {
                SetGhostFace(2);
                StartCoroutine(UnStun(stunDuration));
            }
        }

        private IEnumerator Hover()
        {
            float lerpStart_Time = Time.time;
            float lerpProgress = Time.time - lerpStart_Time;
            float lerpTarget = maxHoverDistance;
            float currentHeight = transform.position.y;
            while (faceState != 2)
            {
                lerpProgress = Time.time - lerpStart_Time;
                float displacement = Mathf.Lerp(0, lerpTarget, lerpProgress/hoverDuration);
                transform.position = new Vector3(transform.position.x, currentHeight + displacement, transform.position.z);
                
                if (lerpProgress > hoverDuration)
                {
                    currentHeight = transform.position.y;
                    lerpStart_Time = Time.time;
                    if(lerpTarget > 0)
                    {
                        lerpTarget = (-maxHoverDistance)*2;
                    } else
                    {
                        lerpTarget = maxHoverDistance*2;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            yield break;
        }

        private IEnumerator HeadShake()
        {
            //Vector3 originVector = new Vector3(0.0F, 0.0f, 0.0F);
            Vector3 originVector = transform.localRotation.eulerAngles;
            Vector3 destinationVector = originVector;
            float currentDelta = headShakeDegree;
            destinationVector.y += headShakeDegree;
            float lerpStartTime = Time.time;
            float lerpProgress = Time.time - lerpStartTime;
            Quaternion from = Quaternion.Euler(originVector);
            Quaternion to = Quaternion.Euler(destinationVector);
            while (faceState == 2)
            {
                lerpProgress = Time.time - lerpStartTime;
                this.transform.localRotation = Quaternion.Lerp(from, to, lerpProgress/headShakeDuration);
                if (lerpProgress > headShakeDuration)
                {
                    lerpStartTime = Time.time;
                    originVector = transform.localRotation.eulerAngles;
                    originVector = destinationVector;
                    destinationVector.y += -currentDelta;
                    if(currentDelta > 0)
                    {
                        currentDelta = -currentDelta + headShakeDelta;
                        destinationVector.y += currentDelta;
                    } else
                    {
                        currentDelta = -currentDelta - headShakeDelta;
                        destinationVector.y += currentDelta;
                    }
                    from = Quaternion.Euler(originVector);
                    to = Quaternion.Euler(destinationVector);
                }
                yield return new WaitForEndOfFrame();
            }
            yield break;
        }

        private void CheckScareDistance(float distance)
        {
            if (GameSettings.Difficulty.easy != GameController.Instance.difficulty && faceState != 0 && nextPhaseTime < Time.time && distance < scareDistance)
            {
                SetGhostFace(0);
            }
        }

        private IEnumerator Evanescence(MeshRenderer target_MeshRender, float lerpDuration, Color startLerp, Color targetLerp)
        {
            Vector3 tpPosition = player.transform.position;
            float lerpStart_Time = Time.time;
            float lerpProgress = Time.time - lerpStart_Time;
            while (lerpProgress <= lerpDuration)
            {
                lerpProgress = Time.time - lerpStart_Time;
                target_MeshRender.material.color = Color.Lerp(startLerp, targetLerp, lerpProgress / lerpDuration);
                yield return new WaitForEndOfFrame();
            }
            transform.position = new Vector3(tpPosition.x, originalPosition.y, tpPosition.z);

            lerpStart_Time = Time.time;
            lerpProgress = Time.time - lerpStart_Time;
            while(lerpProgress <= lerpDuration)
            {
                lerpProgress = Time.time - lerpStart_Time;
                target_MeshRender.material.color = Color.Lerp(targetLerp, startLerp, lerpProgress / lerpDuration);
                yield return new WaitForEndOfFrame();
            }
            SetGhostFace(3);
            yield break;
        }

        private void SetGhostFace(int faceIndex)
        {
            if(currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            Color targetColor = meshRenderer.material.color;
            switch (faceIndex)
            {
                case 0:
                    faceState = faceIndex;
                    targetColor.a = 0;
                    nextPhaseTime = Time.time + phaseIntervalTime + phaseTime;
                    meshRenderer.material.mainTexture = faces[faceIndex];
                    audioSource.PlayOneShot(laughAudioClip, 0.2f);
                    currentCoroutine = StartCoroutine(Hover());
                    currentCoroutine = StartCoroutine(Evanescence(meshRenderer, phaseTime, meshRenderer.material.color, targetColor));
                    break;
                case 2:
                    faceState = faceIndex;
                    targetColor.a = 1;
                    meshRenderer.material.color = targetColor;
                    meshRenderer.material.mainTexture = faces[faceIndex];
                    audioSource.Stop();
                    audioSource.PlayOneShot(cryAudioClip, 0.4f);
                    currentCoroutine = StartCoroutine(HeadShake());
                    break;
                case 3:
                    faceState = faceIndex;
                    targetColor.a = 1;
                    meshRenderer.material.color = targetColor;
                    meshRenderer.material.mainTexture = faces[faceIndex];
                    audioSource.Play();
                    currentCoroutine = StartCoroutine(Hover());
                    break;
                default:
                    Debug.Log("Did not set ghost face, hit default case in switch");
                    break;
            }
        }

        private IEnumerator UnStun(float time)
        {
            yield return new WaitForSeconds(time);
            SetGhostFace(3);
        }
    }
}