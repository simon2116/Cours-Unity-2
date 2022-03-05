using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIbarScript : MonoBehaviour
{
    Image bar;
    Text txt;

    private float val;
    public float Val
    {
        get 
        {
            return val; 
        }
        set
        {
            val = value;
            val = Mathf.Clamp(val, 0, 100);
            UpdateValue();
        }
    }
    void Awake()     
    {
        bar = transform.Find("UIbar").GetComponent<Image>();
        txt = bar.transform.Find("UItext").GetComponent<Text>();
        Val = 100;
    }

    void UpdateValue()
    {
        txt.text = (int)val + "%";
        bar.fillAmount = val / 100;

    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.KeypadMinus))
        {
            Val--;
           
        }
        if(Input.GetKey(KeyCode.KeypadPlus))
        {
            Val++;
        }
        if (Val >= 67)
            bar.color = Color.green;
        
        if(Val < 67)
        {
            if (Val < 25)           
                bar.color = Color.red;
            
            else
                bar.color = Color.yellow;
        }
                    
    }
}
