using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWoM.Characters
{
    public class Player_Character : P_Character
    {

        
        void Start()
        {

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
    }
}