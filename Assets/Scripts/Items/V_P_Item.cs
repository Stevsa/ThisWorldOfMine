using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWoM.Items
{
    public class ItemSlot
    {
        public V_P_Item VItem;
        public int Quantity;
    }

    public class V_P_Item
    {
        public string name;
        public bool Stackable;

        public Sprite Portrait;
        public Sprite SmallSprite;

        public int Value;

        public bool Enchantable;
    }
}