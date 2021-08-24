using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CommunistTerraria
{
	public partial class CommunistTerraria : Mod
	{
		public static bool OurShaderActive = false;

		public static float OurShaderIntensity;

		public static string SelectedReference { get; internal set; } = "Flag Of The Soviet Union";

		public void HandleOurShader()
		{
			if (!OurShaderActive)
			{
				Filters.Scene["OurScreenShader"].GetShader().Shader.Parameters["active"].SetValue(false);
				Filters.Scene["OurScreenShader"].Deactivate();
				return;
			}

			if (!Filters.Scene["OurScreenShader"].IsActive())
			{
				Filters.Scene.Activate("OurScreenShader");
			}

			ScreenShaderData ourScreenShaderData = Filters.Scene["OurScreenShader"].GetShader();
			ourScreenShaderData.UseIntensity(OurShaderIntensity);
			string internalName = SelectedReference.Replace(" ", string.Empty).Replace("'", string.Empty).Replace("!", string.Empty);
			ourScreenShaderData.UseImage(ModContent.GetTexture("CommunistTerraria/Textures/Background/" + internalName));
			ourScreenShaderData.Shader.Parameters["active"].SetValue(true);
		}
	}
}