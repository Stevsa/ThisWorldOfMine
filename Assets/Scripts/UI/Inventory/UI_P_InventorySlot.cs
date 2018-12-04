using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TWoM.UI.Hover;
using TWoM.Items;

namespace TWoM.UI.Inventroys
{
    public class UI_P_InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public ItemSlot HoldingItem;
        
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ButtonPress);
        }
        
        void Update()
        {

        }

        public void SetupItem()
        {
            Transform Child = transform.GetChild(0);

            for (int j = 0; j < Child.childCount; j++)
            {
                if (Child.GetChild(j).GetComponent<Image>() != null)
                {
                    Child.GetChild(j).GetComponent<Image>().sprite = null;
                    Child.GetChild(j).GetComponent<Image>().color = Color.white;

                    Child.GetChild(j).gameObject.SetActive(false);
                    Child.GetChild(j).GetComponent<Image>().preserveAspect = true;
                    if (HoldingItem.VItem.Sprites.Count > j)
                    {
                        Child.GetChild(j).gameObject.SetActive(true);
                        Child.GetChild(j).GetComponent<Image>().sprite = HoldingItem.VItem.Sprites[j];
                        Child.GetChild(j).GetComponent<Image>().color = HoldingItem.VItem.Colours[j];
                    }
                }

                if (Child.GetChild(j).GetComponent<Text>() != null)
                {
                    Child.GetChild(j).gameObject.SetActive(true);
                    Child.GetChild(j).GetComponent<Text>().text = HoldingItem.Quantity.ToString();
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (HoldingItem != null)
                if (HoldingItem.VItem != null)
                    if (HoldingItem.VItem.name != "")
                    {
                        FindObjectOfType<UI_Hoverer>().InventoryHoverEnter();
                        FindObjectOfType<UI_H_Inventory>().Item = HoldingItem;
                        FindObjectOfType<UI_H_Inventory>().ItemSetup();
                    }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            FindObjectOfType<UI_Hoverer>().InventoryHoverExit();
        }

        public void ButtonPress()
        {
            GetComponentInParent<UI_P_Inventory>().SelectItem(gameObject);
        }

    }
}