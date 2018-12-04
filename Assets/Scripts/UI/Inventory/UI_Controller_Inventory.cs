using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWoM.Items;
using TWoM.Characters;
using TWoM.Inworld;

namespace TWoM.UI.Inventroys
{
    public enum InteractAreaType
    {
        ALL,
        GIVE,
        TAKE
    }

    public class UI_Controller_Inventory : MonoBehaviour
    {
        public UI_P_Inventory Main_Inventory;
        public UI_P_Inventory Secondary_Inventory;

        public UI_Inventory_InteractionArea InteractionArea;

        public P_Character Main_Charicter;

        public P_Character Secondary_Charicter;
        public P_Container Secondary_Container;

        public GameObject TempContainer;

        public int SelectedSlot;
        public UI_P_Inventory from_Inventory;

        void Start()
        {
        }
        
        void Update()
        {

        }

        public void OpenInventoryFrom(P_Character _Character)
        {

            if (!FindObjectOfType<UI_Controller>().InMenu)
            {

                FindObjectOfType<UI_Controller>().InMenu = true;
                Main_Charicter = _Character;
                Secondary_Charicter = null;
                Secondary_Container = null;

                Main_Inventory.UpdateFrom(_Character);
                Secondary_Inventory.UpdateEmpty();
                FindObjectOfType<UI_Middle_Interaction_Area>().OpenFrom(gameObject);
            }
            Unselect();
        }

        public void OpenInventoryFrom(P_Character _Character, P_Character _Secondary)
        {

            if (!FindObjectOfType<UI_Controller>().InMenu)
            {
                FindObjectOfType<UI_Controller>().InMenu = true;
                Main_Charicter = _Character;
                Secondary_Charicter = _Secondary;
                Secondary_Container = null;

                Main_Inventory.UpdateFrom(_Character);
                Secondary_Inventory.UpdateFrom(_Secondary);
                FindObjectOfType<UI_Middle_Interaction_Area>().OpenFrom(gameObject);
            }
            Unselect();
        }

        public void OpenInventoryFrom(P_Character _Character, P_Container _Secondary)
        {

            if (!FindObjectOfType<UI_Controller>().InMenu)
            {
                FindObjectOfType<UI_Controller>().InMenu = true;
                Main_Charicter = _Character;
                Secondary_Charicter = null;
                Secondary_Container = _Secondary;

                Main_Inventory.UpdateFrom(_Character);
                Secondary_Inventory.UpdateFrom(_Secondary);
                FindObjectOfType<UI_Middle_Interaction_Area>().OpenFrom(gameObject);
            }
            Unselect();
        }

        public void CloseInventorys()
        {
            Main_Charicter.Inventory = Main_Inventory.Inventory;

            if (Secondary_Charicter != null) Secondary_Charicter.Inventory = Secondary_Inventory.Inventory;
            if (Secondary_Container != null) Secondary_Container.Inventory = Secondary_Inventory.Inventory;
            

            //if (Secondary_Charicter != null) Secondary_Charicter.Inventory = Secondary_Inventory.Inventory;
            if (Secondary_Container != null) Secondary_Container.Close();
            
            FindObjectOfType<UI_Middle_Interaction_Area>().Close();
        }

        void InventoryUsed()
        {
            Main_Inventory.UpdateFrom(Main_Charicter);
            
            if (Secondary_Charicter != null) Secondary_Inventory.UpdateFrom(Secondary_Charicter); ;
            if (Secondary_Container != null) Secondary_Inventory.UpdateFrom(Secondary_Container); ;
        }

        public void Unselect()
        {
            BackInteractionArea();
        }

        //Take
        public void TakeAll()
        {
            if (Secondary_Charicter != null)
            {
                for (int i = 0; i < Secondary_Charicter.Inventory.Count; i++)
                {
                    ItemSlot newItem = Secondary_Charicter.Inventory[i];

                    Main_Charicter.AddItemtoInventory(newItem);
                }
                Secondary_Charicter.Inventory = new List<ItemSlot>();
            }
            if (Secondary_Container != null)
            {
                for (int i = 0; i < Secondary_Container.Inventory.Count; i++)
                {
                    ItemSlot newItem = Secondary_Container.Inventory[i];

                    Main_Charicter.AddItemtoInventory(newItem);
                }
                Secondary_Container.Inventory = new List<ItemSlot>();
            }

            InventoryUsed();
        }
        public void TakeSingle(int amount)
        {
            if (Secondary_Charicter != null)
            {
                if (Secondary_Charicter.Inventory[SelectedSlot].Quantity < amount) amount = Secondary_Charicter.Inventory[SelectedSlot].Quantity;

                ItemSlot newItem = new ItemSlot(Secondary_Charicter.Inventory[SelectedSlot].VItem,amount);
                if (Main_Charicter.AddItemtoInventory(newItem))
                {

                    Secondary_Charicter.Inventory[SelectedSlot].Quantity -= amount;
                    if (Secondary_Charicter.Inventory[SelectedSlot].Quantity <= 0)
                    {
                        Secondary_Charicter.Inventory.RemoveAt(SelectedSlot);
                    }
                }
            }
            if (Secondary_Container != null)
            {
                if (Secondary_Container.Inventory[SelectedSlot].Quantity < amount) amount = Secondary_Container.Inventory[SelectedSlot].Quantity;

                ItemSlot newItem = new ItemSlot(Secondary_Container.Inventory[SelectedSlot].VItem, amount);
                if (Main_Charicter.AddItemtoInventory(newItem))
                {

                    Secondary_Container.Inventory[SelectedSlot].Quantity -= amount;
                    if (Secondary_Container.Inventory[SelectedSlot].Quantity <= 0)
                    {
                        Secondary_Container.Inventory.RemoveAt(SelectedSlot);
                    }
                }
            }

            BackInteractionArea();
            InventoryUsed();
        }
        public void TakeSingle()
        {
            if (Secondary_Charicter != null)
            {
                ItemSlot newItem = new ItemSlot(Secondary_Charicter.Inventory[SelectedSlot].VItem, Secondary_Charicter.Inventory[SelectedSlot].Quantity);
                if (Main_Charicter.AddItemtoInventory(newItem))
                {
                    Secondary_Charicter.Inventory.RemoveAt(SelectedSlot);
                }
            }
            if (Secondary_Container != null)
            {
                ItemSlot newItem = new ItemSlot(Secondary_Container.Inventory[SelectedSlot].VItem, Secondary_Container.Inventory[SelectedSlot].Quantity);
                if (Main_Charicter.AddItemtoInventory(newItem))
                {
                    Secondary_Container.Inventory.RemoveAt(SelectedSlot);
                }
            }

            BackInteractionArea();
            InventoryUsed();
        }

