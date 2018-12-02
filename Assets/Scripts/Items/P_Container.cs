using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWoM.Items;
using TWoM.Characters;
using TWoM.UI.Inventroys;
using TWoM.UI;

namespace TWoM.Inworld
{
    public class P_Container : MonoBehaviour, IUseable<GameObject>
    {
        public List<ItemSlot> Inventory;

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

        public void Use(GameObject User)
        {
            if (User != null)
                if (User.GetComponent<P_Character>() != null)
                {
                    FindObjectOfType<UI_Middle_Interaction_Area>().OpenMenu(Middle_Menues.INVENTORY);
                    FindObjectOfType<UI_Controller_Inventory>().OpenInventoryFrom(User.GetComponent<P_Character>(), GetComponent<P_Container>());
                }
        }

        public void Close()
        {
            if (Inventory.Count <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}