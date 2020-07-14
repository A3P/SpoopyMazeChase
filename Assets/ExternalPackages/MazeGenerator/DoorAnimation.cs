using UnityEngine;
using System.Collections;
using InfusionEdutainment.Objects;

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
	// Use this for initialization
	void Start () {
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
        RaiseGate();
	}

    private void SpawnRandomVocab()
    {
        int rndValue = rnd.Next(vocabCount);
        Debug.Log("Vocab Count: " + vocabCount + " rndValue: " + rndValue);
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
