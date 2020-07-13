using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfusionEdutainment.Objects
{
    [CreateAssetMenu(fileName = "new Chapter", menuName = "InfusionEdutainment/Chapter")]
    public class ChapterObjects : ScriptableObject
    {
        public List<VocabObj> vocabObj;

        public string getRandomName(string exclude)
        {
            int index = Random.Range(0, vocabObj.Count);
            string s = vocabObj[index].name;
            if (s == exclude)
            {
                int mid = s.Length / 2;
                string s1 = s.Substring(Random.Range(0, mid));
                string s2 = s.Substring(Random.Range(mid, s.Length));

                s = s1 + s2;
                s = s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1);
            }

            return s;
        }
    }
}