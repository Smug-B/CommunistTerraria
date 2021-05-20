using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace CommunistTerraria
{
	public partial class CommunistTerraria : Mod
	{
		public static string SelectedStars { get; internal set; } = "Hammer And Sickle";

		private static Texture2D InternalStarTexture;

		private static string InternalStarName;

		public static void UpdateOurStars()
		{
			if (InternalStarName == SelectedStars)
			{
				return;
			}

			string internalName = SelectedStars.Replace(" ", string.Empty).Replace("'", string.Empty);
			Texture2D baseTexture = ModContent.GetTexture("CommunistTerraria/Textures/Star/" + internalName);
			InternalStarTexture = ClonedTexture(baseTexture);
			InternalStarName = SelectedStars;
			return;
		}

		public static string SelectedMoon { get; internal set; } = "Comrade Marx";

		private static Texture2D InternalMoonTexture;

		private static string InternalMoonName;

		public static void UpdateOurMoons()
		{
			if (InternalMoonName == SelectedMoon)
			{
				return;
			}

			string internalName = SelectedMoon.Replace(" ", string.Empty).Replace("'", string.Empty);
			Texture2D baseTexture = ModContent.GetTexture("CommunistTerraria/Textures/Moon/" + internalName);
			InternalMoonTexture = ClonedTexture(baseTexture);
			InternalMoonName = SelectedMoon;
			return;
		}

		private void Main_DoDraw(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);

			OurStarChanges(cursor);

			OurMoonChanges(cursor, "pumpkinMoonTexture");

			OurMoonChanges(cursor, "snowMoonTexture");

			OurMoonChanges(cursor);
		}

		private void OurStarChanges(ILCursor cursor)
		{
			FieldInfo ourTexture = typeof(CommunistTerraria).GetField("InternalStarTexture", BindingFlags.NonPublic | BindingFlags.Static);

			void SubstituteOurTexture()
			{
				cursor.Emit(OpCodes.Pop);
				cursor.Emit(OpCodes.Ldsfld, ourTexture);
			}

			if (!cursor.TryGotoNext(i => i.MatchLdloc(87)))
			{
				Mod.Logger.Info("First Do Draw IL edit failed.");
				return;
			}
			SubstituteOurTexture();

			if (!cursor.TryGotoNext(i => i.MatchCallvirt<Texture2D>("get_Width")))
			{
				Mod.Logger.Info("Second Do Draw IL edit failed.");
				return;
			}
			SubstituteOurTexture();

			if (!cursor.TryGotoNext(i => i.MatchLdcR4(0.5f)))
			{
				Mod.Logger.Info("Third Do Draw IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_R4, 1f);

			if (!cursor.TryGotoNext(i => i.MatchCallvirt<Texture2D>("get_Height")))
			{
				Mod.Logger.Info("Fourth Do Draw IL edit failed.");
				return;
			}
			SubstituteOurTexture();

			if (!cursor.TryGotoNext(i => i.MatchLdcR4(0.5f)))
			{
				Mod.Logger.Info("Fifth Do Draw IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_R4, 1f);

			if (!cursor.TryGotoNext(i => i.MatchCallvirt<Texture2D>("get_Width")))
			{
				Mod.Logger.Info("Sixth Do Draw IL edit failed.");
				return;
			}
			SubstituteOurTexture();

			if (!cursor.TryGotoNext(i => i.MatchCallvirt<Texture2D>("get_Height")))
			{
				Mod.Logger.Info("Seventh Do Draw IL edit failed.");
				return;
			}
			SubstituteOurTexture();

			if (!cursor.TryGotoNext(i => i.MatchCallvirt<Texture2D>("get_Width")))
			{
				Mod.Logger.Info("8th Do Draw IL edit failed.");
				return;
			}
			SubstituteOurTexture();

			if (!cursor.TryGotoNext(i => i.MatchLdcR4(0.5f)))
			{
				Mod.Logger.Info("9th Do Draw IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_R4, 1f);

			if (!cursor.TryGotoNext(i => i.MatchCallvirt<Texture2D>("get_Height")))
			{
				Mod.Logger.Info("10th Do Draw IL edit failed.");
				return;
			}
			SubstituteOurTexture();

			if (!cursor.TryGotoNext(i => i.MatchLdcR4(0.5f)))
			{
				Mod.Logger.Info("11th Do Draw IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_R4, 1f);

			if (!cursor.TryGotoNext(i => i.MatchLdcI4(0)))
			{
				Mod.Logger.Info("12th Do Draw IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Ldc_R4, 0.5f);
			cursor.Emit(OpCodes.Add);
		}

		private void OurMoonChanges(ILCursor cursor, string vanillaMoonTexture)
		{
			FieldInfo ourMoon = typeof(CommunistTerraria).GetField("InternalMoonTexture", BindingFlags.NonPublic | BindingFlags.Static);

			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdsfld<Main>(vanillaMoonTexture)))
			{
				Mod.Logger.Info("Event moon, 1st IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldsfld, ourMoon);

			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchMul()))
			{
				Mod.Logger.Info("Event moon, 2nd IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_I4, 0);

			for (int count = 0; count < 2; count++)
			{
				if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdsfld<Main>(vanillaMoonTexture)))
				{
					Mod.Logger.Info("Event moon, 3rd IL edit failed.");
					return;
				}
				cursor.Emit(OpCodes.Pop);
				cursor.Emit(OpCodes.Ldsfld, ourMoon);
			}

			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchCallvirt<Texture2D>("get_Width")))
			{
				Mod.Logger.Info("Event moon, 4th IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.EmitDelegate<Func<int>>(() => InternalMoonTexture.Height);

			for (int count = 0; count < 2; count++)
			{
				if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdsfld<Main>(vanillaMoonTexture)))
				{
					Mod.Logger.Info("Event moon, 5th IL edit failed.");
					return;
				}
				cursor.Emit(OpCodes.Pop);
				cursor.Emit(OpCodes.Ldsfld, ourMoon);
			}
		}

		private void OurMoonChanges(ILCursor cursor)
		{
			FieldInfo ourMoon = typeof(CommunistTerraria).GetField("InternalMoonTexture", BindingFlags.NonPublic | BindingFlags.Static);

			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdelemRef()))
			{
				Mod.Logger.Info("Moon, 1st IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldsfld, ourMoon);

			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchMul()))
			{
				Mod.Logger.Info("Moon, 2nd IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_I4, 0);

			for (int count = 0; count < 2; count++)
			{
				if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdelemRef()))
				{
					Mod.Logger.Info("Moon, 3rd IL edit failed.");
					return;
				}
				cursor.Emit(OpCodes.Pop);
				cursor.Emit(OpCodes.Ldsfld, ourMoon);
			}

			if (!cursor.TryGotoNext(MoveType.After, i => i.MatchCallvirt<Texture2D>("get_Width")))
			{
				Mod.Logger.Info("Moon, 4th IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.EmitDelegate<Func<int>>(() => InternalMoonTexture.Height);

			for (int count = 0; count < 2; count++)
			{
				if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdelemRef()))
				{
					Mod.Logger.Info("Moon, 5th IL edit failed.");
					return;
				}
				cursor.Emit(OpCodes.Pop);
				cursor.Emit(OpCodes.Ldsfld, ourMoon);
			}
		}
	}
}