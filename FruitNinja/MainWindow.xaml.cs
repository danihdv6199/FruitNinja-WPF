using System.IO;
using System.Windows;
using System.Windows.Input;
using Path = System.IO.Path;
namespace trabajo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel controlador;

        public MainWindow()
        {
            InitializeComponent();
            controlador = new ViewModel();//inicia el vm
        }

        private void ButtonJugarManual_Click(object sender, RoutedEventArgs e)
        {

            switch (SegundosCombo.SelectedIndex)
            {
                case 0:
                    controlador.SetSegC(30);
                    break;
                case 1:
                    controlador.SetSegC(45);
                    break;
                case 2:
                    controlador.SetSegC(60);
                    break;
            }
            controlador.SetJuegoAutoBoolC(false);
            game ventanaJuego = new game(this, controlador);
            ventanaJuego.Show();
        }

        private void ButtonOpciones_Click(object sender, RoutedEventArgs e)
        {
            options ventanaOptions = new options(this, controlador);
            ventanaOptions.Show();
        }

        private void ButtonJuegoAuto_Click(object sender, RoutedEventArgs e)
        {
            controlador.SetJuegoAutoBoolC(true);
            switch (SegundosCombo.SelectedIndex)
            {
                case 0:
                    controlador.SetSegC(30);
                    break;
                case 1:
                    controlador.SetSegC(45);
                    break;
                case 2:
                    controlador.SetSegC(60);
                    break;
            }
            game ventanaJuego = new game(this, controlador);
            ventanaJuego.Show();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = new Cursor($@"{Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))}\Material\arrow.cur");
        }
    }
}
