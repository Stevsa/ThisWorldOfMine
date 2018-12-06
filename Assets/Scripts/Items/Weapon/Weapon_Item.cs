using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWoM.Characters;

namespace TWoM.Items
{
    [System.Serializable]
    public class Weapon_Item : V_P_Item, IEquipable<P_Character>
    {
        public ClothingType ItemLocation { get; set; }

        public bool Equip(P_Character User)
        {

            return false;
        }
    }
}