using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Path = System.IO.Path;

namespace trabajo
{

    public partial class game : Window
    {

        private MainWindow parent;
        private ViewModel controlador;

        DispatcherTimer timer;
        DispatcherTimer timerContadorSeg;
        DispatcherTimer timerCreaFigura;
        bool flag = true;
        int ID = -1;


        public game()
        {

        }
        public game(MainWindow parent, ViewModel c)
        {
            InitializeComponent();
            
            this.parent = parent;
            parent.Hide();
            
            this.controlador = c;
            
            timer = new DispatcherTimer();
            timer.Tick += OnTick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);

            timerContadorSeg = new DispatcherTimer();
            timerContadorSeg.Tick += TickContadorSeg;
            timerContadorSeg.Interval = new TimeSpan(0, 0, 1);
            LabelContador.Content = controlador.GetSegC().ToString();


            Random rand = new Random();
            timerCreaFigura = new DispatcherTimer();
            timerCreaFigura.Tick += TickCreaFigura;
            timerCreaFigura.Interval = new TimeSpan(0, 0, 0, rand.Next(3) + 2);

            controlador.SetPuntuacionC(0);
            LabelPuntuacion.Content = controlador.GetPuntuacionC().ToString();
           
            //gestion del canvas
            Lienzo.MouseLeftButtonDown += Lienzo_MouseLeftButtonDown;

            //Eventos vm            
            controlador.FiguraDeleted += Controlador_FiguraDeleted;
            controlador.DeleteAll += Controlador_DeleteAll;

            //GameOver.Visibility = Visibility.Collapsed;
            controlador.SetPartidaPerdidaC(false);
            controlador.SetBombasC(0);

            if (controlador.GetJuegoAutoBoolC())
            {
                EliminarUltimo.Visibility = Visibility.Hidden;
                CrearFigura.Visibility = Visibility.Hidden;
            }          

        }

