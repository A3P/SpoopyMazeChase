using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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
            transform.LookAt(FirstPersonController.Instance.transform);
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > minDistance)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;

                phaseInOut(distance);
            } else
            {
                GameController.Instance.GameOver();
            }
        }

        private void phaseInOut(float distance)
        {
            Color phasedOutColor = meshRenderer.material.color;
            if (!phasedOut && nextPhaseTime < Time.time && distance < scareDistance)
            {
                phasedOut = true;
                audioSource.PlayOneShot(laughAudioClip, 0.2f);
                nextPhaseTime = Time.time + phaseIntervalTime + phaseTime;
                phasedOutColor.a = 0;
                meshRenderer.material.mainTexture = faces[0];
                StartCoroutine(Lerp_MeshRenderer_Color(meshRenderer, phaseTime, meshRenderer.material.color, phasedOutColor));
            }
            else if (phasedOut && nextPhaseTime < Time.time)
            {
                phasedOutColor.a = 1;
                meshRenderer.material.color = phasedOutColor;
                phasedOut = false;
                meshRenderer.material.mainTexture = faces[3];
                nextPhaseTime = Time.time + phaseIntervalTime;
            }
        }

        private IEnumerator Lerp_MeshRenderer_Color(MeshRenderer target_MeshRender, float lerpDuration, Color startLerp, Color targetLerp)
        {
            float lerpStart_Time = Time.time;
            float lerpProgress;
            bool lerping = true;
            while (lerping)
            {
                yield return new WaitForEndOfFrame();
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
                    float maxOffset = scareDistance / 2;
                    float xOffset = transform.position.x + Random.Range(-maxOffset, maxOffset);
                    float zOffset = transform.position.z + Random.Range(-maxOffset, maxOffset);
                    transform.position = new Vector3(xOffset, transform.position.y, zOffset);
                    lerping = false;
                }
            }
            yield break;
        }

    }
}