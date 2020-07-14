using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections;

namespace InfusionEdutainment.Objects
{
    /// <summary>
    /// Shootable vocabulary.
    /// </summary>
    public class VocabObj : MonoBehaviour
    {
        public MeshFilter meshFilter;
        public Renderer vocabRenderer;

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
        }

        private void OnEnable()
        {
            _timeAlive = 0;
        }

        private void Update()
        {

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
            return text.text;
        }

        public void SetVocab(int index)
        {
            Debug.Log(chapter.vocabObj[index].text.text);
            this.meshFilter.mesh = chapter.vocabObj[index].meshFilter.sharedMesh;
            this.vocabRenderer.material = chapter.vocabObj[index].vocabRenderer.sharedMaterial;
            this.text.text = chapter.vocabObj[index].text.text;
        }
    }
}
