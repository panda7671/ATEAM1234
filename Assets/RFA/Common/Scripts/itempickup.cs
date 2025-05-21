using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.ThirdPersonCharacter
{    public class itempickup : MonoBehaviour
    {
        public int Ammo;
        public int Coin;
        public int Health;
        public int HasGrenades;

        public int MaxAmmo;
        public int MaxCoin;
        public int MaxHealth;
        public int MaxHasGrenades;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnTriggerEnter(Collider other)
        {
            
        }
    }
}