        private void CrearFigura_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            double posX = rand.Next(600) + 200, posY = -50, desX = (rand.NextDouble() * (4 - (-4)) + (-4)), desY = rand.NextDouble() * (4 - 1) + 1;
            int width = rand.Next(30) + 50, height = rand.Next(30) + 50, tipo = 0;

            Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height); //retorna un tiop Figura
            putFigura(f);

        }

        private void putFigura(Figura f)
        {
            if (f != null)
            {
                controlador.AddFiguraC(f);//añadimos al array del modelo la figura
            }
            Lienzo.Children.Add(f.shapeFigura); //se añade al canvas el shape
            Canvas.SetLeft(f.shapeFigura, f.x); //se posiciona
            Canvas.SetTop(f.shapeFigura, f.y);
        }


        private Figura CreateNewFigura(int tipo, double posX, double posY, double desX, double desY, int width, int height)
        {
            String nombre;
            Shape shape;
            ImageBrush imgBrush = new ImageBrush();
            this.ID++;
            Random rand = new Random();          
            int i = rand.Next(4);
            if (controlador.GetJuegoAutoBoolC())
                i = tipo;

            if (i == 0)
            {
                shape = new Rectangle();
                nombre = "Manzana";
                imgBrush.ImageSource = new BitmapImage(new Uri(@"../../Material/Manzana.png", UriKind.Relative));
            }
            else if (i == 1)
            {
                shape = new Rectangle();
                nombre = "Pera";
                imgBrush.ImageSource = new BitmapImage(new Uri(@"../../Material/Pera.png", UriKind.Relative));
            }
            else if (i == 2)
            {
                shape = new Rectangle();
                nombre = "Platano";
                imgBrush.ImageSource = new BitmapImage(new Uri(@"../../Material/Platano.png", UriKind.Relative));
            }
            else
            {
                shape = new Rectangle();
                nombre = "Bomba";
                imgBrush.ImageSource = new BitmapImage(new Uri(@"../../Material/Bomba.png", UriKind.Relative));
            }
            shape.Fill = imgBrush;
            shape.Width = width;//rand.Next(30) + 50;
            shape.Height = height;//rand.Next(30) + 50;
            shape.Uid = ID.ToString();
            return new Figura(nombre, posX, posY, desX, desY, shape, ID);

        }



        private void Lienzo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition(this);

            foreach (Figura f in controlador.GetCollectionOfFiguraC())
            {
                double top = Canvas.GetTop(f.shapeFigura);
                double left = Canvas.GetLeft(f.shapeFigura);
                double right = left + f.shapeFigura.Width;
                double bottom = top + f.shapeFigura.Height;

                if (pt.X > left && pt.X < right && pt.Y > top && pt.Y < bottom)
                {

                    controlador.RemoveFiguraC(f);

                    //Lienzo.Children.Remove(f.shapeFigura);
                    if (f.Name.Equals("Bomba"))
                    {
                        controlador.SetPuntuacionC(controlador.GetPuntuacionC() - 5);
                        LabelPuntuacion.Content = controlador.GetPuntuacionC().ToString();
                        controlador.SetBombasC(controlador.GetBombasC() + 1);
                        if (controlador.GetBombasC().Equals(1))
                        {
                            LabelBombas.Content = "X";
                        }
                        else if (controlador.GetBombasC().Equals(2))
                        {
                            LabelBombas.Content = "X X";
                        }
                        else if (controlador.GetBombasC().Equals(3))
                        {
                            LabelBombas.Content = "X X X";
                        }

                        if (controlador.GetPuntuacionC() < 0 || controlador.GetBombasC().Equals(3))
                        {
                            controlador.SetPartidaPerdidaC(true);
                            timer.Stop();
                            timerContadorSeg.Stop();
                            timerCreaFigura.Stop();
                            controlador.RemoveAllFigurasC();
                            controlador.SetPuntuacionC(0);
                        }

                        MediaPlayer sonidoBomba = new MediaPlayer();
                        sonidoBomba.Open(new Uri(@"../../Material/SonidoBomba.mp3", UriKind.Relative));
                        sonidoBomba.Play();
                    }
                    else
                    {
                        controlador.SetPuntuacionC(controlador.GetPuntuacionC() + 1);
                        LabelPuntuacion.Content = controlador.GetPuntuacionC().ToString();

                        MediaPlayer sonidoFruta = new MediaPlayer();
                        sonidoFruta.Open(new Uri(@"../../Material/SonidoFruta.mp3", UriKind.Relative));
                        sonidoFruta.Play();
                        
                    }
                    break;
                }
            }


        }


        private void EliminarUltimo_Click(object sender, RoutedEventArgs e)
        {
            if (controlador.GetLengthListFiguraC() >= 0)
                controlador.RemoveLastFiguraC();
        }

        private void Animar_Click(object sender, RoutedEventArgs e)
        {
            
            if (controlador.GetJuegoAutoBoolC())
            {

                Animar.Visibility = Visibility.Hidden;
                timer.Start(); //empieza el timer que llama cada milisegundo a onTick
                timerContadorSeg.Start();
                timerCreaFigura.Start();
            }
            else
            {
                if (flag) //si esta parado
                {
                    timer.Start(); //empieza el timer que llama cada milisegundo a onTick
                    timerContadorSeg.Start();
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(@"../../Material/PauseBoton.png", UriKind.Relative));
                    img.Height = 66;
                    img.Width = 64;
                    Animar.Content = img;
                    
                    flag = false;
                }
                else
                {
                    timer.Stop();
                    timerContadorSeg.Stop();
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(@"../../Material/PlayBoton.png", UriKind.Relative));
                    img.Height = 66;
                    img.Width = 64;
                    Animar.Content = img;
                    flag = true;
                }
            }

        }

        private void OnTick(object sender, EventArgs e)
        {
            foreach (Figura f in controlador.GetCollectionOfFiguraC())//de cada figura del array
            {
                f.mueve(Lienzo.ActualWidth); //cambia posX yposY
                Canvas.SetLeft(f.shapeFigura, f.x);
                Canvas.SetTop(f.shapeFigura, f.y);
                if (f.y > Lienzo.ActualHeight)
                {

                    controlador.RemoveFiguraC(f);
                    if (f.Name.Equals("Manzana") || f.Name.Equals("Platano") || f.Name.Equals("Pera"))
                    {
                        controlador.SetPuntuacionC(controlador.GetPuntuacionC() - 3);

                        LabelPuntuacion.Content = controlador.GetPuntuacionC().ToString();

                        if (controlador.GetPuntuacionC() < 0)
                        {
                            controlador.SetPartidaPerdidaC(true);
                            timer.Stop();
                            timerContadorSeg.Stop();
                            timerCreaFigura.Stop();
                            controlador.RemoveAllFigurasC();
                            controlador.SetPuntuacionC(0);
                        }
                    }
                    break;
                }
            }

        }

        private void TickContadorSeg(object sender, EventArgs e)
        {

            controlador.SetSegC(controlador.GetSegC() - 1);
            
            if (controlador.GetSegC()<=0)
            {
                if (controlador.GetLengthListFiguraC() >=0 && controlador.GetSegC()<=0)
                {
                    LabelContador.Content =0;
                }
                else
                {
                   
                    timerContadorSeg.Stop();
                    timer.Stop();
                    timerCreaFigura.Stop();
                    controlador.RemoveAllFigurasC();
                    controlador.SetPuntuacionC(0);
                }
            }
            else
            {
                LabelContador.Content = controlador.GetSegC().ToString();
            }
        }

        private void TickCreaFigura(object sender, EventArgs e)
        {
            Random rand = new Random();
            double posX, posY, desX, desY;
            int width, height, numFiguras, tipo;


            if (controlador.GetSegC() <= 60 && controlador.GetSegC() > 55)
            {
                numFiguras = (rand.Next(3) + 2);
                for (int i = 0; i < numFiguras; i++)
                {
                    posX = rand.Next(600) + 200;
                    posY = -(rand.Next(20) + 55);
                    desX = (rand.NextDouble() * (5 - (-5)) + (-5));
                    desY = rand.NextDouble() * (3 - 1) + 1;
                    width = rand.Next(40) + 70;
                    height = rand.Next(40) + 70;
                    tipo = rand.Next(4);
                    Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height);
                    putFigura(f);
                }
            }
            else if (controlador.GetSegC() <= 55 && controlador.GetSegC() > 50)
            {
                numFiguras = (rand.Next(3) + 3);
                for (int i = 0; i < numFiguras; i++)
                {
                    posX = rand.Next(600) + 200;
                    posY = -(rand.Next(20) + 55);
                    desX = (rand.NextDouble() * (4 - (-4)) + (-4));
                    desY = rand.NextDouble() * (4 - 1) + 1;
                    width = rand.Next(40) + 60;
                    height = rand.Next(40) + 60;

                    tipo = rand.Next(4);
                    Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height);
                    putFigura(f);
                }
            }
            else if (controlador.GetSegC() <= 50 && controlador.GetSegC() > 45)
            {
                numFiguras = (rand.Next(5) + 3);
                for (int i = 0; i < numFiguras; i++)
                {
                    posX = rand.Next(600) + 200;
                    posY = -(rand.Next(20) + 55);
                    desX = (rand.NextDouble() * (4 - (-4)) + (-4));
                    desY = rand.NextDouble() * (4 - 2) + 2;
                    width = rand.Next(30) + 50;
                    height = rand.Next(30) + 50;

                    tipo = rand.Next(4);
                    Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height);
                    putFigura(f);
                }
            }
            else if (controlador.GetSegC() <= 45 && controlador.GetSegC() > 40)
            {
                numFiguras = (rand.Next(5) + 2);
                for (int i = 0; i < numFiguras; i++)
                {
                    posX = rand.Next(600) + 200;
                    posY = -(rand.Next(20) + 55);
                    desX = (rand.NextDouble() * (3.5 - (-3.5)) + (-3.5));
                    desY = rand.NextDouble() * (5 - 2.5) + 2.5;
                    width = rand.Next(30) + 50;
                    height = rand.Next(30) + 50;

                    tipo = rand.Next(4);
                    Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height);
                    putFigura(f);
                }
            }
            else if (controlador.GetSegC() <= 40 && controlador.GetSegC() > 35)
            {
                numFiguras = (rand.Next(3) + 4);
                for (int i = 0; i < numFiguras; i++)
                {
                    posX = rand.Next(600) + 200;
                    posY = -(rand.Next(20) + 55);
                    desX = (rand.NextDouble() * (3 - (-3)) + (-3));
                    desY = rand.NextDouble() * (4 - 2.5) + 2.5;
                    width = rand.Next(30) + 50;
                    height = rand.Next(30) + 50;

                    tipo = rand.Next(4);
                    Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height);
                    putFigura(f);
                }
            }
            else if (controlador.GetSegC() <= 35 && controlador.GetSegC() > 30)
            {
                numFiguras = (rand.Next(3) + 2);
                for (int i = 0; i < numFiguras; i++)
                {
                    posX = rand.Next(600) + 200;
                    posY = -(rand.Next(20) + 55);
                    desX = (rand.NextDouble() * (5 - (-5)) + (-5));
                    desY = rand.NextDouble() * (4 - 1) + 1;
                    width = rand.Next(30) + 50;
                    height = rand.Next(30) + 50;

                    tipo = rand.Next(4);
                    Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height);
                    putFigura(f);
                }
            }
            else if (controlador.GetSegC() <= 30 && controlador.GetSegC() > 25)
            {
                numFiguras = (rand.Next(4) + 2);
                for (int i = 0; i < numFiguras; i++)
                {
                    posX = rand.Next(600) + 200;
                    posY = -(rand.Next(20) + 55);
                    desX = (rand.NextDouble() * (4 - (-4)) + (-4));
                    desY = rand.NextDouble() * (5 - 2) + 2;
                    width = rand.Next(30) + 50;
                    height = rand.Next(30) + 50;

                    tipo = rand.Next(4);
                    Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height);
                    putFigura(f);
                }

            }
            else if (controlador.GetSegC() <= 25 && controlador.GetSegC() > 20)
            {
                numFiguras = (rand.Next(3) + 3);
                for (int i = 0; i < numFiguras; i++)
                {
                    posX = rand.Next(600) + 200;
                    posY = -(rand.Next(20) + 55);
                    desX = (rand.NextDouble() * (3.2 - (-3.2)) + (-3.2));
                    desY = rand.NextDouble() * (5 - 3) + 3;
                    width = rand.Next(30) + 50;
                    height = rand.Next(30) + 50;

                    tipo = rand.Next(4);
                    Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height);
                    putFigura(f);
                }
            }
            else if (controlador.GetSegC() <= 20 && controlador.GetSegC() > 15)
            {
                numFiguras = (rand.Next(6) + 2);
                for (int i = 0; i < numFiguras; i++)
                {
                    posX = rand.Next(600) + 200;
                    posY = -(rand.Next(20) + 55);
                    desX = (rand.NextDouble() * (4 - (-4)) + (-4));
                    desY = rand.NextDouble() * (5.5 - 1) + 1;
                    width = rand.Next(30) + 50;
                    height = rand.Next(30) + 50;

                    tipo = rand.Next(4);
                    Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height);
                    putFigura(f);
                }
            }
            else if (controlador.GetSegC() <= 15 && controlador.GetSegC() > 10)
            {
                numFiguras = (rand.Next(5) + 4);
                for (int i = 0; i < numFiguras; i++)
                {
                    posX = rand.Next(600) + 200;
                    posY = -(rand.Next(20) + 55);
                    desX = (rand.NextDouble() * (2.5 - (-2.5)) + (-2.5));
                    desY = rand.NextDouble() * (6 - 2) + 2;
                    width = rand.Next(30) + 50;
                    height = rand.Next(30) + 50;

                    tipo = rand.Next(4);
                    Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height);
                    putFigura(f);
                }
            }
            else if (controlador.GetSegC() <= 10 && controlador.GetSegC() > 5)
            {
                numFiguras = (rand.Next(4) + 4);
                for (int i = 0; i < numFiguras; i++)
                {
                    posX = rand.Next(600) + 200;
                    posY = -(rand.Next(20) + 55);
                    desX = (rand.NextDouble() * (3 - (-3)) + (-3));
                    desY = rand.NextDouble() * (5 - 3.5) + 3.5;
                    width = rand.Next(30) + 40;
                    height = rand.Next(30) + 40;

                    tipo = rand.Next(4);
                    Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height);
                    putFigura(f);
                }
            }
            else if (controlador.GetSegC() <= 5 && controlador.GetSegC() > 0)
            {

                numFiguras = (rand.Next(7) + 4);
                for (int i = 0; i < numFiguras; i++)
                {
                    posX = rand.Next(600) + 200;
                    posY = -(rand.Next(20) + 55);
                    desX = (rand.NextDouble() * (2 - (-2)) + (-2));
                    desY = rand.NextDouble() * (6.5 - 3) + 3;
                    width = rand.Next(25) + 40;
                    height = rand.Next(25) + 40;

                    tipo = rand.Next(4);
                    Figura f = CreateNewFigura(tipo, posX, posY, desX, desY, width, height);
                    putFigura(f);
                }

            }
            else
            {
            }


        }



        private void Window_Closed(object sender, EventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                timerContadorSeg.Stop();
                timerCreaFigura.Stop();
                controlador.RemoveAllFigurasC();
                //vm.SetJuegoAutoBoolC(null);
            }
            else { controlador.RemoveAllFigurasC(); }
            this.parent.Show();
        }

       


        private void Controlador_FiguraDeleted(object sender, ControladorEventArgs e)
        {
            Figura f = (Figura)e.Figura;

            foreach (UIElement Figura in Lienzo.Children)
            {
                if (f.Id.ToString().Equals(Figura.Uid))
                {

                    Lienzo.Children.Remove(Figura);
                    break;
                }
            }
            
        }

        private void Controlador_DeleteAll(object sender, ControladorEventArgs e)
        {
            Lienzo.Children.Clear();
            if (controlador.GetPartidaPerdidaC())
            {
                Image ImgGameOver = new Image
                {
                    Width = 800,
                    Height = 800,

                    Name = "GameOver",
                    Source = new BitmapImage(new Uri(@"../../Material/game over.png", UriKind.Relative))
                };
                Canvas.SetTop(ImgGameOver, (Lienzo.ActualHeight / 2) - ((ImgGameOver.Height) / 2));
                Canvas.SetLeft(ImgGameOver, (Lienzo.ActualWidth / 2) - ((ImgGameOver.Width) / 2));
                Lienzo.Children.Add(ImgGameOver);

                MediaPlayer sonidoGameOver = new MediaPlayer();
                sonidoGameOver.Open(new Uri(@"../../Material/SonidoGame over.mp3", UriKind.Relative));
                sonidoGameOver.Play();
            }
            else
            {
                Image ImgWin = new Image
                {
                    Width = 850,
                    Height = 850,

                    Name = "GameOver",
                    Source = new BitmapImage(new Uri(@"../../Material/Win.png", UriKind.Relative))
                };
                Canvas.SetTop(ImgWin, (Lienzo.ActualHeight / 2) - ((ImgWin.Height) / 2));
                Canvas.SetLeft(ImgWin, (Lienzo.ActualWidth / 2) - ((ImgWin.Width) / 2));
                Lienzo.Children.Add(ImgWin);

                Label LabelPuntuacion = new Label
                {
                    Content = "Tu puntuacion ha sido " + controlador.GetPuntuacionC(),
                    Height = 500,
                    FontSize = 60,                    
                };
                Canvas.SetTop(LabelPuntuacion, 620);
                Canvas.SetLeft(LabelPuntuacion, 200);
                Lienzo.Children.Add(LabelPuntuacion);
            }
        }

        

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = new Cursor($@"{Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))}\Material\arrow.cur");
        }
    }
}
