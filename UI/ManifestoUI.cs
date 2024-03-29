﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;

namespace CommunistTerraria.UI
{
    public class ManifestoUI : UIState
    {
        private readonly string _manifesto;
        private UIImage _mainPanel;
        private UIText _mainText;
        private UIText _currentPageText;
        private int _currentPage;
        private List<IEnumerable<string>> _pages;

        public ManifestoUI(string manifesto) => _manifesto = manifesto;

        public override void OnInitialize()
        {
            Texture2D background = ModContent.GetInstance<CommunistTerraria>().GetTexture("Textures/UI/ManifestoBackground");
            _mainPanel = new UIImage(background)
            {
                HAlign = .75f,
                VAlign = .5f,
                Width = new StyleDimension(550, 0),
                Height = new StyleDimension(650, 0),
            };
            Append(_mainPanel);

            _pages = SplitPages(_manifesto).ToList();

            _mainText = new UIText(string.Join("\n", _pages.First()))
            {
                Top = new StyleDimension(16, 0),
                Left = new StyleDimension(16, 0),
                VAlign = 0f,
                Height = new StyleDimension(_mainPanel.Height.Pixels - 100, 0),
            };
            _mainPanel.Append(_mainText);

            Texture2D panel = ModContent.GetInstance<CommunistTerraria>().GetTexture("Textures/UI/ManifestoPanel");
            UIImage pagePanel = new UIImage(panel)
            {
                Top = new StyleDimension(_mainPanel.Height.Pixels - 75, 0),
                HAlign = .5f,
                Height = new StyleDimension(50, 0),
                Width = new StyleDimension(200, 0)
            };
            _mainPanel.Append(pagePanel);

            Texture2D back = ModContent.GetInstance<CommunistTerraria>().GetTexture("Textures/UI/ManifestoBack");
            UIImageButton backButton = new UIImageButton(back)
            {
                Left = new StyleDimension(28, 0),
                VAlign = 0.5f,
                Width = new StyleDimension(25, 0f)
            };
            backButton.OnClick += BackClicked;
            pagePanel.Append(backButton);

            Texture2D forward = ModContent.GetInstance<CommunistTerraria>().GetTexture("Textures/UI/ManifestoForward");
            UIImageButton nextButton = new UIImageButton(forward)
            {
                Left = new StyleDimension(150, 0),
                VAlign = 0.5f,
                Width = new StyleDimension(25, 0f)
            };
            nextButton.OnClick += NextClicked;
            pagePanel.Append(nextButton);

            _currentPageText = new UIText("001")
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            };
            pagePanel.Append(_currentPageText);
        }

        private void NextClicked(UIMouseEvent evt, UIElement listeningelement)
        {
            if (++_currentPage >= _pages.Count)
            {
                _currentPage = _pages.Count;
                return;
            }

            UpdateText(_currentPage);
        }

        private void BackClicked(UIMouseEvent evt, UIElement listeningelement)
        {
            if (--_currentPage < 0)
            {
                _currentPage = 0;
                return;
            }

            UpdateText(_currentPage);
        }

        private void UpdateText(int pageNum)
        {
            _currentPageText.SetText((pageNum + 1).ToString("000"));

            string mainText = string.Join("\n", _pages[pageNum]);
            _mainText.SetText(mainText);
        }

        private static IEnumerable<IEnumerable<string>> SplitPages(string manifesto, int lineCount = 20)
        {
            List<string> lines = manifesto.Split('\n').ToList();
            for (int i = 0; i < lines.Count; i += lineCount)
            {
                int leftLines = lines.Count - i;
                yield return lines.GetRange(i, lineCount > leftLines ? leftLines : lineCount);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (_mainPanel.ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}