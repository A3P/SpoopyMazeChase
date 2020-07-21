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
        public float batteryDrain;
        public BatteryUI battery;

        private float charge;
        private bool isMoving = false;
        // Start is called before the first frame update
        void Start()
        {
            charge = 1f;
        }

        // Update is called once per frame
        void Update()
        {
            DrainBattery();
        }

        public void DrainBattery()
        {
            if (gameObject.activeInHierarchy && charge > 0)
            {
                charge -= Time.deltaTime * batteryDrain;
                if (charge < 0)
                {
                    charge = 0f;
                }
                battery.SetChargeLevel(charge);
            }   
        }

        public void ToggleFlashLight()
        {
            if (!isMoving)
            {
                Vector3 targetPosition = transform.localPosition;
                if(gameObject.activeInHierarchy)
                {
                    targetPosition.y -= moveDistance;
                    StartCoroutine(MoveFlashLight(lerpDuration, transform.localPosition, targetPosition, true));
                } else
                {
                    gameObject.SetActive(true);
                    targetPosition.y += moveDistance;
                    StartCoroutine(MoveFlashLight(lerpDuration, transform.localPosition, targetPosition, false));
                }
            }
        }

        private IEnumerator MoveFlashLight(float lerpDuration, Vector3 startPosition, Vector3 targetPosition, Boolean hideObject)
        {
            bool isMoving = true;
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
                gameObject.SetActive(false);
            }
            yield break;
        }

    }
}