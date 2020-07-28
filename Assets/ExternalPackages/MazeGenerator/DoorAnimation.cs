using UnityEngine;
using System.Collections;
using InfusionEdutainment.Objects;
using InfusionEdutainment.Controllers;

public class DoorAnimation : MonoBehaviour {

    public VocabObj vocabBase;

    public static System.Random rnd = new System.Random();
    public int vocabCount;

    public float riseUpBy;
    public float openOnRange;
    public float speed;
    public GameObject trigger;
    private float initialY;
    private float toHeight;

    public bool locked { get; private set; }

    // Use this for initialization
    void Start () {
        locked = true;
        trigger = GameObject.Find("FPSController");
        initialY = transform.position.y;
        vocabBase = Instantiate(vocabBase, transform);
        vocabBase.transform.SetParent(transform);
        vocabBase.gameObject.SetActive(true);
        vocabCount = vocabBase.chapter.vocabObj.Count;
        SpawnRandomVocab();
	}
	
	// Update is called once per frame
	void Update () {
        if (!locked)
        {
            RaiseGate();
        }    
    }

    public void CheckVocab(string answer)
    {
        var dist = Vector3.Distance(transform.position, trigger.transform.position);
        if (dist < openOnRange && answer == vocabBase.GetName())
        {
            locked = false;
            StatTracker.Instance.AddCorrectVocab(answer);
        }
    }

    private void SpawnRandomVocab()
    {
        int rndValue = rnd.Next(vocabCount);
        vocabBase.SetVocab(rndValue);   
    }

    private void RaiseGate()
    {
        var dist = Vector3.Distance(transform.position, trigger.transform.position);

        if (dist < openOnRange)
        {
            toHeight = (1 - dist / openOnRange) * riseUpBy;
        }
        else toHeight = 0;

        var delta = toHeight + initialY - transform.position.y;

        if (Mathf.Abs(delta) > speed * Time.deltaTime)
            delta = Mathf.Sign(delta) * speed * Time.deltaTime;

        transform.position += new Vector3(0, delta, 0);
    }
}
