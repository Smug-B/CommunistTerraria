using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CommunistTerraria
{
	public partial class CommunistTerraria : Mod
	{
		public static string SelectedBackground { get; internal set; } = "Flag Of The Soviet Union";

		private static (Texture2D texture, string name) InternalBackground;

		public static Texture2D OurBackground
		{
			get
			{
				if (InternalBackground.name == SelectedBackground)
				{
					return InternalBackground.texture;
				}

				string internalName = SelectedBackground.Replace(" ", string.Empty).Replace("'", string.Empty).Replace("!", string.Empty);
				Texture2D baseTexture = ModContent.GetTexture("CommunistTerraria/Textures/Background/" + internalName);
				InternalBackground.texture = ClonedTexture(baseTexture);
				InternalBackground.name = SelectedBackground;
				return InternalBackground.texture;
			}
		}

		private void Main_DrawSurfaceBG(On.Terraria.Main.orig_DrawSurfaceBG orig, Main self)
		{
			if (!Main.gameMenu)
			{
				orig.Invoke(self);
				return;
			}

			Main.spriteBatch.Draw(OurBackground, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
		}
	}
}