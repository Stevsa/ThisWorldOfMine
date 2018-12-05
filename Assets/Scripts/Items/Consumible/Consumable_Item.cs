using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWoM.Characters;

namespace TWoM.Items
{
    [System.Serializable]
    public class Consumable_Item : V_P_Item, IConsumable<P_Character>
    {
        public string Verb { get; set; }

        public bool Consume(P_Character User)
        {

            return false;
        }
    }
}