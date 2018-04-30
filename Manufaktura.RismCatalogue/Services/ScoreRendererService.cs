﻿using Manufaktura.Controls.Model;
using Manufaktura.Controls.Rendering.Implementations;
using System;

namespace Manufaktura.RismCatalogue.Services
{
    public class ScoreRendererService
    {
        private readonly SettingsService settingsService;

        public ScoreRendererService(SettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        private static int canvasIdCount;

        public string RenderScore(Score score)
        {
            var settings = settingsService.RendererSettings;

            IScore2HtmlBuilder builder;
            if (settings.RenderSurface == HtmlScoreRendererSettings.HtmlRenderSurface.Canvas)
                builder = new Score2HtmlCanvasBuilder(score, string.Format("scoreCanvas{0}", canvasIdCount), settings);
            else if (settings.RenderSurface == HtmlScoreRendererSettings.HtmlRenderSurface.Svg)
                builder = new Score2HtmlSvgBuilder(score, string.Format("scoreCanvas{0}", canvasIdCount), settings);
            else throw new NotImplementedException("Unsupported rendering engine.");

            string html = builder.Build();

            canvasIdCount++;
            return html;
        }
    }
}