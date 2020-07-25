using System.Collections.Generic;
using InfusionEdutainment.Objects;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class VocabListener : MonoBehaviour
{
    public static VocabListener Instance { get; private set; }

    public ChapterObjects chapter;
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    protected PhraseRecognizer recognizer;

    public System.Collections.Generic.List<DoorAnimation> gateList;
    private string[] keywords;
    // Start is called before the first frame update

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


    void Start()
    {
        SetVocabNames();


        recognizer = new KeywordRecognizer(keywords, confidence);
        recognizer.OnPhraseRecognized += OnVocabRecognized;
        recognizer.Start();
        Debug.Log("Recognizer running: " + recognizer.IsRunning);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnVocabRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Recognized: " + args.text);
        foreach(DoorAnimation gate in gateList)
        {
            gate.checkVocab(args.text);
        }
    }

    private void SetVocabNames()
    {
        keywords = new string[chapter.vocabObj.Count];
        int index = 0;
        foreach (VocabObj vocab in chapter.vocabObj)
        {
            keywords[index++] = vocab.GetName();
        }
    }

    public void SetGateList(System.Collections.Generic.List<MGDoor> doorList)
    {
        gateList = new System.Collections.Generic.List<DoorAnimation>();
        foreach(MGDoor door in doorList)
        {
            DoorAnimation gate = door.transform.GetChild(1).GetComponent<DoorAnimation>();
            gateList.Add(gate);
        }
    }
}
