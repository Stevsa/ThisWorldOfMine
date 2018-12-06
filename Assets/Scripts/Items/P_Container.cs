using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWoM.Items;
using TWoM.Characters;
using TWoM.UI.Inventroys;
using TWoM.UI;

namespace TWoM.Inworld
{
    public class P_Container : MonoBehaviour, IUseable<GameObject>, IInventoryHolder<ItemSlot>
    {
        public List<ItemSlot> Inventory { get; set; }
        public int maxInventorySpaces { get; set; }

        public int Amount { get; set; }

        void Start()
        {

        }

        void Update()
        {

        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<P_Character>() != null)
            {
                if (!collision.gameObject.GetComponent<P_Character>().ObjectsInReach.Contains(gameObject))
                    collision.gameObject.GetComponent<P_Character>().ObjectsInReach.Add(gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<P_Character>() != null)
            {
                if (collision.gameObject.GetComponent<P_Character>().ObjectsInReach.Contains(gameObject))
                    collision.gameObject.GetComponent<P_Character>().ObjectsInReach.Remove(gameObject);
            }
        }

        public bool Use(GameObject User)
        {
            if (User != null)
                if (User.GetComponent<P_Character>() != null)
                {
                    FindObjectOfType<UI_Middle_Interaction_Area>().OpenMenu(Middle_Menues.INVENTORY);
                    FindObjectOfType<UI_Controller_Inventory>().OpenInventoryFrom(User.GetComponent<P_Character>(), GetComponent<P_Container>());
                    return true;
                }
            return false;
        }

        public void Close()
        {
            Debug.Log(Inventory.Count);
            if (Inventory.Count <= 0)
            {
                Destroy(gameObject);
            }
        }

        
        public virtual bool AddItemtoInventory(ItemSlot _item)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].VItem != null)
                    if (Inventory[i].VItem.name == _item.VItem.name)
                        if (Inventory[i].VItem.Stackable)
                        {
                            Inventory[i].Quantity += _item.Quantity;
                            return true;
                        }
            }

            Inventory.Add(new ItemSlot(_item.VItem, _item.Quantity));
            return true;
        }
    }
}