using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;

namespace CommunistTerraria
{
	public partial class CommunistTerraria : Mod
	{
		public static string SelectedLogo { get; internal set; } = "State Emblem Of The Soviet Union";

		private static Texture2D InternalLogoTexture;

		private static string InternalLogoName;

		public static void UpdateOurLogo()
		{
			if (InternalLogoName == SelectedLogo)
			{
				return;
			}

			string internalName = SelectedLogo.Replace(" ", string.Empty).Replace("'", string.Empty);
			Texture2D baseTexture = ModContent.GetTexture("CommunistTerraria/Textures/Logo/" + internalName);
			InternalLogoTexture = ClonedTexture(baseTexture);
			InternalLogoName = SelectedLogo;
			return;
		}

		private static void Main_DrawMenu(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);
			FieldInfo ourLogo = Mod.GetType().GetField("InternalLogoTexture", BindingFlags.NonPublic | BindingFlags.Static);

			for (int count = 0; count < 9; count++)
			{
				if (!cursor.TryGotoNext(i => i.MatchLdsfld(typeof(Main).GetField("logoTexture", BindingFlags.Public | BindingFlags.Static))))
				{
					Mod.Logger.Info("Drawing IL edit failed.");
					return;
				}
				cursor.Index++;
				cursor.Emit(OpCodes.Pop);
				cursor.Emit(OpCodes.Ldsfld, ourLogo);
			}

			if (!cursor.TryGotoPrev(i => i.MatchLdsfld(typeof(Main).GetField("logo2Texture", BindingFlags.Public | BindingFlags.Static))))
			{
				Mod.Logger.Info("Drawing second IL edit failed.");
				return;
			}
			cursor.Index++;
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldsfld, ourLogo);

			if (!cursor.TryGotoPrev(i => i.MatchLdcR4(110f)))
			{
				Mod.Logger.Info("Drawing third IL edit failed.");
				return;
			}
			cursor.Index++;
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_R4, 175f);

			if (!cursor.TryGotoNext(i => i.MatchLdcR4(110f)))
			{
				Mod.Logger.Info("Drawing fourth IL edit failed.");
				return;
			}
			cursor.Index++;
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_R4, 175f);

			if (!cursor.TryGotoPrev(i => i.MatchStloc(1)))
			{
				Mod.Logger.Info("Drawing fifth IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.Emit(OpCodes.Ldc_I4, 255);

			/*if (!cursor.TryGotoNext(i => i.MatchStloc(179)))
			{
				Mod.Logger.Info("Drawing sixth IL edit failed.");
				return;
			}
			cursor.Emit(OpCodes.Pop);
			cursor.EmitDelegate<Func<string>>(() => OurRight);*/
		}
	}
}