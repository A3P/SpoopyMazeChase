using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    [SerializeField]
    private TMP_Text timerText;
    private float startTime;
    private Boolean paused = false;

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

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!paused)
            timerText.text = (Time.time - startTime).ToString("0.0") + "s";
    }

    public void EndTimer()
    {
        paused = true;
    }
}
