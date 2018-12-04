using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Text;
using TWoM.Items;
using TWoM.Characters;

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
                    TestItems.Add(CreateItemFromString(NewTexts[i]));
                }
            }
        }

        FindObjectOfType<Player_Character>().AddItemtoInventory(new ItemSlot(TestItems[1], 5));
        FindObjectOfType<Player_Character>().AddItemtoInventory(new ItemSlot(TestItems[0], 1));
    }

    V_P_Item CreateItemFromString(string str)
    {
        string SearchString = "<";
        string tClass = str.Substring(str.IndexOf(SearchString) + SearchString.Length, str.IndexOf(">", str.IndexOf(SearchString) + SearchString.Length) - (str.IndexOf(SearchString) + SearchString.Length));
        
        V_P_Item NewItem = null;
        NewItem = Activator.CreateInstance(Type.GetType("TWoM.Items." + tClass)) as V_P_Item;
        NewItem.LoadItem(str);

        return NewItem;
    }
	
	void Update () {
		
	}
}
