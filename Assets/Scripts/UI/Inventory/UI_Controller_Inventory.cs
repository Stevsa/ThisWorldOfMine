using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWoM.Items;
using TWoM.Characters;
using TWoM.Inworld;

namespace TWoM.UI.Inventroys
{
    public class UI_Controller_Inventory : MonoBehaviour
    {
        public UI_P_Inventory Main_Inventory;
        public UI_P_Inventory Secondary_Inventory;

        public P_Character Main_Charicter;

        public P_Character Secondary_Charicter;
        public P_Container Secondary_Container;

        public GameObject TempContainer;

        void Start()
        {

        }
        
        void Update()
        {

        }

        public void OpenInventoryFrom(P_Character _Character)
        {

            if (!FindObjectOfType<UI_Controller>().InMenu)
            {
                Main_Charicter = _Character;
                Secondary_Charicter = null;
                Secondary_Container = null;

                Main_Inventory.UpdateFrom(_Character);
                Secondary_Inventory.UpdateEmpty();
                FindObjectOfType<UI_Middle_Interaction_Area>().OpenFrom(gameObject);
            }
        }

        public void OpenInventoryFrom(P_Character _Character, P_Character _Secondary)
        {

            if (!FindObjectOfType<UI_Controller>().InMenu)
            {
                Main_Charicter = _Character;
                Secondary_Charicter = _Secondary;
                Secondary_Container = null;

                Main_Inventory.UpdateFrom(_Character);
                Secondary_Inventory.UpdateFrom(_Secondary);
                FindObjectOfType<UI_Middle_Interaction_Area>().OpenFrom(gameObject);
            }
        }

        public void OpenInventoryFrom(P_Character _Character, P_Container _Secondary)
        {

            if (!FindObjectOfType<UI_Controller>().InMenu)
            {
                Main_Charicter = _Character;
                Secondary_Charicter = null;
                Secondary_Container = _Secondary;

                Main_Inventory.UpdateFrom(_Character);
                Secondary_Inventory.UpdateFrom(_Secondary);
                FindObjectOfType<UI_Middle_Interaction_Area>().OpenFrom(gameObject);
            }
        }

        public void CloseInventorys()
        {
            Main_Charicter.Inventory = Main_Inventory.Inventory;

            if (Secondary_Charicter != null) Secondary_Charicter.Inventory = Secondary_Inventory.Inventory;
            if (Secondary_Container != null) Secondary_Container.Inventory = Secondary_Inventory.Inventory;

            if (Secondary_Charicter == null & Secondary_Container == null)
            {
                P_Container newContainer = Instantiate(TempContainer, Main_Charicter.transform.position, Quaternion.identity).GetComponent<P_Container>();
                newContainer.Inventory = Secondary_Inventory.Inventory;
            }

            //if (Secondary_Charicter != null) Secondary_Charicter.Inventory = Secondary_Inventory.Inventory;
            if (Secondary_Container != null) Secondary_Container.Close();
            
            FindObjectOfType<UI_Middle_Interaction_Area>().Close();
        }

        void InventoryUsed()
        {
            Main_Inventory.RebuildInventroy();
            Secondary_Inventory.RebuildInventroy();
        }
    }
}