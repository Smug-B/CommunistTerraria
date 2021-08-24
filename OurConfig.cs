using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Terraria;
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
		[OptionStrings(new string[] { "Random", "State Anthem Of The USSR", "The Internationale", "The Artilleryman's Song", "March Of The Defenders Of Moscow", "To Serve Russia", "Red Sun In The Sky" })]
		[DefaultValue("Random")]
		[Label("Our choice of music")]
		public string MusicSelection;

		[Header("Background")]
		[DrawTicks]
		[BackgroundColor(255, 216, 0)]
		[SliderColor(205, 0, 0)]
		[OptionStrings(new string[] { "Random", "Flag Of The Soviet Union", "Raising A Flag Over The Reichstag", "Our Leaders", "The Victory Of Communism", "Stalin The Great Helmsman", "Our Army", "Our Triumph In Space", "Forward! Victory Is Near", "Comrades Stalin And Lenin", "First Man In Space", "Red Sun In Our Hearts" })]
		[DefaultValue("Random")]
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
		[OptionStrings(new string[] { "Random", "Comrades Stalin And Lenin", "Our Leaders", "First Man In Space", "Moscow", "Comrade Marx", "Comrade Engels", "Comrade Lenin", "Comrade Stalin", "Comrade Mao" })]
		[DefaultValue("Random")]
		[Label("Our choice of mod icon")]
		public string IconSelection;

		public override void OnChanged()
		{
			if (MusicSelection.Equals("Random"))
			{
				CommunistTerraria.SelectedMusic = Utils.SelectRandom(Main.rand, "State Anthem Of The USSR", "The Internationale", "The Artilleryman's Song", "March Of The Defenders Of Moscow", "To Serve Russia", "Red Sun In The Sky");
			}
			else if (CommunistTerraria.SelectedMusic != MusicSelection)
			{
				CommunistTerraria.SelectedMusic = MusicSelection;
			}

			if (BackgroundSelection.Equals("Random"))
			{
				CommunistTerraria.SelectedBackground = Utils.SelectRandom(Main.rand, "Flag Of The Soviet Union", "Raising A Flag Over The Reichstag", "Our Leaders", "The Victory Of Communism", "Stalin The Great Helmsman", "Our Army", "Our Triumph In Space", "Forward! Victory Is Near", "Comrades Stalin And Lenin", "First Man In Space", "Red Sun In Our Hearts");
			}
			else if (CommunistTerraria.SelectedBackground != BackgroundSelection)
			{
				CommunistTerraria.SelectedBackground = BackgroundSelection;
			}

			if (CommunistTerraria.SelectedLogo != LogoSelection)
			{
				CommunistTerraria.SelectedLogo = LogoSelection;
			}

			if (IconSelection.Equals("Random"))
			{
				CommunistTerraria.SelectedIcon = Utils.SelectRandom(Main.rand, "Comrades Stalin And Lenin", "Our Leaders", "First Man In Space", "Moscow", "Comrade Marx", "Comrade Engels", "Comrade Lenin", "Comrade Stalin", "Comrade Mao");

				if (CommunistTerraria.OurIcon != null)
				{
					CommunistTerraria.UpdateOurIconReflection();
				}
			}
			else if(CommunistTerraria.SelectedIcon != IconSelection)
			{
				CommunistTerraria.SelectedIcon = IconSelection;

				if (CommunistTerraria.OurIcon != null)
				{
					CommunistTerraria.UpdateOurIconReflection();
				}
			}
		}
	}
}