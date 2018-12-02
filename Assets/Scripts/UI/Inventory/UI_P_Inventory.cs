using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TWoM.Items;
using TWoM.Characters;
using TWoM.Inworld;

namespace TWoM.UI.Inventroys
{
    [System.Serializable]
    public class InventorySlot
    {
        public GameObject Slot;
        public GameObject Item;
    }

    public class UI_P_Inventory : MonoBehaviour
    {
        public List<ItemSlot> Inventory;

        //UI Inventory Stuffs
        public List<InventorySlot> Slots;
        public GameObject ExampleItem;

        public List<GameObject> ItemPool;

        void Awake()
        {
            Slots = new List<InventorySlot>();
            for (int i = 0; i < GetComponentInChildren<GridLayoutGroup>().transform.childCount; i++)
            {
                InventorySlot slot = new InventorySlot();
                slot.Slot = GetComponentInChildren<GridLayoutGroup>().transform.GetChild(i).gameObject;
                Slots.Add(slot);
            }
            ExampleItem.SetActive(false);
        }

        public void UpdateFrom(P_Character _Character)
        {
                UpdateFromInventory(_Character.Inventory);
        }

        public void UpdateFrom(P_Container _Container)
        {
            UpdateFromInventory(_Container.Inventory);
        }

        public void UpdateEmpty()
        {
            Inventory = new List<ItemSlot>();
            RebuildInventroy();
        }

        void UpdateFromInventory(List<ItemSlot> items)
        {
            if (items == null)
            {
                items = new List<ItemSlot>();
            }
            if (items != Inventory)
            {
                Inventory = items;
                RebuildInventroy();
            }
        }

        public void RebuildInventroy()
        {
            ExampleItem.SetActive(false);
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].Item != null)
                    Slots[i].Item.SetActive(false);
                Slots[i].Item = null;

                if (Inventory.Count > i)
                {
                    GameObject ItemGO;
                    if (ItemPool.Count > 0)
                    {
                        ItemGO = ItemPool[0];
                        ItemPool.RemoveAt(0);
                    }
                    else
                    {
                        ItemGO = Instantiate(ExampleItem);
                    }
                    ItemGO.SetActive(true);
                    ItemGO.transform.SetParent(Slots[i].Slot.transform);

                    for (int j = 0; j < ItemGO.transform.childCount; j++)
                    {
                        ItemGO.transform.GetChild(j).gameObject.SetActive(true);
                        ItemGO.transform.GetChild(j).GetComponent<Image>().preserveAspect = true;
                        if (Inventory[i].VItem.Sprites.Count > j)
                        {
                            ItemGO.transform.GetChild(j).GetComponent<Image>().sprite = Inventory[i].VItem.Sprites[j];
                            ItemGO.transform.GetChild(j).GetComponent<Image>().color = Inventory[i].VItem.Colours[j];
                        }
                        else
                        {
                            ItemGO.transform.GetChild(j).gameObject.SetActive(false);
                        }
                    }

                    ItemGO.transform.localPosition = new Vector3(0, 0, -1);
                    ItemGO.transform.localScale = new Vector3(1, 1, 1);

                    Slots[i].Item = ItemGO;
                }
            }
        }


    }
}