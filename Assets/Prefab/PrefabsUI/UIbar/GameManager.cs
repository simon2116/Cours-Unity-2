using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{   
    [SerializeField]
    UIbarScript pbHealth, pbEnergy, pbFood;

    //Hunger

    [SerializeField]
    float decreaseFood = 0.5f, decreaseRate = 4f;

    //Energy
    [SerializeField]
    float walkEnergy = 0.01f, runEnergy = 0.01f;

    bool dead =false;



    void Start()
    {
      
        
        StartCoroutine(DecreaseHunger());
    }

    IEnumerator DecreaseHunger()
    {
        while (pbFood.Val>0)
        {
            pbFood.Val -= decreaseFood;
            yield return new WaitForSeconds(decreaseRate);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<ClaireController>().ClaireDead();
    }


    private void FixedUpdate()
    {
        //Energy
        if (pbEnergy.Val > 0 && !dead)
        {
            if (Input.GetAxis("Vertical") != 0 && !dead)
            {
                pbEnergy.Val -= walkEnergy;

                if (Input.GetKey(KeyCode.LeftControl) && !dead)
                {
                    pbEnergy.Val -= runEnergy;
                }
            }
        }
        if (pbEnergy.Val == 0 && !dead)
        {
            dead = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<ClaireController>().ClaireDead();
        }
    }
}
