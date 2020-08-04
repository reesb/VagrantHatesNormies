using System.Reflection;
using BepInEx;
using R2API.AssetPlus;
using RoR2;
using UnityEngine;

using System;
using System.IO;
using System.Linq;
using MonoMod.Cil;

namespace VagrantHatesNormies
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.Arbition.VagrantHatesNormies", "VagrantHatesNormies", "1.0.0")]
    public class VagrantHatesNormies : BaseUnityPlugin
    {
        public static void AddSoundBank()
        {
            byte[] array = VagrantHatesNormies.LoadEmbeddedResource("VagrantHatesNormies.REEE.bnk");
            if (array != null)
            {
                //Debug.Log("VagrantHatesNormies soundbank loaded!");
                SoundBanks.Add(array);
                return;
            }
            Debug.LogError("VagrantHatesNormies soundBank fetching failed");
        }

        private static byte[] LoadEmbeddedResource(string resourceName)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            resourceName = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(resourceName));
            byte[] result;
            using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(resourceName))
            {
                Stream stream = manifestResourceStream;
                if (stream == null)
                {
                    throw new InvalidOperationException();
                }
                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    result = binaryReader.ReadBytes(Convert.ToInt32(manifestResourceStream.Length.ToString()));
                }
            }
            return result;
        }
        public void Awake()
        {
            VagrantHatesNormies.AddSoundBank();
            On.RoR2.Util.PlaySound_string_GameObject += Util_PlaySound_string_GameObject;
        }


        private uint Util_PlaySound_string_GameObject(On.RoR2.Util.orig_PlaySound_string_GameObject orig, string soundString, UnityEngine.GameObject gameObject)
        {
            if (soundString == "Play_vagrant_R_charge")
            {
                return AkSoundEngine.PostEvent(2769586434, gameObject);
            }
            return orig(soundString, gameObject);
        }
    }
}