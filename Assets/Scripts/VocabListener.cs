using InfusionEdutainment.Objects;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class VocabListener : MonoBehaviour
{
    public ChapterObjects chapter;
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    private string[] keywords;
    // Start is called before the first frame update
    void Start()
    {
        SetVocabNames();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetVocabNames()
    {
        keywords = new string[chapter.vocabObj.Count];
        int index = 0;
        foreach (VocabObj vocab in chapter.vocabObj)
        {
            keywords[index++] = vocab.GetName();
            Debug.Log(keywords[index - 1]);
        }
    }
}
