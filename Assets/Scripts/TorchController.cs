﻿using InfusionEdutainment.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public GameObject ghost;
    public Light pointLight;
    public ParticleSystem flame;
    public float maxDistance;
    public AudioClip torchSound;

    public float lightRange;
    public float lightIntensity;
    public float flickerRate;

    [SerializeField]
    private AudioSource audioSource;
    
    private bool lightsOn = true;
    private float nextFlickerTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        ghost = GameController.Instance.ghost;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, ghost.transform.position) < maxDistance)
        {
            flickerLights();
        } else if (!lightsOn)
        {
            audioSource.PlayOneShot(torchSound);
            pointLight.range = lightRange;
            pointLight.intensity = lightIntensity;
            flame.Play();
            lightsOn = true;
        }
    }

    private void flickerLights()
    {
        if(Time.time > nextFlickerTime)
        {
           if(lightsOn)
            {
                pointLight.range = 0;
                pointLight.intensity = 0;
                flame.Stop();
                lightsOn = false;
                nextFlickerTime = Time.time + Random.Range(0, flickerRate*3);
            }
            else
            {
                audioSource.PlayOneShot(torchSound);
                pointLight.range = lightRange;
                pointLight.intensity = lightIntensity;
                flame.Play();
                lightsOn = true;
                nextFlickerTime = Time.time + Random.Range(0, flickerRate);
            }
        }
    }
}
