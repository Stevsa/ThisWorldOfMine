using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWoM.Items
{
    public enum ClothingType
    {
        HEAD,
        BODY,
        LEGS,
        FEET
    }

    [System.Serializable]
    public class Clothing_Item : V_Item
    {
        public ClothingType ItemLocation;

        public override void LoadItem(string sItem)
        {


            string SearchString = "Type ='";

            ItemLocation = (ClothingType)System.Enum.Parse(typeof(ClothingType), LoadStringParts(SearchString,sItem));
            Debug.Log(ItemLocation);

            base.LoadItem(sItem);
        }
    }
}