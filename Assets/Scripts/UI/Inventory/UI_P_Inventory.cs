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
        public V_P_Item v_Item;
        public int v_Quantity;
    }
    [System.Serializable]
    public class EquipmentSlots
    {
        public GameObject Slot;
        public GameObject Item;
        public V_P_Item v_Item;
    }

    public class UI_P_Inventory : MonoBehaviour
    {
        public List<ItemSlot> Inventory;

        //UI Inventory Stuffs
        public List<InventorySlot> Slots;
        public List<EquipmentSlots> E_Slots;
        public GameObject ExampleItem;

        public List<GameObject> ItemPool;

        public GameObject EquipmentArea;
        public GameObject InventoryArea;

        public GameObject TempArea;

        public int Page = 0;

        void Awake()
        {
            Slots = new List<InventorySlot>();
            for (int i = 0; i < InventoryArea.GetComponentInChildren<GridLayoutGroup>().transform.childCount; i++)
            {
                InventorySlot slot = new InventorySlot();
                slot.Slot = InventoryArea.GetComponentInChildren<GridLayoutGroup>().transform.GetChild(i).gameObject;
                Slots.Add(slot);
            }

            E_Slots = new List<EquipmentSlots>();
            for (int i = 0; i < EquipmentArea.GetComponentInChildren<GridLayoutGroup>().transform.childCount; i++)
            {
                EquipmentSlots slot = new EquipmentSlots();
                slot.Slot = EquipmentArea.GetComponentInChildren<GridLayoutGroup>().transform.GetChild(i).gameObject;
                E_Slots.Add(slot);
            }

            ExampleItem.SetActive(false);
        }

        public void UpdateFrom(P_Character _Character)
        {
            UpdateFromInventory(_Character.Inventory);

            transform.Find("Selection Option/Equipment").gameObject.SetActive(true);
        }

        public void UpdateFrom(P_Container _Container)
        {
            UpdateFromInventory(_Container.Inventory);

            transform.Find("Selection Option/Equipment").gameObject.SetActive(false);
        }

        public void UpdateEmpty()
        {
            Inventory = new List<ItemSlot>();
            RebuildInventroy();
        }

        void UpdateFromInventory(List<ItemSlot> items)
        {
            if (items == null) items = new List<ItemSlot>();

            Inventory = items;
            RebuildInventroy();
        }

        public void RebuildInventroy()
        {
            ExampleItem.SetActive(false);
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].Item != null)
                {
                    ItemPool.Add(Slots[i].Item);
                    Slots[i].Item.transform.SetParent(TempArea.transform);
                    Slots[i].Item.SetActive(false);
                    Slots[i].Item = null;
                }
                Slots[i].Slot.GetComponent<UI_P_InventorySlot>().HoldingItem = null;

                if (Inventory.Count > i)
                {
                    Slots[i].v_Item = Inventory[i].VItem;
                    Slots[i].v_Quantity = Inventory[i].Quantity;

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
                    
                    ItemGO.transform.localPosition = new Vector3(0, 0, -1);
                    ItemGO.transform.localScale = new Vector3(1, 1, 1);
                    
                    Slots[i].Slot.GetComponent<UI_P_InventorySlot>().HoldingItem = Inventory[i];

                    Slots[i].Slot.GetComponent<UI_P_InventorySlot>().SetupItem();

                    Slots[i].Item = ItemGO;
                }
            }
        }

        public void SelectItem(GameObject fromSlot)
        {
            Debug.Log("Select Item");
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].Slot == fromSlot)
                {
                    Debug.Log("Found Slot");
                    GetComponentInParent<UI_Controller_Inventory>().SelectSlot((Page * 20) + i, GetComponent<UI_P_Inventory>());
                }
            }
        }

        public void ChangeToEquipment()
        {
            EquipmentArea.SetActive(true);
            InventoryArea.SetActive(false);
        }

        public void ChangeToInventory()
        {
            EquipmentArea.SetActive(false);
            InventoryArea.SetActive(true);
        }
    }
}