using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TWoM.Items;
using TWoM.Characters;

namespace TWoM.UI.Inventroys
{
    public class UI_Inventory_InteractionArea : UI_P_InteractionArea
    {
        public GameObject All_InteractionArea;
        public GameObject Main_InteractionArea;
        public GameObject Secondary_InteractionArea;

        public ItemSlot Item;
        public bool Equiped;

        public bool Main;

        public List<GameObject> MainButtons;
        public List<GameObject> SecondaryButtons;
        public List<GameObject> AllInterationsButtons;

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
                    ResetAll();
                    Item = null;
                    break;
                case InteractAreaType.GIVE:
                    Main_InteractionArea.SetActive(true);
                    Main = true;
                    ResetItem();
                    break;
                case InteractAreaType.TAKE:
                    Secondary_InteractionArea.SetActive(true);
                    Main = false;
                    ResetItem();
                    break;
                default:
                    break;
            }

        }

        public void ResetItem()
        {
            GameObject ItemGO = null;
            GameObject MainGO = null;
            List<GameObject> avalibleButtons = new List<GameObject>();

            for (int i = 0; i < MainButtons.Count; i++)
            {
                MainButtons[i].SetActive(false);
                MainButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            }

            for (int i = 0; i < SecondaryButtons.Count; i++)
            {
                SecondaryButtons[i].SetActive(false);
                SecondaryButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            }

            if (Main_InteractionArea.activeInHierarchy)
            {
                ItemGO = Main_InteractionArea.transform.Find("Item/Item_Placeholder").gameObject;
                MainGO = Main_InteractionArea.transform.Find("Item").gameObject;
                avalibleButtons.AddRange(MainButtons);
            }
            if (Secondary_InteractionArea.activeInHierarchy)
            {
                ItemGO = Secondary_InteractionArea.transform.Find("Item/Item_Placeholder").gameObject;
                MainGO = Secondary_InteractionArea.transform.Find("Item").gameObject;
                avalibleButtons.AddRange(SecondaryButtons);
            }

            if (MainGO != null)
            {
                MainGO.transform.Find("Item Name").GetComponent<Text>().text = Item.VItem.name;

                MainGO.transform.Find("Quantity").GetComponent<Text>().text = "";
                if (Item.VItem.Stackable)
                    MainGO.transform.Find("Quantity").GetComponent<Text>().text = "Quantity: " + Item.Quantity.ToString();
            }

            if (ItemGO != null)
            {
                Debug.Log("Change Sprite");
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

            if (ItemGO != null)
                if (!Equiped)
                {
                    if (Main)
                    {
                        {
                            GameObject newButton = avalibleButtons[0];
                            avalibleButtons.RemoveAt(0);
                            newButton.SetActive(true);
                            newButton.GetComponentInChildren<Text>().text = "Give One";
                            newButton.GetComponent<Button>().onClick.AddListener(delegate { GiveSingle(1); });
                        }
                        {
                            GameObject newButton = avalibleButtons[0];
                            avalibleButtons.RemoveAt(0);
                            newButton.SetActive(true);
                            newButton.GetComponentInChildren<Text>().text = "Give Five";
                            newButton.GetComponent<Button>().onClick.AddListener(delegate { GiveSingle(5); });
                        }
                        {
                            GameObject newButton = avalibleButtons[0];
                            avalibleButtons.RemoveAt(0);
                            newButton.SetActive(true);
                            newButton.GetComponentInChildren<Text>().text = "Give All";
                            newButton.GetComponent<Button>().onClick.AddListener(delegate { GiveSingle(0); });
                        }
                    }
                    else
                    {
                        {
                            GameObject newButton = avalibleButtons[0];
                            avalibleButtons.RemoveAt(0);
                            newButton.SetActive(true);
                            newButton.GetComponentInChildren<Text>().text = "Take One";
                            newButton.GetComponent<Button>().onClick.AddListener(delegate { TakeSingle(1); });
                        }
                        {
                            GameObject newButton = avalibleButtons[0];
                            avalibleButtons.RemoveAt(0);
                            newButton.SetActive(true);
                            newButton.GetComponentInChildren<Text>().text = "Take Five";
                            newButton.GetComponent<Button>().onClick.AddListener(delegate { TakeSingle(5); });
                        }
                        {
                            GameObject newButton = avalibleButtons[0];
                            avalibleButtons.RemoveAt(0);
                            newButton.SetActive(true);
                            newButton.GetComponentInChildren<Text>().text = "Take All";
                            newButton.GetComponent<Button>().onClick.AddListener(delegate { TakeSingle(0); });
                        }
                    }
                }

            if (ItemGO != null)
                if (Item.VItem is IEquipable<P_Character>)
                {
                    if (!Equiped)
                    {
                        {
                            GameObject newButton = avalibleButtons[0];
                            avalibleButtons.RemoveAt(0);
                            newButton.SetActive(true);
                            newButton.GetComponentInChildren<Text>().text = "Equip(Self)";
                            newButton.GetComponent<Button>().onClick.AddListener(EquipClickMain);
                        }
                        if (GetComponentInParent<UI_Controller_Inventory>().Secondary_Charicter != null)
                        {
                            GameObject newButton = avalibleButtons[0];
                            avalibleButtons.RemoveAt(0);
                            newButton.SetActive(true);
                            newButton.GetComponentInChildren<Text>().text = "Equip(Other)";
                            newButton.GetComponent<Button>().onClick.AddListener(EquipClickSecondary);
                        }
                    }
                    else
                    {
                        {
                            GameObject newButton = avalibleButtons[0];
                            avalibleButtons.RemoveAt(0);
                            newButton.SetActive(true);
                            newButton.GetComponentInChildren<Text>().text = "Unequip";
                            newButton.GetComponent<Button>().onClick.AddListener(UnequipClick);
                        }
                    }
                }
        }

        public void ResetAll()
        {
            List<GameObject> avalibleButtons = new List<GameObject>();
            avalibleButtons.AddRange(AllInterationsButtons);

            Equiped = false;
            Item = null;

            Debug.Log("All Swtup");
            {
                GameObject newButton = avalibleButtons[0];
                avalibleButtons.RemoveAt(0);
                newButton.SetActive(true);
                newButton.GetComponentInChildren<Text>().text = "Take All";
                newButton.GetComponent<Button>().onClick.AddListener(TakeAll);
            }
            {
                GameObject newButton = avalibleButtons[0];
                avalibleButtons.RemoveAt(0);
                newButton.SetActive(true);
                newButton.GetComponentInChildren<Text>().text = "Give All";
                newButton.GetComponent<Button>().onClick.AddListener(GiveAll);
            }
            {
                GameObject newButton = avalibleButtons[0];
                avalibleButtons.RemoveAt(0);
                newButton.SetActive(true);
                newButton.GetComponentInChildren<Text>().text = "Leave";
                newButton.GetComponent<Button>().onClick.AddListener(FindObjectOfType<UI_Controller_Inventory>().CloseInventorys);
            }
        }

        public void EquipClickMain()
        {
            if ((Item.VItem as IEquipable<P_Character>).Equip(GetComponentInParent<UI_Controller_Inventory>().Main_Charicter))
            {
                GetComponentInParent<UI_Controller_Inventory>().Main_Charicter.Inventory.RemoveAt(GetComponentInParent<UI_Controller_Inventory>().SelectedSlot);

                GetComponentInParent<UI_Controller_Inventory>().Main_Inventory.RebuildInventroy();
                GetComponentInParent<UI_Controller_Inventory>().Main_Inventory.ChangeToEquipment();
                GetComponentInParent<UI_Controller_Inventory>().Main_Inventory.RebuildEquipment();
                GetComponentInParent<UI_Controller_Inventory>().Unselect();
            }
        }

        public void EquipClickSecondary()
        {
            if ((Item.VItem as IEquipable<P_Character>).Equip(GetComponentInParent<UI_Controller_Inventory>().Secondary_Charicter))
            {
                GetComponentInParent<UI_Controller_Inventory>().Secondary_Charicter.Inventory.RemoveAt(GetComponentInParent<UI_Controller_Inventory>().SelectedSlot);

                GetComponentInParent<UI_Controller_Inventory>().Secondary_Inventory.RebuildInventroy();
                GetComponentInParent<UI_Controller_Inventory>().Secondary_Inventory.ChangeToEquipment();
                GetComponentInParent<UI_Controller_Inventory>().Secondary_Inventory.RebuildEquipment();
                GetComponentInParent<UI_Controller_Inventory>().Unselect();
            }
        }

        public void UnequipClick()
        {
            ItemSlot newItem = Item;
            List<V_P_Item> newEquipment;
            P_Character charicter;

            if (Main)
            {
                newEquipment = GetComponentInParent<UI_Controller_Inventory>().Main_Charicter.Equipment;
                charicter = GetComponentInParent<UI_Controller_Inventory>().Main_Charicter;
            }
            else
            {
                newEquipment = GetComponentInParent<UI_Controller_Inventory>().Secondary_Charicter.Equipment;
                charicter = GetComponentInParent<UI_Controller_Inventory>().Secondary_Charicter;
            }

            for (int i = 0; i < newEquipment.Count; i++)
            {
                if (newEquipment[i] == newItem.VItem)
                {
                    newEquipment.RemoveAt(i);
                    break;
                }
            }

            charicter.AddItemtoInventory(new ItemSlot(Item.VItem, 1));

            SwitchArea(InteractAreaType.ALL);
            Equiped = false;
            if (Main)
            {
                GetComponentInParent<UI_Controller_Inventory>().Main_Inventory.RebuildEquipment();
                GetComponentInParent<UI_Controller_Inventory>().Main_Inventory.ChangeToInventory();
                GetComponentInParent<UI_Controller_Inventory>().Main_Inventory.RebuildInventroy();
            }
            else
            {

                GetComponentInParent<UI_Controller_Inventory>().Secondary_Inventory.RebuildEquipment();
                GetComponentInParent<UI_Controller_Inventory>().Secondary_Inventory.ChangeToInventory();
                GetComponentInParent<UI_Controller_Inventory>().Secondary_Inventory.RebuildInventroy();
            }
            GetComponentInParent<UI_Controller_Inventory>().Unselect();
        }

        public void TakeAll()
        {
            List<ItemSlot> newInventorySlot = new List<ItemSlot>();

            IInventoryHolder<ItemSlot> _from;
            IInventoryHolder<ItemSlot> _to = GetComponentInParent<UI_Controller_Inventory>().Main_Charicter;

            if (GetComponentInParent<UI_Controller_Inventory>().Secondary_Container != null)
                _from = GetComponentInParent<UI_Controller_Inventory>().Secondary_Container;
            else
                _from = GetComponentInParent<UI_Controller_Inventory>().Secondary_Charicter;

            if (_from != null)
            {
                for (int i = 0; i < _from.Inventory.Count; i++)
                {
                    if (!_to.AddItemtoInventory(_from.Inventory[i]))
                    {
                        newInventorySlot.Add(_from.Inventory[i]);
                    }
                }
                _from.Inventory = newInventorySlot;
            }
            GetComponentInParent<UI_Controller_Inventory>().Secondary_Inventory.RebuildInventroy();
            GetComponentInParent<UI_Controller_Inventory>().InventoryUsed();
            GetComponentInParent<UI_Controller_Inventory>().Unselect();
        }
        public void TakeSingle(int amount)
        {
            IInventoryHolder<ItemSlot> _from;
            IInventoryHolder<ItemSlot> _to = GetComponentInParent<UI_Controller_Inventory>().Main_Charicter;

            if (GetComponentInParent<UI_Controller_Inventory>().Secondary_Container != null)
                _from = GetComponentInParent<UI_Controller_Inventory>().Secondary_Container;
            else
                _from = GetComponentInParent<UI_Controller_Inventory>().Secondary_Charicter;

            if (amount < 1)
            {
                amount = Item.Quantity;
            }
            else
            {
                if (amount > Item.Quantity) amount = Item.Quantity;
            }

            if (_to.AddItemtoInventory(new ItemSlot(Item.VItem, amount)))
            {
                int index = _from.Inventory.FindIndex(newItem => newItem == Item);
                _from.Inventory[index].Quantity -= amount;
                if (_from.Inventory[index].Quantity < 1)
                {
                    _from.Inventory.RemoveAt(index);
                }
            }

            GetComponentInParent<UI_Controller_Inventory>().Secondary_Inventory.RebuildInventroy();
            GetComponentInParent<UI_Controller_Inventory>().InventoryUsed();
            GetComponentInParent<UI_Controller_Inventory>().Unselect();
        }


        public void GiveAll()
        {
            List<ItemSlot> newInventorySlot = new List<ItemSlot>();

            IInventoryHolder<ItemSlot> _to;
            IInventoryHolder<ItemSlot> _from = GetComponentInParent<UI_Controller_Inventory>().Main_Charicter;

            if (GetComponentInParent<UI_Controller_Inventory>().Secondary_Container != null)
                _to = GetComponentInParent<UI_Controller_Inventory>().Secondary_Container;
            else
                _to = GetComponentInParent<UI_Controller_Inventory>().Secondary_Charicter;

            for (int i = 0; i < _from.Inventory.Count; i++)
            {
                if (_to == null)
                {
                    GetComponentInParent<UI_Controller_Inventory>().CreateNewContainer(_from.Inventory);
                    break;
                }
                else if (!_to.AddItemtoInventory(_from.Inventory[i]))
                {
                    newInventorySlot.Add(_from.Inventory[i]);
                }
            }

            Debug.Log(_from.Inventory.Count);
            _from.Inventory = newInventorySlot;
            GetComponentInParent<UI_Controller_Inventory>().Secondary_Inventory.RebuildInventroy();
            GetComponentInParent<UI_Controller_Inventory>().InventoryUsed();
            GetComponentInParent<UI_Controller_Inventory>().Unselect();
        }
        public void GiveSingle(int amount)
        {
            IInventoryHolder<ItemSlot> _to;
            IInventoryHolder<ItemSlot> _from = GetComponentInParent<UI_Controller_Inventory>().Main_Charicter;

            if (GetComponentInParent<UI_Controller_Inventory>().Secondary_Container != null)
                _to = GetComponentInParent<UI_Controller_Inventory>().Secondary_Container;
            else
                _to = GetComponentInParent<UI_Controller_Inventory>().Secondary_Charicter;



            if (amount < 1)
            {
                amount = Item.Quantity;
            }
            else
            {
                if (amount > Item.Quantity) amount = Item.Quantity;
            }
            if (_to == null)
            {
                GetComponentInParent<UI_Controller_Inventory>().CreateNewContainer(new List<ItemSlot>());
                _to = GetComponentInParent<UI_Controller_Inventory>().Secondary_Container;
            }
            if (_to.AddItemtoInventory(new ItemSlot(Item.VItem, amount)))
            {
                int index = _from.Inventory.FindIndex(newItem => newItem == Item);
                _from.Inventory[index].Quantity -= amount;
                if (_from.Inventory[index].Quantity < 1)
                {
                    _from.Inventory.RemoveAt(index);
                }
            }
            GetComponentInParent<UI_Controller_Inventory>().Secondary_Inventory.RebuildInventroy();
            GetComponentInParent<UI_Controller_Inventory>().InventoryUsed();
            GetComponentInParent<UI_Controller_Inventory>().Unselect();
        }
    }
}