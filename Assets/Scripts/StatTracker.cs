using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfusionEdutainment.Controllers
{
    public class StatTracker : MonoBehaviour
    {
        public static StatTracker Instance;

        private int gatesOpened;

        private Dictionary<string, int> correctVocabs = new Dictionary<string, int>();

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

        public void AddCorrectVocab(string vocab)
        {
            gatesOpened++;
            if (correctVocabs.ContainsKey(vocab)) {
                correctVocabs[vocab] = correctVocabs[vocab] + 1;
            } else
            {
                correctVocabs.Add(vocab, 1);
            }
            Debug.Log(gatesOpened);
        }
    }
}