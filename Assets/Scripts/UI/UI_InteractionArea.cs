using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TWoM.Characters;
using TWoM.Items;
using TWoM.Inworld;

namespace TWoM.UI
{
    public class UI_InteractionArea : MonoBehaviour
    {
        public GameObject TopInteration;
        private GameObject _TopInteration;

        void Start()
        {
            GetComponentInChildren<Text>().text = "";
        }
        
        void Update()
        {
            if (_TopInteration != TopInteration)
            {
                _TopInteration = TopInteration;
                GetComponentInChildren<Text>().text = "";

                if (TopInteration != null)
                {
                    if (TopInteration.GetComponent<P_ItemBag>() != null)
                    {
                        GetComponentInChildren<Text>().text = "Interact with the Bag with the Key 'e'";
                    }
                }
            }
        }
    }
}