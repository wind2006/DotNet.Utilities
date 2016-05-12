using DevExpress.Skins;
using DevExpress.XtraEditors.Drawing;
using System;
using System.Drawing;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.CustomControl.CustomRangeTrackBar
{
    internal class SkinCustomRangeTrackBarObjectPainter : SkinRangeTrackBarObjectPainter
    {
        public SkinCustomRangeTrackBarObjectPainter(ISkinProvider provider)
            : base(provider)
        {
        }

        public override void DrawPoints(TrackBarObjectInfoArgs e, bool bMirror)
        {
            Point p1 = Point.Empty, p2 = Point.Empty;
            float xPos;
            int tickCount;
            p1.Y = e.ViewInfo.PointsRect.Y;
            for (xPos = 0, tickCount = 0; tickCount < e.ViewInfo.TickCount; xPos += e.ViewInfo.PointsDelta, tickCount++)
            {
                p2.X = p1.X = (int)(e.ViewInfo.PointsRect.X + xPos + 0.01f);
                if (tickCount != 0 && tickCount != e.ViewInfo.TickCount - 1) p2.Y = p1.Y + Math.Max(e.ViewInfo.PointsRect.Height * 3 / 4, 1);
                else p2.Y = p1.Y + e.ViewInfo.PointsRect.Height;
                DrawLine(e, e.ViewInfo.TrackBarHelper.RotateAndMirror(p1, e.ViewInfo.MirrorPoint.Y, bMirror), e.ViewInfo.TrackBarHelper.RotateAndMirror(p2, e.ViewInfo.MirrorPoint.Y, bMirror));
                DrawTickText(e, p1, tickCount);
            }
        }

        private static void DrawTickText(TrackBarObjectInfoArgs e, Point p, int tickCount)
        {
            CustomRangeTrackBarViewInfo vi = e.ViewInfo as CustomRangeTrackBarViewInfo;
            vi.RepositoryItem.OnDrawingTick(new DrawingTickEventArgs(e, tickCount));

            // Do not attempt to display tick value in case if it wasn't specified by user
            if (vi.RepositoryItem.TickDisplayText == null)
                return;

            // Check if there's enough space to draw the tick values assuming that there should be at least 15 pixels available for drawing
            // the text
            int freePixels = e.Bounds.Y + e.Bounds.Height - (e.ViewInfo.PointsRect.Y + e.ViewInfo.PointsRect.Height);
            if (freePixels < 10)
                return;

            Rectangle textRect = new Rectangle();
            textRect.Y = e.ViewInfo.PointsRect.Y + e.ViewInfo.PointsRect.Height + 3;
            textRect.Height = freePixels - 3;

            Font font = new Font(e.Appearance.Font.FontFamily, (Single)Math.Min(freePixels - 3, 7), FontStyle.Regular);

            StringFormat strFormat = e.Appearance.GetStringFormat();
            strFormat.Alignment = StringAlignment.Center;

            textRect.X = (int)(p.X - e.ViewInfo.PointsDelta / 2);
            textRect.Width = (int)(e.ViewInfo.PointsDelta);

            SizeF strSize = e.Graphics.MeasureString(vi.RepositoryItem.TickDisplayText, font);
            double overlap = strSize.Width / textRect.Width;

            if (overlap <= 1)
                e.Paint.DrawString(e.Cache, vi.RepositoryItem.TickDisplayText, font, e.Cache.GetSolidBrush(e.Appearance.ForeColor), textRect, strFormat);
            else
            {
                int range = (int)Math.Round(overlap);
                textRect.Width = (int)(textRect.Width * overlap);

                int tail = (e.ViewInfo.TickCount % 2 == 0) ? 0 : 1;

                if (tickCount % range != tail)
                    e.Paint.DrawString(e.Cache, vi.RepositoryItem.TickDisplayText, font, e.Cache.GetSolidBrush(e.Appearance.ForeColor), textRect, strFormat);
            }
        }
    }
}