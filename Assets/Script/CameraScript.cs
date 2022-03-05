using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    [SerializeField]
    int rotationSpeed = 3;
    [SerializeField]
    bool rotationCam = true;
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        if(rotationCam)
        {
            Quaternion turnangle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            offset = turnangle * offset;
            transform.LookAt(player.transform);
        }
    }
}
