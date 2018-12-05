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

        public void CreateNewContainer(List<ItemSlot> _items)
        {
            P_Container newContainer = Instantiate(TempContainer, new Vector3(Main_Charicter.transform.position.x, Main_Charicter.transform.position.y,-1), Quaternion.identity).GetComponent<P_Container>();
            newContainer.maxInventorySpaces = 15;
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
                InteractionArea.Equiped = false;

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

        public void SelectEquipmentSlot(int _slot, UI_P_Inventory _from)
        {
            SelectedSlot = _slot;
            from_Inventory = _from;
            InteractionArea.Item = new ItemSlot(_from.Equipment[_slot],1);
            InteractionArea.Equiped = true;

            if (from_Inventory == Main_Inventory)
            {
                InteractionArea.SwitchArea(InteractAreaType.GIVE);
            }
            if (from_Inventory == Secondary_Inventory)
            {
                InteractionArea.SwitchArea(InteractAreaType.TAKE);
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