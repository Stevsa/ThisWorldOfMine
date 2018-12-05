using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWoM.Characters;

namespace TWoM.Items
{
    public enum ClothingType
    {
        HEAD,
        FACE,
        EARS,
        HORNS,
        EYES,
        NECK,
        MUZZLE,
        WINGS,
        SHOLDER,
        TORSO,
        BACK,
        BELT,
        WRIST,
        HANDS,
        FINGERS,
        TAIL,
        UNDERFEET,
        FEET,
        LEGS,
        CROTCH
    }

    [System.Serializable]
    public class Clothing_Item : V_Item, IEquipable<P_Character>
    {
        public ClothingType ItemLocation { get; set; }

        public bool Equip(P_Character User)
        {
            V_P_Item returnItem;

            if (User.EquipItem(this,out returnItem))
            {
                if (returnItem != null)
                    User.AddItemtoInventory(new ItemSlot(returnItem, 1));
                return true;
            }

            return false;
        }

        public override void LoadItem(string sItem)
        {


            string SearchString = "Type ='";

            ItemLocation = (ClothingType)System.Enum.Parse(typeof(ClothingType), LoadStringParts(SearchString,sItem));

            base.LoadItem(sItem);
        }
    }
}