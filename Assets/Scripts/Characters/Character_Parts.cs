using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWoM.Characters
{
    public enum BodyParts
    {
        SCALP,
        HORNS,
        FACE,
        EARS,
        NOSE,
        EYES,
        FOREHEAD,
        MOUTH,
        MUZZLE,
        CHIN,
        NECK,
        SHOULDERS,
        CHEST,
        BELLY,
        FOREARMS,
        ARMS,
        WRIST,
        HANDS,
        FINGERS,
        BACK,
        WINGS,
        TAIL,
        BOTTOM,
        CROTCH,
        PENIS,
        VAGINA,
        ANUS,
        HIP,
        FORELEGS,
        LEGS,
        ANKLES,
        FEET,
        TOES
    }

    [System.Serializable]
    public class ALook
    {
        public P_Species species;
        public BodyParts? BodyPart;

        public ALook()
        {
            species = null;
            BodyPart = null;
        }
        public ALook(P_Species _Species, BodyParts _parts)
        {
            species = _Species;
            BodyPart = _parts;
        }
    }
    [System.Serializable]
    public class MultiLook : ALook
    {
        public List<P_Species> Otherspecies;
        
    }

    [System.Serializable]
    public class LooksHolder
    {
        List<ALook> AllLooks;

        public LooksHolder(P_Species _Species)
        {
            AllLooks = new List<ALook>();
            for (int i = 0; i < System.Enum.GetNames(typeof(BodyParts)).Length; i++)
            {
                if (_Species.possibleParts.Contains((BodyParts)i))
                    AllLooks.Add(new ALook(_Species, (BodyParts)i));
                else
                    AllLooks.Add(new ALook(null , (BodyParts)i));
            }
        }

        public void ChangePart(ALook _newLook)
        {
            for (int i = 0; i < AllLooks.Count; i++)
            {
                if (AllLooks[i].BodyPart == _newLook.BodyPart)
                {
                    AllLooks[i] = new ALook
                    {
                        BodyPart = _newLook.BodyPart,
                        species = _newLook.species
                    };
                }
            }
        }
        public void ChangePart(MultiLook _newLook)
        {
            for (int i = 0; i < AllLooks.Count; i++)
            {
                if (AllLooks[i].BodyPart == _newLook.BodyPart)
                {
                    AllLooks[i] = new MultiLook
                    {
                        BodyPart = _newLook.BodyPart,
                        Otherspecies = _newLook.Otherspecies,
                        species = _newLook.species
                    };
                }
            }
        }
    }

    [System.Serializable]
    public class TraitsHolder
    {

    }

    [System.Serializable]
    public class PersonalityHolder
    {

    }


    [System.Serializable]
    public class SpriteHolder
    {
        public Sprite UpSprite;
        public Sprite LeftSprite;
        public Sprite DownSprite;
        public Sprite RightSprite;
    }
}
