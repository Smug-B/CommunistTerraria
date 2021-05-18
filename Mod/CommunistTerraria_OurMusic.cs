using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria.ModLoader;

namespace CommunistTerraria
{
	public partial class CommunistTerraria : Mod
	{
		public static string SelectedMusic { get; internal set; } = "State Anthem Of The USSR";

		public static int OurMusic
		{
			get
			{
				string internalName = SelectedMusic.Replace(" ", string.Empty).Replace("'", string.Empty);
				return Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/" + internalName);
			}
		}

		private static void Main_UpdateAudio(ILContext il)
		{
			ILCursor cursor = new ILCursor(il);

			if (!cursor.TryGotoNext(i => i.MatchLdcI4(6)))
			{
				Mod.Logger.Info("IL Editing Failed.");
				return;
			}

			if (!cursor.TryGotoNext(i => i.MatchLdcI4(6)))
			{
				Mod.Logger.Info("ILEditing Failed at second check.");
				return;
			}

			cursor.Index++;
			cursor.Emit(OpCodes.Pop);
			cursor.EmitDelegate<Func<int>>(() => SafeForAssets ? OurMusic : 6);
		}
	}
}