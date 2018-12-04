using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TWoM.Items;

namespace TWoM.UI.Hover
{
    public class UI_H_Inventory : MonoBehaviour
    {
        public ItemSlot Item;
        
        void Start()
        {

        }
        
        void Update()
        {

        }

        public void ItemSetup()
        {
            Debug.Log("Setup");

            Transform NameGO = transform.GetChild(0);
            Transform ImageGO = transform.GetChild(1);
            Transform QuantityGO = transform.GetChild(2);
            Transform InfoGO = transform.GetChild(3);
            Transform ExtraInfoGO = transform.GetChild(4);

            NameGO.GetComponentInChildren<Text>().text = Item.VItem.name;

            for (int j = 0; j < ImageGO.childCount; j++)
            {
                if (ImageGO.GetChild(j).GetComponent<Image>() != null)
                {
                    ImageGO.GetChild(j).GetComponent<Image>().sprite = null;
                    ImageGO.GetChild(j).GetComponent<Image>().color = Color.white;

                    ImageGO.GetChild(j).gameObject.SetActive(false);
                    ImageGO.GetChild(j).GetComponent<Image>().preserveAspect = true;
                    if (Item.VItem.Sprites.Count > j)
                    {
                        ImageGO.GetChild(j).gameObject.SetActive(true);
                        ImageGO.GetChild(j).GetComponent<Image>().sprite = Item.VItem.Sprites[j];
                        ImageGO.GetChild(j).GetComponent<Image>().color = Item.VItem.Colours[j];
                    }
                }
            }

            QuantityGO.GetComponentInChildren<Text>().text = "";
            if (Item.VItem.Stackable)
                QuantityGO.GetComponentInChildren<Text>().text = "Quantity: " + Item.Quantity.ToString();

        }
    }
}