using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using System;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace CommunistTerraria
{
	public partial class CommunistTerraria : Mod
	{
		public CommunistTerraria()
		{
			Mod = this;
			InternalLogoName = string.Empty;
		}

		public static Mod Mod { get; private set; }

		public static string OurRight { get; internal set; } = "Founded: 29th of December, 1922, Moscow";

		internal static bool SafeForAssets { get; private set; } = false;

		public static Texture2D ClonedTexture(Texture2D refTexture)
		{
			Color[] textureData = new Color[refTexture.Width * refTexture.Height];
			refTexture.GetData(textureData);
			Texture2D newTexture = new Texture2D(Main.instance.GraphicsDevice, refTexture.Width, refTexture.Height);
			newTexture.SetData(textureData);
			return newTexture;
		}

		public override void Close()
		{
			SafeForAssets = false;

			void CloseMusicStream(int slot)
			{
				if (Main.music.IndexInRange(slot) && Main.music[slot]?.IsPlaying == true)
				{
					Main.music[slot].Stop(AudioStopOptions.Immediate);
				}
			}

			CloseMusicStream(GetSoundSlot(SoundType.Music, "Sounds/Music/StateAnthemOfTheUSSR"));
			CloseMusicStream(GetSoundSlot(SoundType.Music, "Sounds/Music/TheInternationale"));
			CloseMusicStream(GetSoundSlot(SoundType.Music, "Sounds/Music/TheArtillerymansSong"));
			CloseMusicStream(GetSoundSlot(SoundType.Music, "Sounds/Music/MarchOfTheDefendersOfMoscow"));
			CloseMusicStream(GetSoundSlot(SoundType.Music, "Sounds/Music/ToServeRussia"));
			base.Close();
		}

		public override void Load()
		{
			if (!Main.dedServ)
			{
				Filters.Scene["OurScreenShader"] = new Filter(new ScreenShaderData(new Ref<Effect>(GetEffect("Effects/OurScreenShader")), "OurScreenShader"), EffectPriority.VeryHigh);
				Filters.Scene["OurScreenShader"].Load();
			}
		}

		public override void PostSetupContent()
		{
			SafeForAssets = true;

			DecideOurTitle();

			UpdateOurLogo();

			SetupOurStaticIconFields();

			UpdateOurIconReflection();

			CommifyOurText();

			if (CommifyImages)
			{
				CommifyOurImages();
			}

			if (InternalBackground.texture is null) // tMod disposes all textures on unload, so we need a clone to ensure a crash does not happen
			{
				Texture2D baseTexture = ModContent.GetTexture("CommunistTerraria/Textures/Background/FlagOfTheSovietUnion");
				InternalBackground.texture = ClonedTexture(baseTexture);
				InternalBackground.name = "Flag Of The Soviet Union";
			}

			UpdateOurIcon();

			IL.Terraria.Main.UpdateAudio += Main_UpdateAudio;
			IL.Terraria.Main.DrawMenu += Main_DrawMenu;
			On.Terraria.Main.DrawSurfaceBG += Main_DrawSurfaceBG;
			On.Terraria.Main.DrawMenu += Main_DrawMenu1;
			On.Terraria.UI.UIElement.Append += UIElement_Append;
			On.Terraria.UI.UIElement.Update += UIElement_Update;
		}

		public override void Unload()
		{
			UnloadOurTextChanges();
		}

		private static void DecideOurTitle()
		{
			string moniker = "Union of Soviet Socialist Republics";

			DateTime today = DateTime.Now;

			if (today.Month == 4 && today.Day == 22)
			{
				Main.instance.Window.Title = moniker + ": Happy birthday to comrade Lenin, founder of the greatest republic!";
				return;
			}

			if (today.Month == 5 && today.Day == 5)
			{
				Main.instance.Window.Title = moniker + ": Happy birthday to Marx, the first abolitionist for capitalism!";
				return;
			}

			if (today.Month == 5 && today.Day == 9)
			{
				Main.instance.Window.Title = moniker + ": " + (today.Year - 1945) + " years ago today, tyranny was crushed under the might of communism!";
				return;
			}

			if (today.Month == 12 && today.Day == 18)
			{
				Main.instance.Window.Title = moniker + ": Happy birthday to comrade Stalin, who won the Great Patriotic War and defeated facist tyranny!";
				return;
			}

			if (today.Month == 12 && today.Day == 26)
			{
				Main.instance.Window.Title = moniker + ": A somber day, as all great things must come to an end.";
				return;
			}

			if (today.Month == 12 && today.Day == 30)
			{
				Main.instance.Window.Title = moniker + ": " + (today.Year - 1922) + " years ago today, we saw the birth of the greatest republic!";
				return;
			}

			if (Main.instance.Window.Title.Contains(moniker))
			{
				return;
			}

			string[] possibleAppendages = new string[]
			{
				"Workers of the world, unite!",
				"Everything for the front!",
				"A specter is haunting Europe—the specter of Communism.",
				"Let the ruling classes tremble at a Communistic revolution!",
				"The proletarians have nothing to loose but their chains. They have a world to win.",
				"The history of all hitherto existing societies is the history of class struggles.",
				"The bourgeois sees in his wife a mere instrument of production.",
				"When there is state there can be no freedom, but when there is freedom there will be no state.",
				"That today, when the wave has ebbed, there remain and will remain only real Marxists, does not frighten us but rejoices us.",
				"All official and liberal science defends wage-slavery, whereas Marxism has declared relentless war on that slavery.",
				"Capital, created by the labour of the worker, crushes the worker, ruining small proprietors and creating an army of unemployed.",
				"Capitalism has triumphed all over the world, but this triumph is only the prelude to the triumph of labour over capital.",
				"Order No.227: Not a step back!",
			};

			Main.instance.Window.Title = moniker + ": " + Utils.SelectRandom(Main.rand, possibleAppendages);
		}

		private void Main_DrawMenu1(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
		{
			orig.Invoke(self, gameTime);
			Main.dayTime = true;
			Main.time = 27000;
			Main.LogoA = 255;
			Main.LogoB = 0;
			UpdateOurLogo();
			UpdateOurIcon();
		}
	}
}