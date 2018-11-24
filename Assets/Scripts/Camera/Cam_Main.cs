using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TWoM.Characters;

namespace TWoM.Camera
{
    public class Cam_Main : MonoBehaviour
    {
        public GameObject Follow;
        
        void Start()
        {

        }
        
        void Update()
        {
            if (Follow != null)
            {
                transform.position = Follow.transform.position + new Vector3(0,0,-10);
            }
            else
            {
                Follow = FindObjectOfType<Player_Character>().gameObject;
            }
        }
    }
}