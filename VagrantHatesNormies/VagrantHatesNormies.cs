using System.Reflection;
using BepInEx;
using RoR2;
using UnityEngine;
using R2API;
using R2API.Utils;

using System;
using System.IO;
using System.Linq;
using MonoMod.Cil;

namespace VagrantHatesNormies
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.Arbition.VagrantHatesNormies", "VagrantHatesNormies", "1.1.0")]
    [R2APISubmoduleDependency(nameof(SoundAPI))]
    public class VagrantHatesNormies : BaseUnityPlugin
    {
        public void Awake()
        {
            using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("VagrantHatesNormies.REEE.bnk"))
            {
                var bytes = new byte[bankStream.Length];
                bankStream.Read(bytes, 0, bytes.Length);
                SoundAPI.SoundBanks.Add(bytes);
            }

            On.RoR2.Util.PlayAttackSpeedSound += Util_PlayAttackSpeedSound;
        }

        private uint Util_PlayAttackSpeedSound(On.RoR2.Util.orig_PlayAttackSpeedSound orig, string soundString, GameObject gameObject, float playbackRate)
        {
            
            if (soundString == EntityStates.VagrantMonster.ChargeMegaNova.chargingSoundString)
            {
                //Debug.LogError(soundString);
                //Debug.Log("REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
                return AkSoundEngine.PostEvent(2769586434, gameObject);
            }
            return orig(soundString, gameObject, playbackRate);
        }
    }
}