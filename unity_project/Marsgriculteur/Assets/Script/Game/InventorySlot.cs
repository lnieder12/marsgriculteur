using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static GameObject createSlot()
    {
        return Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/SlotImage"));
    }

}