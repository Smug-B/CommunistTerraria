using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using System;
using Terraria.ModLoader.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using System.Collections;

namespace CommunistTerraria
{
	public partial class CommunistTerraria : Mod
	{
		public static Type InterfaceType;

		public static Type UIModsType;

		public static Type UIModItemType;

		public static UIState Interface_MenuUI;

		public static PropertyInfo ModNameProperty;

		public static FieldInfo ModIconField;

		public static void SetupStaticIconFields()
		{
			// Sets up the Type variables needed for this reflection
			string classDirectory = "Terraria.ModLoader.UI";
			string assemblyName = typeof(UICreateMod).Assembly.FullName;
			InterfaceType = Type.GetType(classDirectory + ".Interface, " + assemblyName);
			UIModsType = Type.GetType(classDirectory + ".UIMods, " + assemblyName);
			UIModItemType = Type.GetType(classDirectory + ".UIModItem, " + assemblyName);

			FieldInfo modsMenu = InterfaceType.GetField("modsMenu", BindingFlags.NonPublic | BindingFlags.Static);
			object uiModsInstance = modsMenu.GetValue(null);
			Interface_MenuUI = (UIState)uiModsInstance;

			// Gets the ModName property, which will be used to see which variable in the list 'items' is our mod
			ModNameProperty = UIModItemType.GetProperty("ModName", BindingFlags.Public | BindingFlags.Instance);

			// Gets the modIcon field which is a UIImage that will draw our icon texture.
			ModIconField = UIModItemType.GetField("_modIcon", BindingFlags.NonPublic | BindingFlags.Instance);
		}

		public static string SelectedIcon { get; internal set; } = "Comrades Stalin And Lenin";

		private static (Texture2D texture, string name) InternalIcon;

		public static UIImage OurIcon;

		public static void UpdateOurIcon()
		{
			if (OurIcon is null || InternalIcon.name == SelectedIcon || !SafeForAssets)
			{
				return;
			}

			string internalName = SelectedIcon.Replace(" ", string.Empty).Replace("'", string.Empty);
			Texture2D baseTexture = ModContent.GetTexture("CommunistTerraria/Textures/Icon/" + internalName);
			InternalIcon.texture = ClonedTexture(baseTexture);
			InternalIcon.name = SelectedIcon;
			OurIcon.SetImage(InternalIcon.texture);
			return;
		}

		public static void UpdateOurIconReflection()
		{
			FieldInfo modsMenu_FieldInfo = InterfaceType.GetField("modsMenu", BindingFlags.NonPublic | BindingFlags.Static);
			FieldInfo items_FieldInfo = UIModsType.GetField("items", BindingFlags.NonPublic | BindingFlags.Instance);
			object modsMenu = modsMenu_FieldInfo.GetValue(null);
			IEnumerable items = (IEnumerable)items_FieldInfo.GetValue(modsMenu);
			foreach (var item in items)
			{
				string modNameString = (string)ModNameProperty.GetValue(item);
				if (!modNameString.Equals("CommunistTerraria"))
				{
					OurIcon = (UIImage)ModIconField.GetValue(item);
					break;
				}
			}
		}

		private void UIElement_Update(On.Terraria.UI.UIElement.orig_Update orig, UIElement self, GameTime gameTime)
		{
			orig.Invoke(self, gameTime);

			if (Main.menuMode != 888 || !Main.MenuUI.CurrentState.Equals(Interface_MenuUI) || InternalIcon.texture is null)
			{
				return;
			}

			if (OurIcon is null)
			{
				UpdateOurIconReflection();
			}

			OurIcon?.SetImage(InternalIcon.texture);
		}

		private void UIElement_Append(On.Terraria.UI.UIElement.orig_Append orig, UIElement self, UIElement element)
		{
			orig.Invoke(self, element);

			try
			{
				if (Main.menuMode != 888 || !Main.MenuUI.CurrentState.Equals(Interface_MenuUI) || !self.GetType().IsAssignableFrom(UIModItemType) || !element.GetType().IsAssignableFrom(typeof(UIImage)))
				{
					return;
				}

				string modNameString = (string)ModNameProperty.GetValue(self);
				if (!modNameString.Equals("CommunistTerraria"))
				{
					return;
				}

				OurIcon = (UIImage)ModIconField.GetValue(self);

				UpdateOurIcon();
			}
			catch
			{
				Mod.Logger.Error("Generic error occured when reflecting icon"); // This seems to fix start up issues.
			}
		}
	}
}