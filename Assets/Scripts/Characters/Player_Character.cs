using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TWoM.UI;
using TWoM.Inworld;
using TWoM.Items;

namespace TWoM.Characters
{
    public class Player_Character : P_Character
    {

        protected override void Start()
        {

        }

        protected override void Update()
        {
            if (ObjectsInReach.Count > 0)
            {
                FindObjectOfType<UI_InteractionArea>().TopInteration = ObjectsInReach[0];
                if (Input.GetAxis("Use") > 0)
                {
                    InteractWith(ObjectsInReach[0]);
                }
            }
            else
            {
                FindObjectOfType<UI_InteractionArea>().TopInteration = null;
            }

            base.Update();
        }

        protected override void FixedUpdate()
        {
            float moveH = Input.GetAxis("Horizontal");
            float moveV = Input.GetAxis("Vertical");

            if (!FindObjectOfType<UI.UI_Controller>().InMenu)
                GetComponent<Rigidbody2D>().AddForce(new Vector2(moveH, moveV) * speed);
        }

        public void InteractWith(GameObject @object)
        {
            Debug.Log("Interact Start");
            if (!FindObjectOfType<UI.UI_Controller>().InMenu)
                if (@object != null)
                {
                    Debug.Log("Somthing There");
                    if (@object.GetComponent(typeof(IUseable<GameObject>)) != null)
                    {
                        Debug.Log("Interactable");
                        IUseable<GameObject> Using = @object.GetComponent(typeof(IUseable<GameObject>)) as IUseable<GameObject>;
                        Using.Use(gameObject);
                    }
                }
        }
    }
}