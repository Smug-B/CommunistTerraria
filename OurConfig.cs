using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CommunistTerraria
{
	[BackgroundColor(205, 0, 0)]
	public class OurConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Header("Music")]
		[DrawTicks]
		[BackgroundColor(255, 216, 0)]
		[SliderColor(205, 0, 0)]
		[OptionStrings(new string[] { "State Anthem Of The USSR", "The Internationale", "The Artilleryman's Song", "March Of The Defenders Of Moscow", "To Serve Russia" })]
		[DefaultValue("State Anthem Of The USSR")]
		[Label("Our choice of music")]
		public string MusicSelection;

		[Header("Background")]
		[DrawTicks]
		[BackgroundColor(255, 216, 0)]
		[SliderColor(205, 0, 0)]
		[OptionStrings(new string[] { "Flag Of The Soviet Union", "Raising A Flag Over The Reichstag", "Our Leaders", "The Victory Of Communism", "Stalin The Great Helmsman", "Our Army", "Our Triumph In Space", "Forward! Victory Is Near", "Comrades Stalin And Lenin", "First Man In Space" })]
		[DefaultValue("Flag Of The Soviet Union")]
		[Label("Our choice of background")]
		public string BackgroundSelection;

		[Header("Logo")]
		[DrawTicks]
		[BackgroundColor(255, 216, 0)]
		[SliderColor(205, 0, 0)]
		[OptionStrings(new string[] { "State Emblem Of The Soviet Union", "Hammer And Sickle", "No Icon" })]
		[DefaultValue("State Emblem Of The Soviet Union")]
		[Label("Our choice of logo")]
		public string LogoSelection;

		[Header("Icon")]
		[DrawTicks]
		[BackgroundColor(255, 216, 0)]
		[SliderColor(205, 0, 0)]
		[OptionStrings(new string[] { "Comrades Stalin And Lenin", "Our Leaders", "First Man In Space", "Moscow", "Comrade Marx", "Comrade Engels", "Comrade Lenin", "Comrade Stalin" })]
		[DefaultValue("Comrades Stalin And Lenin")]
		[Label("Our choice of mod icon")]
		public string IconSelection;

		public override void OnChanged()
		{
			if (CommunistTerraria.SelectedMusic != MusicSelection)
			{
				CommunistTerraria.SelectedMusic = MusicSelection;
			}

			if (CommunistTerraria.SelectedBackground != BackgroundSelection)
			{
				CommunistTerraria.SelectedBackground = BackgroundSelection;
			}

			if (CommunistTerraria.SelectedLogo != LogoSelection)
			{
				CommunistTerraria.SelectedLogo = LogoSelection;
			}

			if (CommunistTerraria.SelectedIcon != IconSelection)
			{
				CommunistTerraria.SelectedIcon = IconSelection;

				if (CommunistTerraria.OurIcon is null)
				{
					return;
				}

				CommunistTerraria.UpdateOurIconReflection();
			}
		}
	}
}