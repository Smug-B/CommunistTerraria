using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CommunistTerraria.Items
{
    public class ManifestoItem : ModItem
    {
        private static CommunistTerraria Mod => ModContent.GetInstance<CommunistTerraria>();
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The communist manifesto!");
            Tooltip.SetDefault("Written by Karl Marx and Friedrich Engels\n" +
                               "Left click when holding the manifesto to open the manifesto");
        }

        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.rare = ItemRarityID.Red;
            item.useAnimation = 45;
            item.useTime = 45;
            // item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingUp;
        }

        public override bool UseItem(Player player)
        {
            Mod.ToggleManifestoUI();
            
            return true;
        }
    }
}