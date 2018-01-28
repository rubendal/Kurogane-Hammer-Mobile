using Kurogane_Hammer.Classes;
using Kurogane_Hammer.ViewAdapters;
using Kurogane_Hammer.Views.Toolbar;
using Rg.Plugins.Popup.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kurogane_Hammer.Views.CharacterView
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterMoves : ContentPage
    {

        private enum SortOrder
        {
            NONE,
            ACTIVE_FRAME,
            BKB,
            KBG,
            DAMAGE,
            ANGLE
        }

        private Character _Character;
        private int _index = -1;

        private SortOrder sortOrder = SortOrder.NONE;

        public CharacterMoves()
        {
            InitializeComponent();
        }

        public CharacterMoves(Character c, int index)
        {
            InitializeComponent();

            _Character = c;
            _index = index;

            string title = null;

            switch (index)
            {
                case 1:
                    title = "Attributes";
                    if (ToolbarItems.Contains(Sort_Order))
                        ToolbarItems.Remove(Sort_Order);
                    break;
                case 2:
                    title = "Detailed Attributes";
                    if (ToolbarItems.Contains(Sort_Order))
                        ToolbarItems.Remove(Sort_Order);
                    break;
                case 3:
                    title = "Moves";
                    break;
                case 4:
                    title = "Ground moves";
                    break;
                case 5:
                    title = "Throws";
                    break;
                case 6:
                    title = "Aerial moves";
                    break;
                case 7:
                    title = "Specials";
                    break;
                case 8:
                    title = _Character.specificAttribute.attribute;
                    if (ToolbarItems.Contains(Sort_Order))
                        ToolbarItems.Remove(Sort_Order);
                    break;
            }

            if (title == null)
                Title = $"{_Character.getCharacterTitleName()}";
            else
                Title = $"{_Character.getCharacterTitleName()}/{title}";

            CreateView(index);
        }

        private async void CreateView(int index, bool sort = false)
        {

            if (sort)
                await PopupNavigation.PushAsync(new ProgressDialog("Sorting data"));
            else
                await PopupNavigation.PushAsync(new ProgressDialog("Creating page"));

            StackLayout s = new StackLayout();

            

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
                    break;
                case 1:
                    //Attributes
                    s.Children.Add(await GetMovementTable().Generate());
                    break;
                case 2:
                    //Detailed attributes
                    s.Children.Add(await GetAttributeTable().Generate());
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
                    break;
                case 4:
                    //Ground moves
                    s.Children.Add(await GetMoveTable(MoveType.Ground).Generate());
                    s.Children.Add(await GetMoveTable(MoveType.Evasion).Generate());
                    break;
                case 5:
                    //Throws
                    s.Children.Add(await GetMoveTable(MoveType.Throw).Generate());
                    s.Children.Add(await GetGrabsTable().Generate());
                    break;
                case 6:
                    //Aerial moves
                    s.Children.Add(await GetMoveTable(MoveType.Aerial).Generate());
                    break;
                case 7:
                    //Specials
                    s.Children.Add(await GetMoveTable(MoveType.Special).Generate());
                    break;
                case 8:
                    //Specific attribute for character
                    s.Children.Add(await GetSpecificAttributeTable().Generate());
                    break;
            }

            Layout.Content = s;

            await PopupNavigation.PopAsync();
        }

        private GridTable GetMovementTable()
        {
            GridTable table = new GridTable(new TextFormat() { Background = Color.FromHex(_Character.ColorTheme) }, new TextFormat());
            table.AddHeader(new List<string>()
                    {
                        "Attribute",
                        "Value"
                    });
            List<Movement> list = Runtime.GetMovements(_Character.OwnerId);
            foreach (Movement m in list)
            {
                table.AddRow(new List<string>()
                    {
                        m.name,
                        m.value
                    });
            }

            return table;
        }

        private GridTable GetAttributeTable()
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
                foreach (AttributeValue v in a.attributes)
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

        private GridTable GetMoveTable(MoveType moveType)
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


            List<Move> moves = _Character.moves[moveType];

            switch (sortOrder)
            {
                case SortOrder.ACTIVE_FRAME:
                    moves = moves.OrderBy(m =>
                    {
                        if (string.IsNullOrWhiteSpace(m.hitboxActive))
                            return -1;
                        int frame = 0;
                        if (int.TryParse(new Regex(@"/[a-z]|\?|\(.+\)|\:/gi").Replace(m.hitboxActive,"").Split('-')[0].Split(',')[0], out frame))
                            return frame;
                        else
                            return -1;
                    }).ToList();
                    break;
                case SortOrder.DAMAGE:
                    moves = moves.OrderByDescending(m =>
                    {
                        if (string.IsNullOrWhiteSpace(m.baseDamage))
                            return -1;
                        float damage = 0;
                        if (float.TryParse(new Regex(@"/[a-z]|\?|\(.+\)|\:/gi").Replace(m.baseDamage, "").Split('-')[0].Split(',')[0].Split('(')[0].Split('/')[0], out damage))
                            return damage;
                        else
                            return -1;
                    }).ToList();
                    break;
                case SortOrder.ANGLE:
                    moves = moves.OrderBy(m =>
                    {
                        if (string.IsNullOrWhiteSpace(m.angle))
                            return 1000;
                        int angle = 0;
                        if (int.TryParse(m.angle.Split('/')[0].Split('-')[0].Split('(')[0], out angle))
                            return angle;
                        else
                            return 1000;
                    }).ToList();
                    break;
                case SortOrder.BKB:
                    moves = moves.OrderByDescending(m =>
                    {
                        if (string.IsNullOrWhiteSpace(m.bkb))
                            return -1;
                        int bkb = 0;
                        if (int.TryParse(m.bkb.Replace("W:","").Replace("B:","").TrimStart().Split('/')[0].Split('-')[0].Split('(')[0].Split(' ')[0], out bkb))
                            return bkb;
                        else
                            return -1;
                    }).ToList();
                    break;
                case SortOrder.KBG:
                    moves = moves.OrderByDescending(m =>
                    {
                        if (string.IsNullOrWhiteSpace(m.kbg))
                            return -1;
                        int kbg = 0;
                        if (int.TryParse(m.kbg.Split('/')[0].Split('-')[0].Split('(')[0].Split(' ')[0], out kbg))
                            return kbg;
                        else
                            return -1;
                    }).ToList();
                    break;
            }

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

        private GridTable GetGrabsTable()
        {
            GridTable table = new GridTable(new TextFormat() { Background = Color.FromHex(_Character.ColorTheme) }, new TextFormat());
            table.AddHeader(new List<string>()
                    {
                        "Move",
                        "Hitbox Active",
                        "FAF",
                    });


            List<Move> moves = _Character.moves[MoveType.Ground].Where(m => m.name == "Standing Grab" || m.name == "Dash Grab" || m.name == "Pivot Grab").ToList();

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
                foreach (RowValue r in sa.NameValueTable)
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

        private async void Sort_Order_Clicked(object sender, System.EventArgs e)
        {
            SortOrder prev = sortOrder;
            string res = await DisplayActionSheet("Sort options", "Cancel", null, "None", "Active frames", "Damage", "Angle", "BKB", "KBG");
            switch (res)
            {
                case "None":
                    sortOrder = SortOrder.NONE;
                    break;
                case "Active frames":
                    sortOrder = SortOrder.ACTIVE_FRAME;
                    break;
                case "Damage":
                    sortOrder = SortOrder.DAMAGE;
                    break;
                case "Angle":
                    sortOrder = SortOrder.ANGLE;
                    break;
                case "BKB":
                    sortOrder = SortOrder.BKB;
                    break;
                case "KBG":
                    sortOrder = SortOrder.KBG;
                    break;
            }

            if (prev != sortOrder)
                CreateView(_index, true);
        }
    }
}