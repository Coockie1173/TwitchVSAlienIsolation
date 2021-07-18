using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameOverlay.Drawing;
using GameOverlay.Windows;

namespace AIvsTwitch
{
    public class GameOverlayAI
    {
        private readonly GraphicsWindow _window;

        private readonly Dictionary<string, SolidBrush> _brushes;
        private readonly Dictionary<string, Font> _fonts;        

        public GameOverlayAI()
        {
            _fonts = new Dictionary<string, Font>();
            _brushes = new Dictionary<string, SolidBrush>();

            var gfx = new Graphics()
            {
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true
            };

            _window = new GraphicsWindow(0, 0, int.Parse(ConfigHandler.ConfigData["Width"]), int.Parse(ConfigHandler.ConfigData["Height"]), gfx)
            {
                FPS = 60,
                IsTopmost = true,
                IsVisible = true,
            };

            _window.DrawGraphics += _window_DrawGFX;
            _window.SetupGraphics += _window_SetupGFX;
        }

        private void _window_DrawGFX(object sender, DrawGraphicsEventArgs e)
        {
            var gfx = e.Graphics;

            //var padding = 16;
            gfx.ClearScene(_brushes["background"]);

            var infotext = new StringBuilder().Append(ThreadSharedData.DrawData).ToString();

            if (ThreadSharedData.CurrentlyShow)
            {
                gfx.DrawTextWithBackground(_fonts["consolas"], _brushes["green"], _brushes["slightlyvisible"], 58, 20, infotext);
            }            
        }
        private void _window_SetupGFX(object sender, SetupGraphicsEventArgs e)
        {
            var gfx = e.Graphics;

            if (e.RecreateResources)
            {
                foreach (var pair in _brushes) pair.Value.Dispose();
            }


            _fonts["arial"] = gfx.CreateFont("Arial", int.Parse(ConfigHandler.ConfigData["FontSize"]));
            _fonts["consolas"] = gfx.CreateFont("Consolas", int.Parse(ConfigHandler.ConfigData["FontSize"]));

            _brushes["black"] = gfx.CreateSolidBrush(0, 0, 0);
            _brushes["white"] = gfx.CreateSolidBrush(255, 255, 255);
            _brushes["red"] = gfx.CreateSolidBrush(255, 0, 0);
            _brushes["green"] = gfx.CreateSolidBrush(0, 255, 0);
            _brushes["blue"] = gfx.CreateSolidBrush(0, 0, 255);
            _brushes["background"] = gfx.CreateSolidBrush(0,0,0,0);
            _brushes["slightlyvisible"] = gfx.CreateSolidBrush(0,0,0,0.5f);
            _brushes["grid"] = gfx.CreateSolidBrush(255, 255, 255, 0.2f);
            _brushes["random"] = gfx.CreateSolidBrush(0, 0, 0);

            _window.Move(int.Parse(ConfigHandler.ConfigData["XShift"]), int.Parse(ConfigHandler.ConfigData["YShift"]));
        }


        public void Run()
        {
            _window.Create();
            _window.Join();
        }
    }
}
