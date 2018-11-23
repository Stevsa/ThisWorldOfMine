using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWoM.Characters
{
    [System.Serializable]
    public class V_Character
    {
        public string FirstName;
        public List<string> MiddleNames;
        public string LastName;

        public int apparentAge;
        public int _Age;

        public int Cash;

        public LooksHolder charLooks;
        public PersonalityHolder charPersonality;
        public TraitsHolder charTraits;

        public Sprite UniqueSprite;
        public Sprite UniquePortrait;

        void Start()
        {

        }

        void Update()
        {

        }
    }
}