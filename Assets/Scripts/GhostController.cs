using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
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

        private int faceState = 3;
        private float stunTime;
        private AudioSource audioSource;
        private Vector3 originalLocalPosition;
        private FirstPersonController player;
        private MeshRenderer meshRenderer;
        private float nextPhaseTime;
        private Coroutine currentCoroutine;

        // Start is called before the first frame update
        void Start()
        {
            originalLocalPosition = transform.localPosition;
            player = FirstPersonController.Instance;
            meshRenderer = GetComponent<MeshRenderer>();
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (stunTime < Time.time)
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

        private void CheckScareDistance(float distance)
        {
            if (faceState != 0 && nextPhaseTime < Time.time && distance < scareDistance)
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
            transform.position = tpPosition;
            
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
                    currentCoroutine = StartCoroutine(Evanescence(meshRenderer, phaseTime, meshRenderer.material.color, targetColor));
                    break;
                case 2:
                    faceState = faceIndex;
                    targetColor.a = 1;
                    meshRenderer.material.color = targetColor;
                    meshRenderer.material.mainTexture = faces[faceIndex];
                    audioSource.Stop();
                    audioSource.PlayOneShot(cryAudioClip, 0.4f);
                    break;
                case 3:
                    faceState = faceIndex;
                    targetColor.a = 1;
                    meshRenderer.material.color = targetColor;
                    meshRenderer.material.mainTexture = faces[faceIndex];
                    audioSource.Play();
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

        public void StunGhost()
        {
            if (stunTime < Time.time)
            {
                stunTime = Time.time + stunDuration;
                SetGhostFace(2);
                StartCoroutine(UnStun(stunDuration));
            }
        }
    }
}