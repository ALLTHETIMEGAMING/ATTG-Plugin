using Harmony;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using RemoteAdmin;
using Smod2;

namespace ATTG_Command
{
    [HarmonyPatch(typeof(MTFRespawn))]
    [HarmonyPatch("SummonVan")]

    public class Patch
    {
        [HarmonyPatch]
        static bool Prefix(MTFRespawn __instance)
        {
            return false;
        }

        internal static void PatchMethodUsingHarmony()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("com.joker119.ghost");
            harmony.PatchAll();
        }
    }
}
