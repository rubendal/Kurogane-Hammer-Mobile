using Kurogane_Hammer.Classes;
using Kurogane_Hammer.ViewAdapters;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kurogane_Hammer.Views.CharacterView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterListViewPage : ContentPage
    {
        private Character _Character { get; set; }

        public ObservableCollection<string> Items { get; set; }

        public CharacterListViewPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {

            };

            MyListView.ItemsSource = Items;
        }

        public CharacterListViewPage(Character c)
        {
            InitializeComponent();

            _Character = c;

            Items = new ObservableCollection<string>
            {
                "All data",
                "Attributes",
                "Detailed attributes",
                "All Moves",
                "Ground Moves",
                "Throws",
                "Aerial Moves",
                "Special Moves"
            };

            if (_Character.DisplayName == "Little Mac")
                Items[6] = "Aerial Moves (Please don't actually use these)";

            if (_Character.hasSpecificAttributes)
                Items.Add(_Character.specificAttribute.attribute);

            MyListView.ItemsSource = Items;

            Title = c.getCharacterTitleName();
        }

        public GridTable GetMovementTable()
        {
            GridTable table = new GridTable(new TextFormat() { Background = Color.FromHex(_Character.ColorTheme) }, new TextFormat());
            table.AddHeader(new List<string>()
                    {
                        "Attribute",
                        "Value"
                    });
            List<Movement> list = Runtime.GetMovements(_Character.OwnerId);
            foreach(Movement m in list)
            {
                table.AddRow(new List<string>()
                    {
                        m.name,
                        m.value
                    });
            }

            return table;
        }

        public GridTable GetAttributeTable()
        {
            GridTable table = new GridTable(new TextFormat() { Background = Color.FromHex(_Character.ColorTheme) }, new TextFormat());
            table.AddHeader(new List<string>()
                    {
                        "Attribute",
                        "Value Type",
                        "Value"
                    });
            List<Attribute> list = Runtime.GetAttributes(_Character.OwnerId);
            foreach (Attribute a in list)
            {
                foreach(AttributeValue v in a.attributes)
                {
                    table.AddRow(new List<string>()
                    {
                        a.name,
                        v.name,
                        v.value
                    });
                }
            }

            return table;
        }

        public GridTable GetMoveTable(MoveType moveType)
        {
            GridTable table = new GridTable(new TextFormat() { Background = Color.FromHex(_Character.ColorTheme) }, new TextFormat());
            switch (moveType)
            {
                case MoveType.Evasion:
                    table.AddHeader(new List<string>()
                    {
                        "Miscellaneous",
                        "Intangibility",
                        "FAF"
                    });
                    break;
                case MoveType.Aerial:
                    table.AddHeader(new List<string>()
                    {
                        "Move",
                        "Hitbox Active",
                        "FAF",
                        "Damage",
                        "Angle",
                        "BKB",
                        "KBG",
                        "Landing Lag",
                        "Autocancel"
                    });
                    break;
                case MoveType.Throw:
                    table.AddHeader(new List<string>()
                    {
                        "Move",
                        "Weight Dependent",
                        "Damage",
                        "Angle",
                        "BKB",
                        "KBG"
                    });
                    break;
                default:
                    table.AddHeader(new List<string>()
                    {
                        "Move",
                        "Hitbox Active",
                        "FAF",
                        "Damage",
                        "Angle",
                        "BKB",
                        "KBG"
                    });
                    break;
            }
            

            List<Move> moves = Runtime.GetMoves(_Character.OwnerId, moveType);

            switch (moveType)
            {
                case MoveType.Evasion:
                    foreach (Move move in moves)
                    {
                        table.AddRow(new List<string>
                        {
                            move.name,
                            move.hitboxActive,
                            move.FAF
                        });
                    }
                    break;
                case MoveType.Aerial:
                    foreach (AerialMove move in moves)
                    {
                        table.AddRow(new List<string>
                        {
                            move.name,
                            move.hitboxActive,
                            move.FAF,
                            move.baseDamage,
                            move.angle,
                            move.bkb,
                            move.kbg,
                            move.landingLag,
                            move.autoCancel.Replace("&gt;",">")
                        });
                    }
                    break;
                case MoveType.Throw:
                    foreach (ThrowMove move in moves)
                    {
                        table.AddRow(new List<string>
                        {
                            move.name,
                            move.weightDependent ? "Yes" : "No",
                            move.baseDamage,
                            move.angle,
                            move.bkb,
                            move.kbg
                        });
                    }
                    break;
                default:
                    foreach (Move move in moves)
                    {
                        table.AddRow(new List<string>
                        {
                            move.name,
                            move.hitboxActive,
                            move.FAF,
                            move.baseDamage,
                            move.angle,
                            move.bkb,
                            move.kbg
                        });
                    }
                    break;
            }

            

            return table;
        }

        public GridTable GetGrabsTable()
        {
            GridTable table = new GridTable(new TextFormat() { Background = Color.FromHex(_Character.ColorTheme) }, new TextFormat());
            table.AddHeader(new List<string>()
                    {
                        "Move",
                        "Hitbox Active",
                        "FAF",
                    });


            List<Move> moves = Runtime.GetMoves(_Character.OwnerId, MoveType.Ground).Where(m => m.name == "Standing Grab" || m.name == "Dash Grab" || m.name == "Pivot Grab").ToList();

            foreach (Move move in moves)
            {
                table.AddRow(new List<string>
                        {
                            move.name,
                            move.hitboxActive,
                            move.FAF
                        });
            }
            
            return table;
        }

        public GridTable GetSpecificAttributeTable()
        {
            SpecificAttribute sa = _Character.specificAttribute;

            GridTable table = new GridTable(new TextFormat() { Background = Color.FromHex(_Character.ColorTheme) }, new TextFormat());
            if (sa.isSimple)
            {
                table.AddHeader(new List<string>()
                {
                    sa.attribute,
                    "-"
                });
                foreach(RowValue r in sa.NameValueTable)
                {
                    table.AddRow(new List<string>()
                    {
                        r.name,
                        r.value
                    });
                }
            }
            else
            {
                table.AddHeader(sa.headers);
                foreach (List<string> l in sa.data)
                {
                    table.AddRow(l);
                }
            }

            return table;
        }

        private async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            ((ListView)sender).SelectedItem = null;

            StackLayout s = new StackLayout();

            int index = Items.IndexOf((string)e.Item);
            switch (index)
            {
                case 0:
                    //All data
                    s.Children.Add(await GetMovementTable().Generate());
                    if (_Character.hasSpecificAttributes)
                        if (_Character.specificAttribute.isSimple)
                            s.Children.Add(await GetSpecificAttributeTable().Generate());
                    s.Children.Add(new Label() { Text = "Ground moves", FontSize = 20, TextColor = Color.Black });
                    s.Children.Add(await GetMoveTable(MoveType.Ground).Generate());
                    s.Children.Add(await GetMoveTable(MoveType.Evasion).Generate());
                    s.Children.Add(new Label() { Text = "Throws", FontSize = 20, TextColor = Color.Black });
                    s.Children.Add(await GetMoveTable(MoveType.Throw).Generate());
                    if (_Character.DisplayName == "Little Mac")
                        s.Children.Add(new Label() { Text = "Aerial moves (Please don't actually use these)", FontSize = 20, TextColor = Color.Black });
                    else
                        s.Children.Add(new Label() { Text = "Aerial moves", FontSize = 20, TextColor = Color.Black });
                    s.Children.Add(await GetMoveTable(MoveType.Aerial).Generate());
                    s.Children.Add(new Label() { Text = "Special moves", FontSize = 20, TextColor = Color.Black });
                    s.Children.Add(await GetMoveTable(MoveType.Special).Generate());
                    if (_Character.hasSpecificAttributes)
                        if (!_Character.specificAttribute.isSimple)
                            s.Children.Add(await GetSpecificAttributeTable().Generate());
                    await Navigation.PushAsync(new CharacterMoves(_Character, s, null));
                    break;
                case 1:
                    //Attributes
                    await Navigation.PushAsync(new CharacterMoves(_Character, GetMovementTable(), "Attributes"));
                    break;
                case 2:
                    //Detailed attributes
                    await Navigation.PushAsync(new CharacterMoves(_Character, GetAttributeTable(), "Detailed Attributes"));
                    break;
                case 3:
                    //All moves
                    s.Children.Add(new Label() { Text = "Ground moves", FontSize = 20, TextColor = Color.Black });
                    s.Children.Add(await GetMoveTable(MoveType.Ground).Generate());
                    s.Children.Add(await GetMoveTable(MoveType.Evasion).Generate());
                    s.Children.Add(new Label() { Text = "Throws", FontSize = 20, TextColor = Color.Black });
                    s.Children.Add(await GetMoveTable(MoveType.Throw).Generate());
                    if (_Character.DisplayName == "Little Mac")
                        s.Children.Add(new Label() { Text = "Aerial moves (Please don't actually use these)", FontSize = 20, TextColor = Color.Black });
                    else
                        s.Children.Add(new Label() { Text = "Aerial moves", FontSize = 20, TextColor = Color.Black });
                    s.Children.Add(await GetMoveTable(MoveType.Aerial).Generate());
                    s.Children.Add(new Label() { Text = "Special moves", FontSize = 20, TextColor = Color.Black });
                    s.Children.Add(await GetMoveTable(MoveType.Special).Generate());
                    await Navigation.PushAsync(new CharacterMoves(_Character, s, "Moves"));
                    break;
                case 4:
                    //Ground moves
                    s.Children.Add(await GetMoveTable(MoveType.Ground).Generate());
                    s.Children.Add(await GetMoveTable(MoveType.Evasion).Generate());
                    await Navigation.PushAsync(new CharacterMoves(_Character, s, "Ground moves"));
                    break;
                case 5:
                    //Throws
                    s.Children.Add(await GetMoveTable(MoveType.Throw).Generate());
                    s.Children.Add(await GetGrabsTable().Generate());
                    await Navigation.PushAsync(new CharacterMoves(_Character, s, "Throws"));
                    break;
                case 6:
                    //Aerial moves
                    await Navigation.PushAsync(new CharacterMoves(_Character, GetMoveTable(MoveType.Aerial), "Aerial moves"));
                    break;
                case 7:
                    //Specials
                    await Navigation.PushAsync(new CharacterMoves(_Character, GetMoveTable(MoveType.Special), "Specials"));
                    break;
                case 8:
                    //Specific attribute for character
                    await Navigation.PushAsync(new CharacterMoves(_Character, GetSpecificAttributeTable(), _Character.specificAttribute.attribute));
                    break;
            }
        }
    }
}
