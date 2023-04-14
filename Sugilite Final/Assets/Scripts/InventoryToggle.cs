using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public bool inventoryOn;
    public GameObject inventoryCanvas;

    // Start is called before the first frame update
    void Start()
    {
        inventoryOn = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !inventoryOn)
        {
            inventoryCanvas.SetActive(true);
            inventoryOn = true;
            Debug.Log("Inventory On!");
            return;
        }

        if (Input.GetKeyDown(KeyCode.I) && inventoryOn)
        {
            inventoryCanvas.SetActive(false);
            inventoryOn = false;
            Debug.Log("Inventory Off!");
            return;
        }
        

        
    }

    
}
