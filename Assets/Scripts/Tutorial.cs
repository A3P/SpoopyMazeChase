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
        public Texture[] stunGhostTutorial;

        public float tutorialDuration;
        public float timeTriggerIncrease;
        public bool tutorialsActive;

        public FlashLightController flashLight;
        public float flashLightTimeTrigger;

        private int deathCount;
        private Coroutine currentCoroutine;
        private RawImage image;
        private Queue<Texture[]> tutorialsQueue;
        // Start is called before the first frame update
        void Start()
        {
            tutorialsQueue = new Queue<Texture[]>();
            tutorialsActive = true;
            image = gameObject.GetComponent<RawImage>();
            AddToQueue(movementTutorial);
        }

        // Update is called once per frame
        void Update()
        {
            if ((Time.time - flashLight.lastTimeUsed) > flashLightTimeTrigger && currentCoroutine == null) {
                AddToQueue(flashLightTutorial);
                AddToQueue(stunGhostTutorial);
                flashLightTimeTrigger += timeTriggerIncrease;
            }
            ShowNextTutorial();
        }

        public void SetTutorialsActive(bool activeStatus)
        {
            tutorialsActive = activeStatus;
        }

        private void ShowNextTutorial()
        {
            if (image.enabled == false && tutorialsQueue.Count != 0)
            {
                currentCoroutine = StartCoroutine(ShowTutorial(tutorialsQueue.Dequeue()));
            }
        }

        private void AddToQueue(Texture[] tutorial)
        {
            tutorialsQueue.Enqueue(tutorial);
        }

        private IEnumerator ShowTutorial(Texture[] tutorials)
        {
            if (tutorialsActive)
            {
                Debug.Log("Show Tutorial");
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
}