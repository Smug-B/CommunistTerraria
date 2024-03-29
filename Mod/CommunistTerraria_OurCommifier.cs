﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CommunistTerraria
{
    // Credits to PBone for the following. Glory to Commmunist Terraria.
    public partial class CommunistTerraria : Mod
    {
        public static bool HammerAndSickleSuffix;

        public static bool OurPrefix = false;

        public static bool CommifyImages = false;

        public void CommifyOurText(bool forcedUnload = false)
        {
            GameCulture originalCulture = LanguageManager.Instance.ActiveCulture;
            LanguageManager.Instance.SetLanguage(GameCulture.German);
            LanguageManager.Instance.SetLanguage(GameCulture.Polish);
            LanguageManager.Instance.SetLanguage(GameCulture.Italian);
            LanguageManager.Instance.SetLanguage(GameCulture.French);
            LanguageManager.Instance.SetLanguage(GameCulture.Chinese);
            LanguageManager.Instance.SetLanguage(originalCulture);

            Type languageManager = LanguageManager.Instance.GetType();
            FieldInfo localizedTextInfo = languageManager.GetField("_localizedTexts", BindingFlags.Instance | BindingFlags.NonPublic);
            Dictionary<string, LocalizedText> localizedText = localizedTextInfo.GetValue(LanguageManager.Instance) as Dictionary<string, LocalizedText>;

            ConstructorInfo constructor = typeof(LocalizedText).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];

            LocalizedText[] currentValues = localizedText.Values.ToArray();

            string[] currentKeys = localizedText.Keys.ToArray();

            for (int we = 0; we < localizedText.Count; we++)
            {
                string originalText = currentValues[we].Value;
                string productString = OurPrefix ? "Our " : string.Empty;
                productString += originalText;
                productString += HammerAndSickleSuffix ? " ☭" : string.Empty;

                LocalizedText text = (LocalizedText)constructor.Invoke(currentValues[we], new object[2]
                {
                    currentKeys[we],
                    (originalText.Length > 0 && !forcedUnload) ? productString : originalText
                });

                localizedText.Keys.ToArray().SetValue(text, we);
            }
        }

        public void CommifyOurImages()
        {
            Texture2D ourNewTexture = ModContent.GetTexture("CommunistTerraria/Textures/OurHammerAndOurSickle");

            Type[] red = typeof(Main).Assembly.GetTypes();

            List<FieldInfo> fields = new List<FieldInfo>();
            foreach (Type type in from Type t in red where !t.ContainsGenericParameters select t)
            {
                foreach (FieldInfo fieldInfo in type.GetFields())
                {
                    fields.Add(fieldInfo);
                }
            }

            FieldInfo[] textures = fields.Where((FieldInfo field) => field.IsStatic && field.FieldType == typeof(Texture2D)).ToArray();
            FieldInfo[] arrays = fields.Where((FieldInfo field) => field.IsStatic && field.FieldType == typeof(Texture2D[])).ToArray();

            foreach (FieldInfo texture in textures)
            {
                texture.SetValue(null, ourNewTexture);
            }

            List<FieldInfo> texes = new List<FieldInfo>();
            foreach (FieldInfo field in arrays)
            {
                Texture2D[] arr = field.GetValue(null) as Texture2D[];

                for (int i = 0; i < arr.Length; i++)
                {
                    (field.GetValue(null) as Texture2D[]).SetValue(ourNewTexture, i);
                }
            }
        }
    }
}
