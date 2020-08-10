using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace InfusionEdutainment.Controllers
{
    public class GameController : MonoBehaviour
    {

        public static GameController Instance { get; private set; }
        public PanelController panel;
        public GameObject ghost;
        public int deathCount;
        public Tutorial tutorial;

        public GameSettings.Difficulty difficulty;

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
            GameSettings settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<GameSettings>();
            difficulty = settings.currentDifficulty;
        }

        // Start is called before the first frame update
        void Start()
        {
            Cursor.visible = false;
            player = FirstPersonController.Instance;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GameOver()
        {
            deathCount++;
            panel.GameOver();
            EndGame();
        }

        public void Victory()
        {
            panel.Victory();
            EndGame();
        }

        public void EndGame()
        {
            Cursor.visible = true;
            tutorial.SetTutorialsActive(false);
            ghost.SetActive(false);
            panel.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
        }
    }
}