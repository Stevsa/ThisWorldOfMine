using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWoM.Items;

namespace TWoM.Characters
{
    public class P_Character : MonoBehaviour
    {
        public string FirstName;
        public List<string> MiddleNames;
        public string LastName;

        public int apparentAge;
        private int _Age;

        public int Cash;

        public LooksHolder charLooks;
        public PersonalityHolder charPersonality;
        public TraitsHolder charTraits;

        public List<ItemSlot> Inventory;
        public int maxInventorySpaces;

        public SpriteHolder charSprites;

        public Sprite UniqueSprite;
        public Sprite UniquePortrait;

        public float speed;

        public List<GameObject> ObjectsInReach;

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            Debug.Log(rigidbody);

            if (rigidbody!=null)
            {
                Debug.Log(rigidbody.velocity);
                if (rigidbody.velocity == Vector2.zero)
                {
                    
                }
                else if (rigidbody.velocity.x > rigidbody.velocity.y && rigidbody.velocity.x > -rigidbody.velocity.y)
                {
                    GetComponent<SpriteRenderer>().sprite = charSprites.RightSprite;
                }
                else if (rigidbody.velocity.x < rigidbody.velocity.y && rigidbody.velocity.x < -rigidbody.velocity.y)
                {
                    GetComponent<SpriteRenderer>().sprite = charSprites.LeftSprite;
                }
                else if (rigidbody.velocity.y > rigidbody.velocity.x && rigidbody.velocity.x > -rigidbody.velocity.y)
                {
                    GetComponent<SpriteRenderer>().sprite = charSprites.UpSprite;
                }
                else if (rigidbody.velocity.y < rigidbody.velocity.x && rigidbody.velocity.x < -rigidbody.velocity.y)
                {
                    GetComponent<SpriteRenderer>().sprite = charSprites.DownSprite;
                }
            }


        }

        protected virtual void FixedUpdate()
        {

        }

        public virtual void CreateFromVirtual(V_Character charVirtural)
        {
            FirstName = charVirtural.FirstName;
            MiddleNames = charVirtural.MiddleNames;
            LastName = charVirtural.LastName;


            apparentAge = charVirtural.apparentAge;
            _Age = charVirtural._Age;
            Cash = charVirtural.Cash;
            charLooks = charVirtural.charLooks;
            charPersonality = charVirtural.charPersonality;
            charTraits = charVirtural.charTraits;

            UniqueSprite = charVirtural.UniqueSprite;
            UniquePortrait = charVirtural.UniquePortrait;
        }

        public virtual ItemSlot[] PickupFromInventroy(ItemSlot[] Inventory)
        {
            Debug.Log("Picking Up Inventroy");
            List<ItemSlot> newInventory = new List<ItemSlot>();
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] != null)
                    if (Inventory[i].VItem != null)
                        if (!AddItemtoInventory(Inventory[i]))
                        {
                            newInventory.Add(Inventory[i]);
                        }
            }

            return newInventory.ToArray();
        }

        public virtual bool AddItemtoInventory(ItemSlot ItemSlot)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].VItem != null)
                    if (Inventory[i].VItem.name == ItemSlot.VItem.name)
                        if (Inventory[i].VItem.Stackable)
                        {
                            Inventory[i].Quantity += ItemSlot.Quantity;
                            return true;
                        }
            }

            if (Inventory.Count < maxInventorySpaces)
            {
                Inventory.Add(new ItemSlot(ItemSlot.VItem, ItemSlot.Quantity));
                return true;
            }
            return false;
        }
    }
}