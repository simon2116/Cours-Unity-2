using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{

 

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource audiosource = GetComponent<AudioSource>();
            audiosource.Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<ClaireController>().HaveKey = true;
            Destroy(gameObject, audiosource.clip.length);
        }

    }
}
