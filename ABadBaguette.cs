using BepInEx;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;

namespace ABadBaguette
{
    [BepInPlugin("TheRonTron.BadBaguette", "ABadBaguette", "4.2.0")]
    [BepInDependency(Jotunn.Main.ModGuid)]
    internal class ABadBaguette : BaseUnityPlugin
    {
        private AssetBundle BadBaguette;
        public void Awake()
        {
            Jotunn.Logger.ShowDate = true;
            Game.isModded = true;

            Jotunn.Logger.LogInfo($"Embedded resources: {string.Join(",", typeof(ABadBaguette).Assembly.GetManifestResourceNames())}");
            BadBaguette = AssetUtils.LoadAssetBundleFromResources("badbaguette", typeof(ABadBaguette).Assembly);

            var ABadBaguette = BadBaguette.LoadAsset<GameObject>("BadBaguette");
            ItemConfig BreadConfig = new ItemConfig();

            var incineratorConfig = new IncineratorConversionConfig();
            incineratorConfig.Requirements.Add(new IncineratorRequirementConfig("BreadDough", 1));
            incineratorConfig.Requirements.Add(new IncineratorRequirementConfig("SwordBronze", 1));
            incineratorConfig.ToItem = "BadBaguette";
            incineratorConfig.Station = Incinerators.Incinerator;
            incineratorConfig.ProducedItems = 1;
            incineratorConfig.RequireOnlyOneIngredient = false;
            incineratorConfig.Priority = 5;
            ItemManager.Instance.AddItemConversion(new CustomItemConversion(incineratorConfig));

            ItemManager.Instance.AddItem(new CustomItem(ABadBaguette, fixReference: true, BreadConfig));

            BadBaguette.Unload(false);
        }
    }
}
