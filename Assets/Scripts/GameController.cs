using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace InfusionEdutainment.Controllers
{
    public class GameController : MonoBehaviour
    {

        public static GameController Instance { get; private set; }
        public GameObject Panel;
        public GameObject ghost;
        
         private FirstPersonController player;

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
            player = FirstPersonController.Instance;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GameOver()
        {
            Debug.Log("You have been spooked");
            Panel.SetActive(true);
            player.gameObject.SetActive(false);
        }

        public void Victory()
        {
            Debug.Log("Victory!");
        }
    }
}