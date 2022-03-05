using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



 [RequireComponent(typeof(AudioSource))]

public class PauseScript : MonoBehaviour
{
    private Text txtPause;
    private Image imPause;
    [SerializeField] bool onPause = false;
    [SerializeField] AudioClip sndPause, sndUnPause;


    void Awake()
    {
        imPause = transform.Find("ImPause").GetComponent<Image>();
        txtPause = transform.Find("TxtPause").GetComponent<Text>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            onPause = !onPause;
            if(onPause)
            {
                Time.timeScale = 0f;
                GetComponent<AudioSource>().PlayOneShot(sndPause);
                imPause.enabled = true;
                txtPause.enabled = true;
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(sndUnPause);
                imPause.enabled = false;
                txtPause.enabled = false;
                Time.timeScale = 1f;

            }
        }
        
    }
}
