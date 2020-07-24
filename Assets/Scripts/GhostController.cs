using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

namespace InfusionEdutainment.Controllers
{
    public class GhostController : MonoBehaviour
    {
        public float moveSpeed;
        public float minDistance;
        public float scareDistance;
        public float phaseIntervalTime;
        public float phaseTime;
        public AudioClip laughAudioClip;
        public Texture[] faces;
        public float stunDuration;
        
        private float stunTime;
        private AudioSource audioSource;
        private Vector3 originalLocalPosition;
        private FirstPersonController player;
        private MeshRenderer meshRenderer;
        private float nextPhaseTime;
        private bool phasedOut = false;

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
                if (distance > minDistance)
                {
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    PhaseInOut(distance);
                }
                else
                {
                    GameController.Instance.GameOver();
                }
            }
        }

        private void PhaseInOut(float distance)
        {
            Color targetColor = meshRenderer.material.color;
            if (!phasedOut && nextPhaseTime < Time.time && distance < scareDistance)
            {
                phasedOut = true;
                audioSource.PlayOneShot(laughAudioClip, 0.2f);
                nextPhaseTime = Time.time + phaseIntervalTime + phaseTime;
                targetColor.a = 0;
                meshRenderer.material.mainTexture = faces[0];
                StartCoroutine(Lerp_MeshRenderer_Color(meshRenderer, phaseTime, meshRenderer.material.color, targetColor, true));
            }
            else if (phasedOut && nextPhaseTime < Time.time)
            {
                phasedOut = false;
                nextPhaseTime = Time.time + phaseIntervalTime + phaseTime;
                targetColor.a = 1;
                meshRenderer.material.mainTexture = faces[3];
                StartCoroutine(Lerp_MeshRenderer_Color(meshRenderer, phaseTime, meshRenderer.material.color, targetColor, false));
            }
        }

        private IEnumerator Lerp_MeshRenderer_Color(MeshRenderer target_MeshRender, float lerpDuration, Color startLerp, Color targetLerp, Boolean teleport)
        {
            Vector3 tpPosition = player.transform.position;
            float lerpStart_Time = Time.time;
            float lerpProgress;
            bool lerping = true;
            while (lerping && stunTime < Time.time)
            {
                lerpProgress = Time.time - lerpStart_Time;
                if (target_MeshRender != null)
                {
                    target_MeshRender.material.color = Color.Lerp(startLerp, targetLerp, lerpProgress / lerpDuration);
                }
                else
                {
                    lerping = false;
                }

                if (lerpProgress >= lerpDuration)
                {
                    if(teleport)
                        transform.position = tpPosition;
                    lerping = false;
                }
                yield return new WaitForEndOfFrame();
            }
            yield break;
        }

        private void SetGhostFace(int faceIndex)
        {
            Color targetColor = meshRenderer.material.color;
            switch (faceIndex)
            {
                case 0:
                    targetColor.a = 1;
                    meshRenderer.material.color = targetColor;
                    meshRenderer.material.mainTexture = faces[faceIndex];
                    break;
                case 2:
                    targetColor.a = 1;
                    meshRenderer.material.color = targetColor;
                    meshRenderer.material.mainTexture = faces[faceIndex];
                    break;
                default:
                    break;
            }
        }

        private IEnumerator UnStun(float time)
        {
            yield return new WaitForSeconds(time);
            SetGhostFace(0);
        }

        public void StunGhost()
        {
            if (stunTime < Time.time)
            {
                stunTime = Time.time + stunDuration;
                SetGhostFace(2);
                UnStun(stunDuration);
            }
        }
    }
}