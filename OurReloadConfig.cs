using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CommunistTerraria
{
	[BackgroundColor(205, 0, 0)]
	public class OurReloadConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Header("Misc ( Note: These changes will cause a reload )")]

		[Tooltip("Modifies our game text by appending a hammer and sickle at the end.")]
		[Label("Hammer and Sickle suffix")]
		[BackgroundColor(255, 216, 0)]
		[ReloadRequired]
		[DefaultValue(false)]
		public bool HammerAndSickleSuffix;

		[Tooltip("Modifies our game text by appending the word 'Our' at the beginning.")]
		[Label("'Our' prefix")]
		[BackgroundColor(255, 216, 0)]
		[ReloadRequired]
		[DefaultValue(false)]
		public bool OurPrefix;

		[Tooltip("Allows Us to commify Our images")]
		[Label("Image Commifier")]
		[BackgroundColor(255, 216, 0)]
		[ReloadRequired]
		[DefaultValue(false)]
		public bool OurSuperCommunismMode;

		public override void OnChanged()
		{
			if (CommunistTerraria.HammerAndSickleSuffix != HammerAndSickleSuffix)
			{
				CommunistTerraria.HammerAndSickleSuffix = HammerAndSickleSuffix;
			}

			if (CommunistTerraria.OurPrefix != OurPrefix)
			{
				CommunistTerraria.OurPrefix = OurPrefix;
			}

			if (CommunistTerraria.CommifyImages != OurSuperCommunismMode)
			{
				CommunistTerraria.CommifyImages = OurSuperCommunismMode;
			}
		}
	}
}