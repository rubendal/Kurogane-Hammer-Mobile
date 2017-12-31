using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kurogane_Hammer.ViewAdapters
{
    public class GridTable
    {
        private TextFormat HeaderFormat;
        private TextFormat ContentFormat;
        private Row Header;
        private List<Row> Rows = new List<Row>();
        private double padding = 3;
        private Color evenRowColor = Color.FromHex("#D9D9D9");

        private int Columns = 0;

        private Grid grid;

        public GridTable(TextFormat headerFormat, TextFormat contentFormat)
        {
            HeaderFormat = headerFormat;
            ContentFormat = contentFormat;

            grid = new Grid();
        }

        public void AddHeader(IEnumerable<string> rowContent)
        {
            Header = new Row(rowContent);
            Columns = Header.Count;
        }

        public void ResetRows()
        {
            Rows = new List<Row>();
        }

        public void AddRow(IEnumerable<string> rowContent)
        {
            if (rowContent.Count() == Columns)
                Rows.Add(new Row(rowContent));
        }

        public async Task<Grid> Generate()
        {
            return await Task.Run(()=> {
                grid = new Grid();

                grid.ColumnSpacing = 0;

                for (int i = 0; i < Rows.Count; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
                }
                for (int i = 0; i < Columns; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
                }

                List<Label> labels = new List<Label>();

                int row = 0;
                int column = 0;

                //Header
                labels = Header.GetLabels(HeaderFormat, Columns);
                foreach (Label label in labels)
                {
                    StackLayout s = new StackLayout();
                    s.BackgroundColor = HeaderFormat.Background;
                    s.Padding = new Thickness(padding);
                    s.Children.Add(label);
                    grid.Children.Add(s, column, row);
                    column++;
                }
                row = 1;
                column = 0;

                //Content
                foreach (Row r in Rows)
                {
                    labels = r.GetLabels(ContentFormat, Columns);
                    column = 0;
                    foreach (Label label in labels)
                    {
                        StackLayout s = new StackLayout();
                        if (row % 2 == 0)
                        {
                            s.BackgroundColor = evenRowColor;
                            label.BackgroundColor = evenRowColor;
                        }
                        else
                            s.BackgroundColor = ContentFormat.Background;
                        s.Padding = new Thickness(padding);
                        s.Children.Add(label);
                        grid.Children.Add(s, column, row);
                        column++;
                    }
                    row++;
                }

                return grid;

            });
        }
    }

    public class Row
    {
        List<string> Content { get; set; } = new List<string>();
        public int Count
        {
            get
            {
                return Content.Count;
            }
        }

        public Row()
        {
            Content = new List<string>();
        }

        public Row(IEnumerable<string> rowContent)
        {
            Content = new List<string>();
            foreach (string s in rowContent)
            {
                Content.Add(s);
            }
        }

        public List<Label> GetLabels(TextFormat format, int columnCount)
        {
            List<Label> Labels = new List<Label>();
            for (int i = 0; i < columnCount; i++)
            {
                Labels.Add(new Label()
                {
                    Text = i < Content.Count ? Content[i] : "",
                    FontFamily = format.FontFamily,
                    FontSize = format.FontSize,
                    FontAttributes = format.FontAttributes,
                    TextColor = format.TextColor,
                    BackgroundColor = format.Background,
                    HorizontalTextAlignment = format.HorizontalTextAlignment,
                    VerticalTextAlignment = format.VerticalTextAlignment,
                    TranslationX = format.XTranslation,
                    TranslationY = format.YTranslation,
                });
            }

            return Labels;
        }
    }
}
