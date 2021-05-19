using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CommunistTerraria
{
    public partial class CommunistTerraria : Mod
    {
        public static bool CommifyText;
        public static bool CommifyImages;

        public void CommifyOurText()
        {
            // If We do all of this We should be able to reload Our localized text
            GameCulture ogCulture = LanguageManager.Instance.ActiveCulture;
            LanguageManager.Instance.SetLanguage(GameCulture.German);
            LanguageManager.Instance.SetLanguage(GameCulture.Polish);
            LanguageManager.Instance.SetLanguage(GameCulture.Italian);
            LanguageManager.Instance.SetLanguage(GameCulture.French);
            LanguageManager.Instance.SetLanguage(GameCulture.Chinese);
            LanguageManager.Instance.SetLanguage(ogCulture);

            Dictionary<string, LocalizedText> lt = LanguageManager.Instance.GetType().GetField("_localizedTexts", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(LanguageManager.Instance) as Dictionary<string, LocalizedText>;
            ConstructorInfo ctor = typeof(LocalizedText).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];

            for (int i = 0; i < lt.Count; i++)
            {
                string t = lt.Values.ToArray()[i].Value;

                LocalizedText text = (LocalizedText)ctor.Invoke(lt.Values.ToArray()[i], new object[2] {
                    lt.Keys.ToArray()[i],
                    t.Length > 0 ? "Our " + t + " ☭" : t
                });

                lt.Keys.ToArray().SetValue(text, i);
            }
        }

        public void CommifyOurImages()
        {
            Texture2D ourNewTexture = ModContent.GetTexture("CommunistTerraria/Textures/OurHammerAndOurSickle");

            Type[] red = typeof(Main).Assembly.GetTypes();

            List<FieldInfo> fields = new List<FieldInfo>();
            foreach (Type type in from Type t in red where !t.ContainsGenericParameters select t)
                foreach (FieldInfo f in type.GetFields())
                    fields.Add(f);

            FieldInfo[] textures = fields.Where((FieldInfo field) => field.IsStatic && field.FieldType == typeof(Texture2D)).ToArray();
            FieldInfo[] arrays = fields.Where((FieldInfo field) => field.IsStatic && field.FieldType == typeof(Texture2D[])).ToArray();

            foreach (FieldInfo field in textures)
                field.SetValue(null, ourNewTexture);

            List<FieldInfo> texes = new List<FieldInfo>();
            foreach (FieldInfo field in arrays)
            {
                Texture2D[] arr = field.GetValue(null) as Texture2D[];

                for (int i = 0; i < arr.Length; i++)
                    (field.GetValue(null) as Texture2D[]).SetValue(ourNewTexture, i);
            }
        }
    }
}
