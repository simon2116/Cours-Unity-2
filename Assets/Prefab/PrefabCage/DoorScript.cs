using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    
    public bool OpenDoor = false;
    private bool Opened = false;
    public float DegreesPerSecond = 60f;
    public float rotationDegreesAmount = 120f;
    private float totalRotation = 0;

    

    AudioSource audioSource;
    [SerializeField] AudioClip SndOpen;
    [SerializeField] GameObject Kid;
    // Start is called before the first frame update
    void Start()
    {     
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (OpenDoor)
        {
            audioSource.PlayOneShot(SndOpen);            
            Opened = true;

            Kid.GetComponent<KidScript>().inCage = false;
            
            OpenDoor = false;
        }
        if (Opened)
        {
            if (Mathf.Abs(totalRotation) < Mathf.Abs(rotationDegreesAmount))
                SwingOpen();
        }
           
    }
    void SwingOpen()
    {
        float currentAngle = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.AngleAxis(currentAngle + (Time.deltaTime * DegreesPerSecond), Vector3.up);
        totalRotation += Time.deltaTime * DegreesPerSecond;

    }

}