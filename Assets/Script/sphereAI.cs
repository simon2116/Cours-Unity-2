using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereAI : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    Ray rayon;
    RaycastHit hit;

    [SerializeField] Transform leftSensor, rightSensor;

    void Update()
    {
        rayon = new Ray(leftSensor.position, transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(rayon, out hit, Mathf.Infinity))
        {
            Debug.Log("Left Sensor Objet;" + hit.collider.name + "Distance:" + hit.distance);

            if (hit.distance<1)
            {
                float angle = Random.Range(100f, 300f);
                transform.Rotate(Vector3.up * angle);
            }
        }
        rayon = new Ray(rightSensor.position, transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(rayon, out hit, Mathf.Infinity))
        {
            Debug.Log("right Sensor Objet;" + hit.collider.name + "Distance:" + hit.distance);

            if (hit.distance < 1)
            {
                float angle = Random.Range(100f, 300f);
                transform.Rotate(Vector3.up * angle);
            }
        }
        //mieu de faire le raycast d'un emptyobject devant l'objet (car sinon tiré du centre de l'objet)


        //pour voir ray
        Debug.DrawRay(leftSensor.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);
        Debug.DrawRay(rightSensor.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}

