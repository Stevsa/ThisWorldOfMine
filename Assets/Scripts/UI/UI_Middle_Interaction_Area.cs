using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWoM.UI.Inventroys;

namespace TWoM.UI
{
    public enum Middle_Menues
    {
        NONE,
        INVENTORY
    }


    public class UI_Middle_Interaction_Area : MonoBehaviour
    {
        public GameObject OpenFor;

        
        void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        
        void Update()
        {

        }

        public void OpenMenu(Middle_Menues menu)
        {
            switch (menu)
            {
                case Middle_Menues.NONE:
                    Close();
                    break;
                case Middle_Menues.INVENTORY:
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).GetComponent<UI_Controller_Inventory>() != null)
                        {
                            transform.GetChild(i).gameObject.SetActive(true);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void OpenFrom(GameObject Opener)
        {
            OpenFor = Opener;
            
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            OpenFor.SetActive(true);
        }

        public void Close()
        {
            OpenFor = null;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}