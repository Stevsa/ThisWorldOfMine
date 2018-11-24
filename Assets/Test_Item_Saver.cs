using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TWoM.Items;

public class Test_Item_Saver : MonoBehaviour
{
    public V_P_Item TestItem;
    public string theText;

	void Start () {
        TextAsset text = Resources.Load("Items/Test_Items.txt") as TextAsset;

        theText = text.text;
	}
	
	void Update () {
		
	}
}
