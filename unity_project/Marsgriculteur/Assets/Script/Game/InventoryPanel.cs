using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class InventoryPanel : MonoBehaviour
    {
        public GameObject PanelInventory;
        public GameObject PanelNotif;

        void Start()
        {
            PanelInventory.SetActive(false);
        }
        private void OnMouseDown()
        {
            
        }
        public void OpenPanel()
        {
            if(!PanelNotif.activeSelf)
                PanelInventory.SetActive(!PanelInventory.activeSelf);
            /*if (PanelInventory.activeSelf == false)
            {
                if (PanelNotif.activeSelf == true)
                {
                    PanelNotif.SetActive(false);
                }
                PanelInventory.SetActive(true);
            }
            else
            {
                PanelInventory.SetActive(false);
            }*/
        }
    }
}
