using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Exercise_2
{
    /// <summary>
    /// Interaction logic for Part_2Part_3.xaml
    /// </summary>
    public partial class Part_2Part_3 : UserControl
    {
        public string Title
        {
            get => tblTitle.Text;
            set
            {
                tblTitle.Text = value;
            }
        }
        public override string ToString()
        {
            return this.Title;
        }

        private List<DataGridColumn> _dataGrodColumns;

        private ViewModelPart_2 _viewModel = new ViewModelPart_2();

        public Part_2Part_3()
        {
            InitializeComponent();
            Initialise();
        }
        private void Initialise()
        {
            dgtcFirstName.Header = typeof(Person).GetProperties()[1].Name;
            dgtclastName.Header = typeof(Person).GetProperties()[2].Name;
            dgtcAge.Header = typeof(Person).GetProperties()[3].Name;
            dgtcEmail.Header =  typeof(Person).GetProperties()[4].Name;
            dgtcIsMember.Header = typeof(Person).GetProperties()[5].Name;
            dgtcOrderValue.Header = typeof(Person).GetProperties()[6].Name;
            dgccOrderStatus.Header = typeof(Person).GetProperties()[7].Name;


            this.dgrTwoWaydataGrid.CurrentCellChanged += (object sender, EventArgs e) => { CellEdited(); };
            this.dgrTwoWaydataGrid.CellEditEnding += (object sender, DataGridCellEditEndingEventArgs e) => { CellEdited(); };
            //

        }

        private void CellEdited()
        {
            long count = 0;
            List<Person> personLst = new List<Person>();
            foreach (var s in dgrTwoWaydataGrid.Items)
            {
                var r = s.GetType().GetProperties();
                var b = typeof(Person).GetProperties();
                Person person = new Person();
                for (int i = 1; i < typeof(Person).GetProperties().Length; i++)
                {

                    if (s.GetType().GetProperties().Length == typeof(Person).GetProperties().Length)
                        typeof(Person).GetProperties()[i].SetValue(person, s.GetType().GetProperties()[i].GetValue(s));
                }

                person.ID = count;
                count++;                                   
                personLst.Add(person);
            }

            _viewModel.Persons = personLst;
        }


    }
}
