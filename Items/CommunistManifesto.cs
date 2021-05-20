using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CommunistTerraria.Items
{
    public class CommunistManifesto : ModItem
    {
        private static CommunistTerraria Mod => ModContent.GetInstance<CommunistTerraria>();
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Communist Manifesto");
            Tooltip.SetDefault("-Karl Marx & Friedrich Engels"
            + "\nLeft click when holding the manifesto to open the manifesto");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 46;

            item.rare = ItemRarityID.Red;
            item.useAnimation = 48;
            item.useTime = 48;
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