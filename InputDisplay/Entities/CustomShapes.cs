using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InputDisplay
{
    static class CustomShapes
    {

        static public GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        static public void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (pen == null)
                throw new ArgumentNullException("pen");

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.DrawPath(pen, path);
            }
        }

        static public void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (brush == null)
                throw new ArgumentNullException("brush");

            using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }

        static public GraphicsPath Plus(Rectangle bounds, int thickness, int radius)
        {
            // center of the plus
            (int x, int y) center = (bounds.X + bounds.Size.Width / 2, bounds.Y + bounds.Size.Height / 2);

            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(new Point(center.x - thickness / 2, bounds.Y), size);
            GraphicsPath path = new GraphicsPath();

            // top left arc
            path.AddArc(arc, 180, 90);

            // top right arc
            arc.X += thickness - diameter;
            path.AddArc(arc, 270, 90);

            // top right inner arc
            arc.X += diameter;
            arc.Y = center.y - thickness / 2 - diameter;
            path.AddArc(arc, 180, -90);

            // right top arc
            arc.X = bounds.Right - diameter;
            arc.Y += diameter;
            path.AddArc(arc, 270, 90);

            // right bottom arc
            arc.Y = center.y + thickness / 2 - diameter;
            path.AddArc(arc, 0, 90);

            // bottom right inner arc
            arc.X = center.x + thickness / 2;
            arc.Y = center.y + thickness / 2;
            path.AddArc(arc, 270, -90);

            // bottom right arc
            arc.X = center.x + thickness / 2 - diameter;
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc
            arc.X = center.x - thickness / 2;
            path.AddArc(arc, 90, 90);

            // bottom left inner arc
            arc.X -= diameter;
            arc.Y = center.y + thickness / 2;
            path.AddArc(arc, 0, -90);

            // left bottom arc
            arc.X = bounds.X;
            arc.Y -= diameter;
            path.AddArc(arc, 90, 90);

            // left top arc
            arc.Y = center.y - thickness / 2;
            path.AddArc(arc, 180, 90);

            // top left inner arc
            arc.X = center.x - thickness / 2 - diameter;
            arc.Y -= diameter;
            path.AddArc(arc, 90, -90);

            path.CloseFigure();
            return path;
        }

        static public void DrawPlus(this Graphics graphics, Pen pen, Rectangle bounds, int thickness, int cornerRadius)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (pen == null)
                throw new ArgumentNullException("pen");

            using (GraphicsPath path = Plus(bounds, thickness, cornerRadius))
            {
                graphics.DrawPath(pen, path);
            }
        }

        static public GraphicsPath PointDirection(Point center, int thickness, int size, int radius, int direction)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            Point leftInner = new Point(center.X - thickness / 2, center.Y - thickness / 2);
            Point rightInner = new Point(center.X + thickness / 2, center.Y - thickness / 2);
            path.AddLine(center, leftInner);
            path.AddLine(leftInner, new Point(leftInner.X, center.Y - size / 2 + radius));

            Rectangle arc = new Rectangle(new Point(center.X - thickness / 2, center.Y - size / 2), new Size(diameter, diameter));
            path.AddArc(arc, 180, 90);

            arc.X += thickness - diameter;
            path.AddArc(arc, 270, 90);

            path.AddLine(new Point(rightInner.X, center.Y - size / 2 + radius), rightInner);
            path.AddLine(rightInner, center);

            path.CloseFigure();

            Matrix rotateMatrix = new Matrix();
            switch (direction)
            {
                case 2:
                    rotateMatrix.RotateAt(180, center);
                    path.Transform(rotateMatrix);
                    break;

                case 3:
                    rotateMatrix.RotateAt(270, center);
                    path.Transform(rotateMatrix);
                    break;

                case 4:
                    rotateMatrix.RotateAt(90, center);
                    path.Transform(rotateMatrix);
                    break;

                default:
                    break;
            }

            return path;
        }

        static public void FillPointDirection(this Graphics graphics, Brush brush, Point center, int thickness, int size, int radius, int direction)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");
            if (brush == null)
                throw new ArgumentNullException("brush");

            using (GraphicsPath path = PointDirection(center, thickness, size, radius, direction))
            {
                graphics.FillPath(brush, path);
            }
        }

        static public GraphicsPath CreateOctagon(Point center, int radius)
        {
            float c = (float)Math.Sqrt(2) / 2;
            List<(float x, float y)> coords = new List<(float x, float y)> { (1, 0), (c, c), (0, 1), (-c, c), (-1, 0), (-c, -c), (0, -1), (c, -c) };
            List<PointF> octagonPointsList = new List<PointF>();
            for (int i = 0; i < coords.Count; ++i)
            {
                octagonPointsList.Add(new PointF(radius * coords[i].x + center.X, radius * coords[i].y + center.Y));
            }
            byte[] types = new byte[8] { 1, 1, 1, 1, 1, 1, 1, 1 };
            return new GraphicsPath(octagonPointsList.ToArray(), types);
        }

        static public void DrawOctagon(this Graphics graphics, Pen pen, Point center, int radius)
        {
            float c = (float)Math.Sqrt(2) / 2;
            List<(float x, float y)> coords = new List<(float x, float y)> { (1, 0), (c, c), (0, 1), (-c, c), (-1, 0), (-c, -c), (0, -1), (c, -c) };
            List<PointF> octagonPointsList = new List<PointF>();
            for (int i = 0; i < coords.Count; ++i)
            {
                octagonPointsList.Add(new PointF(radius * coords[i].x + center.X, radius * coords[i].y + center.Y));
            }
            byte[] types = new byte[8] { 1, 1, 1, 1, 1, 1, 1, 1 };
            graphics.DrawPolygon(pen, octagonPointsList.ToArray());
        }

    }
}
