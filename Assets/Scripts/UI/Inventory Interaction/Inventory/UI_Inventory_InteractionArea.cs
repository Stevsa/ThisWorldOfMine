using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TWoM.Items;

namespace TWoM.UI.Inventroys
{
    public class UI_Inventory_InteractionArea : UI_P_InteractionArea
    {
        public GameObject All_InteractionArea;
        public GameObject Main_InteractionArea;
        public GameObject Secondary_InteractionArea;

        public ItemSlot Item;

        void Start()
        {

        }
        
        void Update()
        {

        }

        public void SwitchArea(InteractAreaType _type)
        {
            All_InteractionArea.SetActive(false);
            Main_InteractionArea.SetActive(false);
            Secondary_InteractionArea.SetActive(false);
            switch (_type)
            {
                case InteractAreaType.ALL:
                    All_InteractionArea.SetActive(true);
                    break;
                case InteractAreaType.GIVE:
                    Main_InteractionArea.SetActive(true);
                    ResetItem();
                    break;
                case InteractAreaType.TAKE:
                    Secondary_InteractionArea.SetActive(true);
                    ResetItem();
                    break;
                default:
                    break;
            }
        }

        public void ResetItem()
        {
            GameObject ItemGO = null;

            if (Main_InteractionArea.activeInHierarchy)
            {
                ItemGO = Main_InteractionArea.transform.Find("Item/Item_Placeholder").gameObject;
            }
            if (Secondary_InteractionArea.activeInHierarchy)
            {
                ItemGO = Secondary_InteractionArea.transform.Find("Item/Item_Placeholder").gameObject;
            }

            ItemGO.transform.parent.Find("Item Name").GetComponent<Text>().text = Item.VItem.name;

            ItemGO.transform.parent.Find("Quantity").GetComponent<Text>().text = "";
            if (Item.VItem.Stackable)
                ItemGO.transform.parent.Find("Quantity").GetComponent<Text>().text = "Quantity: " + Item.Quantity.ToString();

            if (ItemGO!=null)
            {
                for (int j = 0; j < ItemGO.transform.childCount; j++)
                {
                    if (ItemGO.transform.GetChild(j).GetComponent<Image>() != null)
                    {
                        ItemGO.transform.GetChild(j).GetComponent<Image>().sprite = null;
                        ItemGO.transform.GetChild(j).GetComponent<Image>().color = Color.white;

                        ItemGO.transform.GetChild(j).gameObject.SetActive(false);
                        ItemGO.transform.GetChild(j).GetComponent<Image>().preserveAspect = true;
                        if (Item.VItem.Sprites.Count > j)
                        {
                            ItemGO.transform.GetChild(j).gameObject.SetActive(true);
                            ItemGO.transform.GetChild(j).GetComponent<Image>().sprite = Item.VItem.Sprites[j];
                            ItemGO.transform.GetChild(j).GetComponent<Image>().color = Item.VItem.Colours[j];
                        }
                    }
                }

                ItemGO.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}