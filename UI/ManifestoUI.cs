using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace CommunistTerraria.UI
{
    public class ManifestoUI : UIState
    {
        private readonly string _manifesto;
        private UIPanel _mainPanel;

        public ManifestoUI(string manifesto) => _manifesto = manifesto;

        public override void OnInitialize()
        {
            _mainPanel = new UIPanel
            {
                HAlign = .75f,
                VAlign = .5f,
                Width = new StyleDimension(550, 0),
                Height = new StyleDimension(650, 0),
            };
            Append(_mainPanel);

            IEnumerable<IEnumerable<string>> pages = SplitPages(_manifesto);

            UIText simpleText = new UIText(string.Join("\n", pages.First()))
            {
                HAlign = 0f,
                VAlign = 0f,
                Height = new StyleDimension(_mainPanel.Height.Pixels - 100, 0),
            };
            _mainPanel.Append(simpleText);
        }

        private static IEnumerable<IEnumerable<string>> SplitPages(string manifesto, int lineCount = 20)
        {
            List<string> lines = manifesto.Split('\n').ToList();
            for (int i = 0; i < lines.Count; i += lineCount)
            {
                yield return lines.GetRange(i, lineCount);
            }
        }
    }
}