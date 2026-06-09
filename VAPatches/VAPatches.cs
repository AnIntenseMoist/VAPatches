using BepInEx;
using BepInEx.Configuration;
using BepInEx.Harmony;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using ValheimAscended.Abilities;
using ValheimAscended.Config;
using ValheimAscended.Core;
using ValheimAscended.Progression;

namespace VAPatches
{
    [BepInPlugin(GUID:PluginGUID, Name:PluginName, Version:PluginVersion)]
    [BepInDependency("com.valheimascended.mod")]
    public class PatcherMain : BaseUnityPlugin
    {
        public const string PluginGUID = "com.anintensemoist.vapatches";
        public const string PluginName = "VA - Cooldown Patch";
        public const string PluginVersion = "0.0.1";

        public static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource(PluginName);

        private readonly Harmony _harmony = new Harmony(PluginGUID);

        private ConfigEntry<bool> configCooldownPatch;

        public void Awake()
        {
            logger.LogInfo($"Loading {PluginGUID}");

            configCooldownPatch = Config.Bind("General", "Enable Cooldown Patch", true, "Enables the cooldown patch.");


            if (configCooldownPatch.Value)
            {
                MethodInfo cdMethod = typeof(AbilitySystem).GetMethod(nameof(AbilitySystem.GetById), AccessTools.all);
                HarmonyMethod cdPatch = new HarmonyMethod(typeof(PatchVACooldowns), nameof(PatchVACooldowns.Postfix));
                _harmony.Patch(cdMethod, postfix: cdPatch);
            }
            logger.LogInfo("Finished patching.");
        }

        public static class PatchVACooldowns
        {
            static FieldInfo _cooldownField = AccessTools.Field(typeof(AbilityDef), "<Cooldown>k__BackingField");
            static readonly Dictionary<string, float> _baseCooldowns = new Dictionary<string, float>();
            public static void Postfix(ref AbilityDef __result)
            {
                PlayerData playerData = RenownSystem.GetOrCreate(Player.m_localPlayer);
                if (__result == null || playerData == null) { return; }
                try
                {
                    if (!_baseCooldowns.TryGetValue(__result.Id, out float baseCd))
                    {
                        baseCd = __result.Cooldown;
                        _baseCooldowns[__result.Id] = baseCd;
                    }
                    float cdFactor = System.Math.Max((float)0.1, 1 - (playerData.TotalINT * ModConfig.IntCooldownReductionPerPoint.Value));
                    _cooldownField.SetValue(__result, baseCd * cdFactor);
                }
                catch (System.Exception e)
                {
                    logger.LogError($"Error adjusting CD on GetById return: {e}");
                }
            }
        }
    }
}