        //Give
        public void GiveAll()
        {

            if (Secondary_Charicter != null)
            {
                for (int i = 0; i < Main_Charicter.Inventory.Count; i++)
                {
                    Secondary_Charicter.AddItemtoInventory(Main_Charicter.Inventory[i]);
                }
            }
            if (Secondary_Container != null)
            {
                for (int i = 0; i < Main_Charicter.Inventory.Count; i++)
                {
                    Secondary_Container.AddItemtoInventory(Main_Charicter.Inventory[i]);
                }
            }

            if (Secondary_Charicter == null && Secondary_Container == null) CreateNewContainer(Main_Charicter.Inventory);

            Main_Charicter.Inventory = new List<ItemSlot>();

            InventoryUsed();
        }
        public void GiveSinge(int amount)
        {
            if (Main_Charicter.Inventory[SelectedSlot].Quantity < amount) amount = Main_Charicter.Inventory[SelectedSlot].Quantity;

            ItemSlot newItem = new ItemSlot(Main_Charicter.Inventory[SelectedSlot].VItem, amount);

            if (Secondary_Charicter != null)
            {
                if (Secondary_Charicter.AddItemtoInventory(newItem))
                {
                    Main_Charicter.Inventory[SelectedSlot].Quantity -= amount;
                    if (Main_Charicter.Inventory[SelectedSlot].Quantity <= 0)
                    {
                        Main_Charicter.Inventory.RemoveAt(SelectedSlot);
                    }
                }
            }
            if (Secondary_Container != null)
            {
                if (Secondary_Container.AddItemtoInventory(newItem))
                {
                    Main_Charicter.Inventory[SelectedSlot].Quantity -= amount;
                    if (Main_Charicter.Inventory[SelectedSlot].Quantity <= 0)
                    {
                        Main_Charicter.Inventory.RemoveAt(SelectedSlot);
                    }
                }
            }

            List<ItemSlot> newlist = new List<ItemSlot>();

            newlist.Add(newItem);
            if (Secondary_Charicter == null && Secondary_Container == null)
            {
                CreateNewContainer(newlist);
                Main_Charicter.Inventory[SelectedSlot].Quantity -= amount;
                if (Main_Charicter.Inventory[SelectedSlot].Quantity <= 0)
                {
                    Main_Charicter.Inventory.RemoveAt(SelectedSlot);
                }
            }

            BackInteractionArea();
            InventoryUsed();
        }
        public void GiveSinge()
        {
            ItemSlot newItem = new ItemSlot(Main_Charicter.Inventory[SelectedSlot].VItem, Main_Charicter.Inventory[SelectedSlot].Quantity);
            if (Secondary_Charicter != null)
            {
                if (Secondary_Charicter.AddItemtoInventory(newItem))
                {
                    Main_Charicter.Inventory.RemoveAt(SelectedSlot);
                }
            }
            if (Secondary_Container != null)
            {
                if (Secondary_Container.AddItemtoInventory(newItem))
                {
                    Main_Charicter.Inventory.RemoveAt(SelectedSlot);
                }
            }
            List<ItemSlot> newlist = new List<ItemSlot>();

            newlist.Add(newItem);
            if (Secondary_Charicter == null && Secondary_Container == null)
            {
                CreateNewContainer(newlist);
                Main_Charicter.Inventory.RemoveAt(SelectedSlot);
            }

            BackInteractionArea();
            InventoryUsed();
        }

        void CreateNewContainer(List<ItemSlot> _items)
        {
            P_Container newContainer = Instantiate(TempContainer, new Vector3(Main_Charicter.transform.position.x, Main_Charicter.transform.position.y,-1), Quaternion.identity).GetComponent<P_Container>();
            newContainer.Inventory = _items;
            Secondary_Container = newContainer;
        }

        public void SelectSlot(int _slot, UI_P_Inventory _from)
        {
            if (_from.Inventory.Count > _slot)
            {
                SelectedSlot = _slot;
                from_Inventory = _from;
                InteractionArea.Item = _from.Inventory[_slot];

                if (from_Inventory == Main_Inventory)
                {
                    InteractionArea.SwitchArea(InteractAreaType.GIVE);
                }
                if (from_Inventory == Secondary_Inventory)
                {
                    InteractionArea.SwitchArea(InteractAreaType.TAKE);
                }
            }
            else
            {
                BackInteractionArea();
            }
        }

        public void BackInteractionArea()
        {
            InteractionArea.SwitchArea(InteractAreaType.ALL);
            SelectedSlot = -1;
            from_Inventory = null;
        }
    }
}