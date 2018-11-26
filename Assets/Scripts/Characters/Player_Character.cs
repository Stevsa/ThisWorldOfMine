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

        void Start()
        {

        }

        void Update()
        {
            if (ObjectsInReach.Count > 0)
            {
                FindObjectOfType<UI_InteractionArea>().TopInteration = ObjectsInReach[0];
                if (Input.GetAxis("Use") > 0)
                {
                    InteravtWith(ObjectsInReach[0]);
                }
            }
            else
            {
                FindObjectOfType<UI_InteractionArea>().TopInteration = null;
            }
        }

        void FixedUpdate()
        {
            float moveH = Input.GetAxis("Horizontal");
            float moveV = Input.GetAxis("Vertical");

            GetComponent<Rigidbody2D>().AddForce(new Vector2(moveH, moveV) * speed);
            if (moveH == 0)
            {
                //GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            }
            if (moveV == 0)
            {
                //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,0);
            }
        }

        public void InteravtWith(GameObject @object)
        {
            Debug.Log("Interact Start");
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