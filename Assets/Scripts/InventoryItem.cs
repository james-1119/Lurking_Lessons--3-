using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem{ // inialize the name, image, functions for a inventory item
    string Name{get;}
    Sprite Image{get; }
    void OnPickup();
    void OnDrop();
}

public class InventoryEventArgs : EventArgs
{
   
    public InventoryEventArgs(IInventoryItem item){ // initialize a event handler function so functions can react
        Item = item;
    }

    public IInventoryItem Item;
}
