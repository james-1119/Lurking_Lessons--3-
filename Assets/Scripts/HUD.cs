using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory Inventory;
    // Start is called before the first frame update
    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemRemoved += Inventory_ItemRemoved;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e){
        Transform inventoryPanel = transform.Find("Inventory");
        foreach(Transform slot in inventoryPanel){

            // Border Image
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();

            // finding an empty slot in the bar
            if(!image.enabled){
                image.enabled = true;
                image.sprite = e.Item.Image;
                break;
            }
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e){
        Transform inventoryPanel = transform.Find("Inventory"); // find Inventory class in Unity
        Image image = inventoryPanel.GetChild(Inventory.itemSelection).GetChild(0).GetChild(0).GetComponent<Image>(); // locate it's image
        if(image.enabled){ // enable the image
            image.enabled = false;
            image.sprite = null;
                
        }

    }

   
}
