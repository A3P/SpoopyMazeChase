using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using InfusionEdutainment.Controllers;

namespace InfusionEdutainment.Objects
{
    /// <summary>
    /// Shootable vocabulary.
    /// </summary>
    public class VocabObj : MonoBehaviour
    {
        public MeshFilter meshFilter;
        public Renderer vocabRenderer;
        public FirstPersonController player;
        public string correctAnswer;

        public ChapterObjects chapter;

        [SerializeField] int _hitValue = 0;
        public int HitValue { get { return _hitValue; } set { _hitValue = value; } }

        private bool _isHit = false;
        private float _timeAlive;

        [SerializeField] TextMeshPro text;

        void Awake()
        {
        }

        private void Start()
        {
            player = FirstPersonController.Instance;
        }

        private void OnEnable()
        {
            _timeAlive = 0;
        }

        private void Update()
        {
            this.transform.forward = player.transform.forward;
            float vocabDistance = Vector3.Distance(player.transform.position, transform.position);
            float gateDistance = Vector3.Distance(player.transform.position, transform.parent.transform.position);
            if(vocabDistance > gateDistance)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -transform.localPosition.z);
            }
        }

        public void SetName(string s)
        {
            if (s == null)
            {
                Debug.Log("name string is null");
                return;
            }

            text.text = s;
        }

        public string GetName()
        {
            return correctAnswer;
        }

        public void SetVocab(int index)
        {
            this.meshFilter.mesh = chapter.vocabObj[index].meshFilter.sharedMesh;
            this.vocabRenderer.material = chapter.vocabObj[index].vocabRenderer.sharedMaterial;

            correctAnswer = chapter.vocabObj[index].text.text;
            switch (GameController.Instance.difficulty)
            {
                case GameSettings.Difficulty.easy:
                    this.text.text = chapter.vocabObj[index].text.text;
                    break;
                
                case GameSettings.Difficulty.medium:

                    int prevID = index;
                    int id = UnityEngine.Random.Range(0, chapter.vocabObj.Count);

                    this.text.text = "";
                    int correctAnswerOrder = UnityEngine.Random.Range(0, 3);
                    for(int i = 0; i < 3; i++)
                    {
                        if (correctAnswerOrder == i)
                        {
                            text.text += chapter.vocabObj[index].text.text + "\n";
                        }
                        else
                        {
                            while (id == index || id == prevID)
                                id = UnityEngine.Random.Range(0, chapter.vocabObj.Count);

                            prevID = id;
                            text.text += chapter.vocabObj[id].text.text + "\n";
                        }
                    }

                    break;
                
                case GameSettings.Difficulty.hard:
                    text.gameObject.SetActive(false);
                    break;

                default:
                    break;
            }
        }
    }
}
