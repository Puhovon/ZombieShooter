using System;
using UnityEngine;

namespace General
{
    public class SoundControler : MonoBehaviour
    {
        [SerializeField] private AkBank _bank;

        private void Start()
        {
            AkSoundEngine.SetRTPCValue("New_Game_Parameter", 0f);
        }
    }
}