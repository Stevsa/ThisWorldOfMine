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

        [TextArea]
        public string Info;
        [TextArea]
        public string ExtraInfo;

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

        public string LoadStringParts(string SearchString, string sItem)
        {
            return sItem.Substring(sItem.IndexOf(SearchString) + SearchString.Length, sItem.IndexOf("'", sItem.IndexOf(SearchString) + SearchString.Length) - (sItem.IndexOf(SearchString) + SearchString.Length));
        }

        public Sprite GetSpriteFromString(string _string)
        {
            if (_string.Contains(","))
            {
                string[] Parts = _string.Split(",".ToCharArray());
                Sprite[] SpriteAt = Resources.LoadAll<Sprite>(Parts[0]);
                for (int i = 0; i < SpriteAt.Length; i++)
                {
                    if (SpriteAt[i].name == Parts[1])
                    {
                        return SpriteAt[i];
                    }
                }
            }
            else
            {
                return Resources.Load<Sprite>(_string);
            }

            return null;
        }

        public virtual void LoadItem(string sItem)
        {
            string SearchString = "Name ='";
            name = LoadStringParts(SearchString, sItem);

            SearchString = "Stackable ='";
            Stackable = "TRUE" == LoadStringParts(SearchString, sItem);

            SearchString = "Sprite ='";
            string tSpriteAll = LoadStringParts(SearchString, sItem);
            Sprites = new List<Sprite>();
            if (tSpriteAll != "NULL")
            {
                string[] newSprites = tSpriteAll.Split("|".ToCharArray());

                for (int i = 0; i < newSprites.Length; i++)
                {
                    Sprites.Add(GetSpriteFromString(newSprites[i]));
                }
            }

            SearchString = "Colour ='";
            string tColourAll = LoadStringParts(SearchString, sItem);
            string[] newColours = tColourAll.Split("|".ToCharArray());
            Colours = new List<Color>();
            for (int i = 0; i < newColours.Length; i++)
            {
                Color newCol;
                ColorUtility.TryParseHtmlString(newColours[i], out newCol);
                Colours.Add(newCol);
            }

            SearchString = "Value ='";
            Value = int.Parse(LoadStringParts(SearchString, sItem));

            SearchString = "Enchantable ='";
            Enchantable = "TRUE" == LoadStringParts(SearchString, sItem);

            SearchString = "Info ='";
            Info = LoadStringParts(SearchString, sItem);

            SearchString = "Extra ='";
            ExtraInfo = LoadStringParts(SearchString, sItem);
            
        }
    }
}