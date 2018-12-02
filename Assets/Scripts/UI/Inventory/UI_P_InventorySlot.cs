using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TWoM.UI.Inventroys
{
    public class UI_P_InventorySlot : MonoBehaviour
    {
        
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ButtonPress);
        }
        
        void Update()
        {

        }

        public void ButtonPress()
        {
            Debug.Log("Click");
            GetComponentInParent<UI_P_Inventory>().SelectItem(gameObject);
        }
    }
}