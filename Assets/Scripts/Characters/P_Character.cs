using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWoM.Items;

namespace TWoM.Characters
{
    public interface IInventoryHolder<in t>
    {
        List<ItemSlot> Inventory { get; set; }
        int maxInventorySpaces { get; set; }
        bool AddItemtoInventory(t _item);
    }

    public class P_Character : MonoBehaviour, IInventoryHolder<ItemSlot>
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

        public List<ItemSlot> Inventory { get; set; }
        public int maxInventorySpaces { get; set; }

        public List<V_P_Item> Equipment;

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

            if (rigidbody!=null)
            {
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

        public bool AddItemtoInventory(ItemSlot _item)
        {
            if (maxInventorySpaces == 0) maxInventorySpaces = 10;
            if (Inventory == null) Inventory = new List<ItemSlot>();

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

            if (Inventory.Count < maxInventorySpaces)
            {
                Inventory.Add(new ItemSlot(_item.VItem, _item.Quantity));
                return true;
            }
            return false;
        }

        public bool EquipItem(V_P_Item _item, out V_P_Item _outItem)
        {
            _outItem = null;
            for (int i = 0; i < Equipment.Count; i++)
            {
                if ((Equipment[i] as IEquipable<P_Character>).ItemLocation == (_item as IEquipable<P_Character>).ItemLocation)
                {
                    _outItem = Equipment[i];
                    Equipment.RemoveAt(i);
                    Equipment.Add(_item);
                    return true;
                }
            }

            if (_outItem == null)
            {
                Equipment.Add(_item);
                return true;
            }

            _outItem = null;
            return false;
        }
    }
}