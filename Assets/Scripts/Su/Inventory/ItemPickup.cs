using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; 

    void Pickup()
    {        
        InventoryManager.Instance.Add(item);
        if(item.id == 6 || item.id == 7)
        {
            gameObject.SetActive(false); 
            gameObject.transform.position = Vector3.zero; 
        }
        else
        {
            Destroy(gameObject); 
        }
        
        //gameObject.SetActive(false);
    }

    public void OnMouseDownFunction()
    {
        Pickup(); 
    }

}
