using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class InfopointScript : MonoBehaviour
    
{
    [TextArea]
    public string TxtInfoPoint;

    [SerializeField] Text txt;

    [SerializeField] GameObject panel;

    [SerializeField] AudioClip sndPop, sndUnpop;


    void Start()
    {
        txt.text = TxtInfoPoint;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            panel.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(sndPop);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<AudioSource>().PlayOneShot(sndUnpop);
            panel.SetActive(false);
        }
    }
}
