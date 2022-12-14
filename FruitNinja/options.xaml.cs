using System;
using System.Windows;

namespace trabajo
{

    public partial class options : Window
    {
        private MainWindow parent;
        private ViewModel vm;

        public options()
        {
            InitializeComponent();
        }

        public options(MainWindow parent, ViewModel c)
        {
            InitializeComponent();
            this.parent = parent;

            this.vm = c;

            //nombre del datagrid
            DataGrid.ItemsSource = vm.GetAllFigurasForBinding();
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            if (this.parent.IsActive)
                this.parent.Show();
        }

        private void EliminarFigura_Click(object sender, RoutedEventArgs e)
        {
            Figura f = (Figura)DataGrid.SelectedItem;
            if (null != f)
                vm.RemoveFiguraC(f);
        }
    }
}
