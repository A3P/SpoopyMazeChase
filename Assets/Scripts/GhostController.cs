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

        private FirstPersonController player;

        // Start is called before the first frame update
        void Start()
        {
            player = FirstPersonController.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(FirstPersonController.Instance.transform);
            // rotation to make up for the opposite forward vector of the model
            transform.localRotation *= Quaternion.Euler(0, 180, 0);
            if (Vector3.Distance(transform.position, player.transform.position) > minDistance)
            {
                // Subtraction instead of addition due to model
                transform.position -= transform.forward * moveSpeed * Time.deltaTime;
            } else
            {
                GameController.Instance.GameOver();
            }
        }
    }
}