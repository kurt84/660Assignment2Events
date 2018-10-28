using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EventPlanner
{
    static class EventPreviewMaker
    {
        public static Grid CreatePreview(Event e)
        {
            //create grid to hold the preview card
            var grid = new Grid { Width = 400, Height = 180, Margin = new Thickness(0,1,0,1)};
            grid.Tag = e;

            grid.RowDefinitions.Add(new RowDefinition{ Height = new GridLength(60) });
            grid.RowDefinitions.Add(new RowDefinition{ Height = new GridLength(60) });
            grid.RowDefinitions.Add(new RowDefinition{ Height = new GridLength(60) });

            grid.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(380) });
            grid.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(20) });

            //create contents of the preview card
            var border = new Border { BorderThickness = new Thickness(3), BorderBrush = new SolidColorBrush(Colors.Black), Width = 375, Height = 180 };
            var titleLabel = new Label { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center, FontSize = 20, Margin = new Thickness(10, 0, 0, 0), Content = e.Title };
            var dateLabel = new Label { HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center, FontSize = 20, Margin = new Thickness(0, 0, 10, 0), Content = e.Date.ToShortDateString() };
            var noteLabel = new Label { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center, FontSize = 20, Margin = new Thickness(10, 0, 0, 0), Content = e.Note };
            var categoryLabel = new Label { HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center, FontSize = 20, Margin = new Thickness(0, 0, 10, 0), Content = e.Category.Name };

            border.Background = GetColoredBackground(e.CategoryId);

            //set rows and columns for children
            Grid.SetRow(border, 0);
            Grid.SetColumn(border, 0);
            Grid.SetRowSpan(border, 3);

            Grid.SetRow(titleLabel, 0);
            Grid.SetRow(dateLabel, 0);
            Grid.SetRow(noteLabel, 1);
            Grid.SetRow(categoryLabel, 2);

            //put the contents into the grid
            grid.Children.Add(border);
            grid.Children.Add(titleLabel);
            grid.Children.Add(dateLabel);
            grid.Children.Add(noteLabel);
            grid.Children.Add(categoryLabel);

            return grid;
        }

        public static SolidColorBrush GetColoredBackground(int id)
        {
            //set color based on category
            switch (id)
            {
                case 1:
                    return new SolidColorBrush(Colors.Beige);
                case 2:
                    return new SolidColorBrush(Colors.Aquamarine);
                case 3:
                    return new SolidColorBrush(Colors.Lavender);
                case 4:
                    return new SolidColorBrush(Colors.Tomato);
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }
    }
}
