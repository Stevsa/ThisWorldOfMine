using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWoM.Items;
using TWoM.Characters;

namespace TWoM.Inworld
{
    public class P_ItemBag : MonoBehaviour, IUseable<GameObject>
    {
        public List<ItemSlot> Holding;
        
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
            Debug.Log("Bag Being Used");
            if (User!=null)
                if (User.GetComponent<P_Character>() != null)
                {
                    Debug.Log("User is Charicter");
                    ItemSlot[] Newholding = User.GetComponent<P_Character>().PickupFromInventroy(Holding.ToArray());
                    Holding = new List<ItemSlot>();
                    Holding.AddRange(Newholding);
                    if (Holding.Count < 1)
                    {
                        Destroy(gameObject);
                    }
                }
        }
    }
}