using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddChildScript : MonoBehaviour
{
    [SerializeField] Sprite slotWithChild;
    [SerializeField] Image[] slots;
    private int x = 0;
    public bool SlotComplete = false;
    public void AddChild()
    
    {
        slots[x].GetComponent<Image>().sprite = slotWithChild;
        x++;
        x = Mathf.Clamp(x, 0, slots.Length);

        if(x==slots.Length)
        {
            SlotComplete = true;
        }
    }
}