using Kurogane_Hammer.ViewAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Kurogane_Hammer.Classes;
using Newtonsoft.Json;

namespace Kurogane_Hammer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormulasPage : ContentPage
    {
        private List<Formulas> formulas;

        public FormulasPage()
        {
            InitializeComponent();

            formulas = JsonConvert.DeserializeObject<List<Formulas>>(App.storage.Read("data/formulas.json"));

            Start();
        }

        private GridTable GenerateTable(Formulas formula)
        {
            GridTable table = new GridTable(new TextFormat() { Background = Color.FromHex("#D0EAFF") }, new TextFormat()) { evenRowColor = Color.Transparent };

            table.AddHeader(new List<string>()
            {
                "FORMULA",
                "EQUATION"
            });

            table.AddRow(new List<string>()
            {
                formula.formula,
                formula.equation
            });

            table.AddRow(new List<string>()
            {
                "Variables/Notes",
                formula.notes
            });

            return table;
        }

        private async void Start()
        {
            StackLayout s = new StackLayout();

            foreach(Formulas formula in formulas)
             s.Children.Add(await GenerateTable(formula).Generate());

            Layout.Content = s;
        }
    }
}