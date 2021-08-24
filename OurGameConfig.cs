using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CommunistTerraria
{
	[BackgroundColor(205, 0, 0)]
	public class OurGameConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[Header("Screenshader")]
		[Tooltip("Toggles the screenshader")]
		[Label("Our choice of whether to shade the screen")]
		[BackgroundColor(255, 216, 0)]
		[DefaultValue(false)]
		public bool ShaderActive;

		[DrawTicks]
		[BackgroundColor(255, 216, 0)]
		[SliderColor(205, 0, 0)]
		[OptionStrings(new string[] { "Flag Of The Soviet Union", "Raising A Flag Over The Reichstag", "Our Leaders", "The Victory Of Communism", "Stalin The Great Helmsman", "Our Army", "Our Triumph In Space", "Forward! Victory Is Near", "Comrades Stalin And Lenin", "First Man In Space", "Red Sun In Our Hearts" })]
		[DefaultValue("Flag Of The Soviet Union")]
		[Label("Our choice of screenshader")]
		public string ShaderSelection;

		[Range(0f, 1f)]
		[Increment(0.01f)]
		[BackgroundColor(255, 216, 0)]
		[DefaultValue(0.5f)]
		[Label("Our choice of screenshader intensity")]
		public float ShaderIntensity;

		[Header("Our Night Sky")]
		[DrawTicks]
		[BackgroundColor(255, 216, 0)]
		[SliderColor(205, 0, 0)]
		[OptionStrings(new string[] { "Hammer And Sickle", "Red Star"})]
		[DefaultValue("Hammer And Sickle")]
		[Label("Our choice of stars")]
		public string StarSelection;

		[DrawTicks]
		[BackgroundColor(255, 216, 0)]
		[SliderColor(205, 0, 0)]
		[OptionStrings(new string[] { "Comrade Marx", "Comrade Engels", "Comrade Lenin", "Comrade Stalin", "Comrade Mao", "Hammer And Sickle" })]
		[DefaultValue("Comrade Marx")]
		[Label("Our choice of moon")]
		public string MoonSelection;

		public override void OnChanged()
		{
			if (CommunistTerraria.OurShaderActive != ShaderActive)
			{
				CommunistTerraria.OurShaderActive = ShaderActive;
			}

			if (CommunistTerraria.SelectedReference != ShaderSelection)
			{
				CommunistTerraria.SelectedReference = ShaderSelection;
			}

			if (CommunistTerraria.OurShaderIntensity != ShaderIntensity)
			{
				CommunistTerraria.OurShaderIntensity = ShaderIntensity;
			}

			if (CommunistTerraria.SelectedStars != StarSelection)
			{
				CommunistTerraria.SelectedStars = StarSelection;
			}

			if (CommunistTerraria.SelectedMoon != MoonSelection)
			{
				CommunistTerraria.SelectedMoon = MoonSelection;
			}
		}
	}
}