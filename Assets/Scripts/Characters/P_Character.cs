using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWoM.Characters
{
    public class P_Character : MonoBehaviour
    {
        public string FirstName;
        public List<string> MiddleNames;
        public string LastName;

        public int apparentAge;
        private int _Age;

        public int Cash;

        public LooksHolder charLooks;
        public PersonalityHolder charPersonality;
        public TraitsHolder charTraits;

        public SpriteHolder charSprites;

        public Sprite UniqueSprite;
        public Sprite UniquePortrait;

        public float speed;

        void Start()
        {

        }
        
        void Update()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

            if (rigidbody!=null)
            {
                if (rigidbody.velocity == Vector2.zero)
                {
                    
                }
                else if (rigidbody.velocity.x > rigidbody.velocity.y && rigidbody.velocity.x > -rigidbody.velocity.y)
                {
                    GetComponent<SpriteRenderer>().sprite = charSprites.RightSprite;
                }
                else if (rigidbody.velocity.x < rigidbody.velocity.y && rigidbody.velocity.x < -rigidbody.velocity.y)
                {
                    GetComponent<SpriteRenderer>().sprite = charSprites.LeftSprite;
                }
                else if (rigidbody.velocity.y > rigidbody.velocity.x && rigidbody.velocity.x > -rigidbody.velocity.y)
                {
                    GetComponent<SpriteRenderer>().sprite = charSprites.UpSprite;
                }
                else if (rigidbody.velocity.y < rigidbody.velocity.x && rigidbody.velocity.x < -rigidbody.velocity.y)
                {
                    GetComponent<SpriteRenderer>().sprite = charSprites.DownSprite;
                }
            }
        }

        public void CreateFromVirtual(V_Character charVirtural)
        {
            FirstName = charVirtural.FirstName;
            MiddleNames = charVirtural.MiddleNames;
            LastName = charVirtural.LastName;


            apparentAge = charVirtural.apparentAge;
            _Age = charVirtural._Age;
            Cash = charVirtural.Cash;
            charLooks = charVirtural.charLooks;
            charPersonality = charVirtural.charPersonality;
            charTraits = charVirtural.charTraits;

            UniqueSprite = charVirtural.UniqueSprite;
            UniquePortrait = charVirtural.UniquePortrait;
        }
    }
}