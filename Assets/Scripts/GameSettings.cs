using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfusionEdutainment.Controllers
{
    public class GameSettings : MonoBehaviour
    {
        public Difficulty currentDifficulty;

        void Awake()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Settings");

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TestButton()
        {
            Debug.Log("Test Button");
        }

        public void SetDifficulty(int difficulty)
        {
            currentDifficulty = (Difficulty)difficulty;
        }

        public enum Difficulty
        {
            easy,
            medium,
            hard
        }
    }
}
