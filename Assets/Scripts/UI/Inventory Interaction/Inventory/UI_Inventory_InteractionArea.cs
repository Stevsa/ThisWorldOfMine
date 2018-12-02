using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWoM.UI.Inventroys
{
    public class UI_Inventory_InteractionArea : UI_P_InteractionArea
    {
        public GameObject All_InteractionArea;
        public GameObject Main_InteractionArea;
        public GameObject Secondary_InteractionArea;


        void Start()
        {

        }
        
        void Update()
        {

        }

        public void SwitchArea(InteractAreaType _type)
        {
            Debug.Log("Switch Area");
            All_InteractionArea.SetActive(false);
            Main_InteractionArea.SetActive(false);
            Secondary_InteractionArea.SetActive(false);
            switch (_type)
            {
                case InteractAreaType.ALL:
                    All_InteractionArea.SetActive(true);
                    break;
                case InteractAreaType.GIVE:
                    Main_InteractionArea.SetActive(true);
                    break;
                case InteractAreaType.TAKE:
                    Secondary_InteractionArea.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}