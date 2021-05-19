using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace CommunistTerraria.UI
{
    public class ManifestoUI : UIState
    {
        private UIPanel _mainPanel;
        
        public override void OnInitialize()
        {
            _mainPanel = new UIPanel
            {
                HAlign = .75f,
                VAlign = .5f,
                Width = new StyleDimension(500, 0),
                Height = new StyleDimension(600, 0),
            };
            Append(_mainPanel);

            string lorem = "Glutens sunt devirginatos de fatalis ignigena. Mensas ire! Sunt sagaes convertam peritus, salvus cliniases.";
            IEnumerable<string> split = SplitText(lorem);
            
            UIText simpleText = new UIText(string.Join("\n", split))
            {
                HAlign = 0f,
                VAlign = 0f,
                // Width = new StyleDimension(0, .99f),
                Height = new StyleDimension(_mainPanel.Height.Pixels - 100, 0),
                // Left = new StyleDimension(0, 0)
            };
            _mainPanel.Append(simpleText);
        }

        private IEnumerable<string> SplitText(string text, int limit = 60)
        {
            string part = string.Empty;
            foreach (string word in text.Split(' '))
            {
                if (part.Length + word.Length < limit)
                    part += string.IsNullOrEmpty(part) ? word : " " + word;
                else
                {
                    yield return part;
                    part = word;
                }
            }

            yield return part;
        }
    }
}