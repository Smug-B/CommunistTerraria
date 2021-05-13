using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using ReLogic.Graphics;
using System;

namespace CommunistTerraria
{
	public class CommunistTerraria : Mod
	{
		private static Texture2D OurLogo;

		private static Texture2D OurBackground;

		public override void Close()
		{
			int anthemSlot = GetSoundSlot(SoundType.Music, "Sounds/Music/StateAnthemOfTheUSSR");
			if (Main.music.IndexInRange(anthemSlot) && Main.music[anthemSlot]?.IsPlaying == true)
			{
				Main.music[anthemSlot].Stop(AudioStopOptions.Immediate);
			}
			base.Close();
		}

		public override void PostSetupContent()
		{
			DecideTitle();

			if (OurLogo is null) // tMod disposes all textures on unload, so we need a clone to ensure a crash does not happen
			{
				Texture2D baseTexture = ModContent.GetTexture("CommunistTerraria/Textures/StateEmblemOfTheSovietUnion");
				Color[] logoData = new Color[baseTexture.Width * baseTexture.Height];
				baseTexture.GetData(logoData);
				OurLogo = new Texture2D(Main.instance.GraphicsDevice, baseTexture.Width, baseTexture.Height);
				OurLogo.SetData(logoData);
			}

			if (OurBackground is null)
			{
				Texture2D baseTexture = ModContent.GetTexture("CommunistTerraria/Textures/FlagOfTheSovietUnion");
				Color[] backgroundData = new Color[baseTexture.Width * baseTexture.Height];
				baseTexture.GetData(backgroundData);
				OurBackground = new Texture2D(Main.instance.GraphicsDevice, baseTexture.Width, baseTexture.Height);
				OurBackground.SetData(backgroundData);
			}

			IL.Terraria.Main.UpdateAudio += Main_UpdateAudio;
			IL.Terraria.Main.DrawMenu += Main_DrawMenu;
			On.Terraria.Main.DrawSurfaceBG += Main_DrawSurfaceBG;
			On.Terraria.Main.DrawMenu += Main_DrawMenu1;
		}

		private static void DecideTitle()
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

		private void Main_UpdateAudio(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);

			if (!cursor.TryGotoNext(i => i.MatchLdcI4(6)))
			{
				Logger.Info("IL Editing Failed.");
				return;
			}

			if (!cursor.TryGotoNext(i => i.MatchLdcI4(6)))
			{
				Logger.Info("ILEditing Failed at second check.");
				return;
			}

			cursor.Index++;

			cursor.Emit(OpCodes.Pop);

			cursor.Emit(OpCodes.Ldc_I4, GetSoundSlot(SoundType.Music, "Sounds/Music/StateAnthemOfTheUSSR"));
		}

		private void Main_DrawMenu(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);
			FieldInfo ourLogo = GetType().GetField("OurLogo", BindingFlags.NonPublic | BindingFlags.Static);

			for (int count = 0; count < 9; count++)
			{
				if (!cursor.TryGotoNext(i => i.MatchLdsfld(typeof(Main).GetField("logoTexture", BindingFlags.Public | BindingFlags.Static))))
				{
					Logger.Info("Drawing IL edit failed.");
					return;
				}
				cursor.Index++;
				cursor.Emit(OpCodes.Pop);
				cursor.Emit(OpCodes.Ldsfld, ourLogo);
			}

			if (!cursor.TryGotoPrev(i => i.MatchLdsfld(typeof(Main).GetField("logo2Texture", BindingFlags.Public | BindingFlags.Static))))
			{
				Logger.Info("Drawing second IL edit failed.");
				return;
			}
			cursor.Index++;
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldsfld, ourLogo);

			if (!cursor.TryGotoPrev(i => i.MatchLdcR4(110f)))
			{
				Logger.Info("Drawing third IL edit failed.");
				return;
			}
			cursor.Index++;
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_R4, 175f);

			if (!cursor.TryGotoNext(i => i.MatchLdcR4(110f)))
			{
				Logger.Info("Drawing fourth IL edit failed.");
				return;
			}
			cursor.Index++;
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_R4, 175f);
			return;
		}

		private void Main_DrawMenu1(On.Terraria.Main.orig_DrawMenu orig, Main self, GameTime gameTime)
		{
			orig.Invoke(self, gameTime);
			Main.dayTime = true;
			Main.time = 27000;
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