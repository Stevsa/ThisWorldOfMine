using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TWoM.Items;
using TWoM.UI.Inventroys;

namespace TWoM.UI.Equipment
{
    public class UI_EquipmentSlot : MonoBehaviour
    {
        public V_P_Item Item;
        public ClothingType SlotType;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ButtonPress);
        }
        
        void Update()
        {

        }

        public void ButtonPress()
        {
            GetComponentInParent<UI_P_Inventory>().SelectEquipment(gameObject);
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
                    if (Item.Sprites.Count > j)
                    {
                        Child.GetChild(j).gameObject.SetActive(true);
                        Child.GetChild(j).GetComponent<Image>().sprite = Item.Sprites[j];
                        Child.GetChild(j).GetComponent<Image>().color = Item.Colours[j];
                    }
                }

                if (Child.GetChild(j).GetComponent<Text>() != null)
                {
                    Child.GetChild(j).gameObject.SetActive(false);
                }
            }
        }
    }
}