using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TWoM.Items
{
    public interface IUseable<in t>
    {
        void Use(t User);
    }

    [System.Serializable]
    public class ItemSlot
    {
        public V_P_Item VItem;
        public int Quantity;

        public ItemSlot(V_P_Item item,int quantity)
        {
            VItem = item;
            Quantity = quantity;
        }
    }

    [System.Serializable]
    public class V_P_Item
    {
        public string name;
        public bool Stackable;

        public List<Sprite> Sprites;
        public List<Color> Colours;

        public int Value;

        public bool Enchantable;

        protected string SaveString = "";

        public string ItemSave()
        {
            if (SaveString.Length==0)
            {
                SaveString += "<V_P_Item>";
            }
            SaveString += "<Name ='" + name + "'>";
            SaveString += "<Stackable ='" + Stackable.ToString() + "'>";
            /*
            if (Portrait != null)
            {
                SaveString += "<Portrait ='" + AssetDatabase.GetAssetPath(Portrait).ToString() + "', ";
            }
            else
            {
                SaveString += "<Portrait ='NULL', ";
            }
            if (SmallSprite != null)
            {
                SaveString += "Small Sprite ='" + AssetDatabase.GetAssetPath(SmallSprite).ToString().ToString() + "'>";
            }
            else
            {
                SaveString += "Small Sprite ='NULL'>";
            }
            */
            SaveString += "<Value ='" + Value + "'>";
            SaveString += "<Enchantable =" + Enchantable.ToString() + ">";

            return SaveString;
        }

        public string LoadItem(string sItem)
        {
            string SearchString = "Name ='";
            name = sItem.Substring(sItem.IndexOf(SearchString) + SearchString.Length, sItem.IndexOf("'", sItem.IndexOf(SearchString) + SearchString.Length) - (sItem.IndexOf(SearchString) + SearchString.Length));

            SearchString = "Stackable ='";
            Stackable = "TRUE" == sItem.Substring(sItem.IndexOf(SearchString) + SearchString.Length, sItem.IndexOf("'", sItem.IndexOf(SearchString) + SearchString.Length) - (sItem.IndexOf(SearchString) + SearchString.Length));

            /*
            SearchString = "Portrait ='";
            string tPortrait = sItem.Substring(sItem.IndexOf(SearchString) + SearchString.Length, sItem.IndexOf("'", sItem.IndexOf(SearchString) + SearchString.Length) - (sItem.IndexOf(SearchString) + SearchString.Length));
            Portrait = Resources.Load<Sprite>(tPortrait);

            SearchString = "Small Sprite ='";
            string tSmallSprite = sItem.Substring(sItem.IndexOf(SearchString) + SearchString.Length, sItem.IndexOf("'", sItem.IndexOf(SearchString) + SearchString.Length) - (sItem.IndexOf(SearchString) + SearchString.Length));
            SmallSprite = Resources.Load<Sprite>(tSmallSprite);
            */
            
            SearchString = "Value ='";
            Value = int.Parse(sItem.Substring(sItem.IndexOf(SearchString) + SearchString.Length, sItem.IndexOf("'", sItem.IndexOf(SearchString) + SearchString.Length) - (sItem.IndexOf(SearchString) + SearchString.Length)));

            SearchString = "Enchantable ='";
            Enchantable = "TRUE" == sItem.Substring(sItem.IndexOf(SearchString) + SearchString.Length, sItem.IndexOf("'", sItem.IndexOf(SearchString) + SearchString.Length) - (sItem.IndexOf(SearchString) + SearchString.Length));


            return sItem;
        }
    }
}