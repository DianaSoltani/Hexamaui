﻿using HexaMaui;
using Microsoft.Maui;
using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexamauiAppSample
{
    public class HexagonLinedrawSample : IDrawable
    {
        void IDrawable.Draw(ICanvas canvas, RectF dirtyRect)
        {
            throw new NotImplementedException();
        }
    }

    public class HexagonBorder : Border
    {
        /*
        IView content;
        Thickness padding;
        Brush stroke;
        double strokeThickness;
        DoubleCollection strokeDashArray;
        double strokeDashOffset;
        PenLineCap strokeLineCap;
        PenLineJoin strokeLineJoin;
        double strokeMiterLimit;*/


        public HexagonBorder()
        {
            DefaultBuilder();
        }

        /// <summary>
        /// Copied from documentation - example of setters.
        /// </summary>
        public void DefaultBuilder()
        {
            Orientation orientation = Orientation.PointyLayout;
            HexagonLayout layout = new HexagonLayout(orientation, new Point(50,50), new Point(0,0));
            Hex hex = new Hex(0,0);
            List<Point> points = Hexagon.HexagonCorners(layout, hex);
            PointCollection pointCollection = new PointCollection(points.ToArray());
            Border border = new Border
            {
                Stroke = Color.FromArgb("#C49B33"),
                Background = Color.FromArgb("#2B0B98"),
                StrokeThickness =2,
                Padding = new Thickness(50, 50),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                StrokeShape = new Polygon
                {
                    Points = pointCollection
                },
                Content = new Label
                {
                    Text = "12",
                    TextColor = Colors.Gray,
                    FontSize = 18,
                    FontAttributes = FontAttributes.Bold,
                }
            };
            this.Stroke = border.Stroke;
            this.Background = border.Background;
            this.StrokeThickness = border.StrokeThickness;
            this.Padding = border.Padding;
            this.HorizontalOptions = border.HorizontalOptions;
            this.VerticalOptions = border.VerticalOptions;
            this.StrokeShape = border.StrokeShape;
            this.Content = border.Content;
            this.Content.HorizontalOptions = border.HorizontalOptions;
            this.Content.VerticalOptions = border.VerticalOptions;
            
        }
    }
}
