using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfusionEdutainment.Controllers
{
    public class GameController : MonoBehaviour
    {

        public static GameController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GameOver()
        {
            Debug.Log("You have been spooked");
        }
    }
}