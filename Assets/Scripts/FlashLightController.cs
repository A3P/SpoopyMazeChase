using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfusionEdutainment.Controllers
{
    public class FlashLightController : MonoBehaviour
    {
        public float moveDistance;
        public float lerpDuration;
        public float drainRate;
        public float chargeRate;
        public BatteryUI battery;
        public Light spotLight;
        public float raycastDistance;
        public float raycastRadius;
        public float lastTimeUsed;

        private Renderer flashLightRenderer;
        private float charge;
        private bool isMoving = false;
        private int ghostLayerMask = 1 << 8;


        // Start is called before the first frame update
        void Start()
        {
            charge = 1f;
            flashLightRenderer = GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateBattery();
            if(flashLightRenderer.enabled)
                GhostRayCast(); 
        }

        // Raycast seeking ghost
        private void GhostRayCast()
        {
            Vector3 p1 = transform.position;
            Vector3 p2 = p1 + transform.forward * raycastDistance;
            RaycastHit[] hits = Physics.CapsuleCastAll(p1, p2, raycastRadius, transform.forward, raycastDistance, ghostLayerMask);
            foreach(RaycastHit hit in hits)
            {
                hit.transform.gameObject.GetComponent<GhostController>().StunGhost();
            }
        }

        private void UpdateBattery()
        {
            if (flashLightRenderer.enabled)
            {
                charge -= Time.deltaTime * drainRate;
                
                if (charge <= 0)
                {
                    charge = 0f;
                    ToggleFlashLight(false);
                }
            } else
            {
                charge += Time.deltaTime * chargeRate;
                if(charge > 1f)
                {
                    charge = 1f;
                }
            }
            battery.SetChargeLevel(charge);
        }

        public void ToggleFlashLight(bool setTime)
        {
            if (setTime)
                lastTimeUsed = Time.time;
            if (!isMoving)
            {
                Vector3 targetPosition = transform.localPosition;
                if(flashLightRenderer.enabled)
                {
                    targetPosition.y -= moveDistance;
                    StartCoroutine(MoveFlashLight(lerpDuration, transform.localPosition, targetPosition, true));
                } else if (charge > 0)
                {
                    SetVisibility(true);
                    targetPosition.y += moveDistance;
                    StartCoroutine(MoveFlashLight(lerpDuration, transform.localPosition, targetPosition, false));
                }
            }
        }

        private IEnumerator MoveFlashLight(float lerpDuration, Vector3 startPosition, Vector3 targetPosition, Boolean hideObject)
        {
            isMoving = true;
            float lerpStart_Time = Time.time;
            float lerpProgress;
            while (isMoving)
            {
                yield return new WaitForEndOfFrame();
                lerpProgress = Time.time - lerpStart_Time;
                transform.localPosition = Vector3.Lerp(startPosition, targetPosition, lerpProgress / lerpDuration);
                if(lerpProgress >= lerpDuration)
                {
                    isMoving = false;
                }
            }
            if (hideObject)
            {
                SetVisibility(false);
            }
            yield break;
        }

        private void SetVisibility(bool visible)
        {
            flashLightRenderer.enabled = visible;
            spotLight.gameObject.SetActive(visible);
        }

        public void SetLastTimeUsed()
        {
            lastTimeUsed = Time.time;
        }
    }
}