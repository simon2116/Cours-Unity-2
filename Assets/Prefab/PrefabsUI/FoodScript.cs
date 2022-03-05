using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    [SerializeField] UIbarScript pb;
    [SerializeField] int itemVal = 5;
    [SerializeField] MeshRenderer meshrenderer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            pb.Val += itemVal;
            audioSource.Play();

            GetComponent<BoxCollider>().enabled = false;
            meshrenderer.enabled = false;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
                Destroy(gameObject, audioSource.clip.length);
        }
    }
}
