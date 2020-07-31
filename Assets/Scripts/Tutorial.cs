using InfusionEdutainment.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfusionEdutainment.Controllers
{
    public class Tutorial : MonoBehaviour
    {
        public Texture[] movementTutorial;
        public Texture[] flashLightTutorial;
        public float tutorialDuration;
        public float timeTriggerIncrease;

        public FlashLightController flashLight;
        public float flashLightTimeTrigger;

        private int deathCount;
        private Coroutine currentCoroutine;
        private RawImage image;
        // Start is called before the first frame update
        void Start()
        {
            image = gameObject.GetComponent<RawImage>();
            image.enabled = false;
            currentCoroutine = StartCoroutine(ShowTutorial(movementTutorial));
        }

        // Update is called once per frame
        void Update()
        {
            if ((Time.time - flashLight.lastTimeUsed) > flashLightTimeTrigger && currentCoroutine == null) {
                currentCoroutine = StartCoroutine(ShowTutorial(flashLightTutorial));
                flashLightTimeTrigger += timeTriggerIncrease;
            }
        }

        public IEnumerator ShowTutorial(Texture[] tutorials)
        {
            image.enabled = true;
            float startTime = Time.time;
            for (int i = 0; (Time.time - startTime) < tutorialDuration; i++)
            {
                i = i % tutorials.Length;
                image.texture = tutorials[i];
                yield return new WaitForSeconds(0.5f);
            }
            image.enabled = false;
            currentCoroutine = null;
            yield break;
        }
    }
}