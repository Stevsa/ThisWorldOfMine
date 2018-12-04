using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TWoM.UI
{
    public class UI_Hoverer : MonoBehaviour
    {
        public GameObject InventoryHover;
        
        void Start()
        {
            CloseAll();
        }
        
        public void CloseAll()
        {
            InventoryHover.SetActive(false);
        }

        void Update()
        {
            GetComponent<RectTransform>().position = Input.mousePosition;
        }

        public void InventoryHoverEnter()
        {
            CloseAll();
            InventoryHover.SetActive(true);
        }
        public void InventoryHoverExit()
        {
            CloseAll();
        }
    }
}