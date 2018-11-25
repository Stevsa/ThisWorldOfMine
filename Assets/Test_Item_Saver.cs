using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Text;
using TWoM.Items;

public class Test_Item_Saver : MonoBehaviour
{
    public List<V_P_Item> TestItems;

    [TextArea]
    public string theText;

	void Start ()
    {
        TextAsset[] AllText = Resources.LoadAll<TextAsset>("Items");

        for (int J = 0; J < AllText.Length; J++)
        {
            TextAsset Text = AllText[J];
            theText = Text.text;
            //theText = TestItem.ItemSave();
            string[] NewTexts = theText.Split("\n".ToCharArray());

            for (int i = 0; i < NewTexts.Length; i++)
            {
                if (!NewTexts[i].Contains("//") && NewTexts[i].Contains("<"))
                {
                    Debug.Log(NewTexts[i].Length);
                    TestItems.Add(CreateItemFromString(NewTexts[i]));
                }
            }
        }


    }

    V_P_Item CreateItemFromString(string str)
    {
        string SearchString = "<";
        string tClass = str.Substring(str.IndexOf(SearchString) + SearchString.Length, str.IndexOf(">", str.IndexOf(SearchString) + SearchString.Length) - (str.IndexOf(SearchString) + SearchString.Length));
        
        V_P_Item NewItem = null;

        switch (tClass)
        {
            case "V_P_Item":
                NewItem = new V_P_Item();
                NewItem.LoadItem(str);
                break;
            case "V_Item":
                NewItem = new V_Item();
                NewItem.LoadItem(str);
                break;
            case "V_Book":
                NewItem = new V_Book();
                NewItem.LoadItem(str);
                break;
            case "Clothing_Item":
                NewItem = new Clothing_Item();
                NewItem.LoadItem(str);
                break;
            case "Consumable_Item":
                NewItem = new Consumable_Item();
                NewItem.LoadItem(str);
                break;
            case "Quest_Item":
                NewItem = new Quest_Item();
                NewItem.LoadItem(str);
                break;
            case "Unique_Item":
                NewItem = new Unique_Item();
                NewItem.LoadItem(str);
                break;
            case "Weapon_Item":
                NewItem = new Weapon_Item();
                NewItem.LoadItem(str);
                break;
            default:
                break;
        }

        return NewItem;
    }
	
	void Update () {
		
	}
}
