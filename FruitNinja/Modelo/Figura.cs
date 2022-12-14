using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Shapes;

namespace trabajo
{
    public class Figura : INotifyPropertyChanged
    {
        
        private String nombre;
        private double posX;
        private double posY;
        private double desX;
        private double desY;
        private Shape shape;
        private int ID;

        public Figura(String nombre,
            double posX,
            double posY,
            double desX,
            double desY,
            Shape shape,
            int Id)
        {
            this.nombre = nombre;
            this.posX = posX;
            this.posY = posY;
            this.desX = desX;
            this.desY = desY;
            this.shape = shape;
            this.ID = Id;
            
        }


        public void mueve(double limite_x)//cambia posicion 
        {
            if (posX + desX + shape.Width > limite_x || posX + desX < 0)
            // || posY + desY > limite_y || posY + desY < 0)
            {
                desX = desX * -1;
                //desY = desY * -1;
            }
            posX += desX;
            posY += desY;
        }
        public int Id
        {
            get { return this.ID; }
            set
            {
                ID = value;
                OnPropertyChanged("ID");
            }
        }
        public String Name
        {
            get { return this.nombre; }
            set
            {
                this.nombre = value;
                OnPropertyChanged("Name");
            }
        }

        public double velX
        {
            get { return this.desX; }
            set
            {
                this.desX = value;
                OnPropertyChanged("velX");
            }
        }

        public double velY
        {
            get { return this.desY; }
            set
            {
                this.desY = value;
                OnPropertyChanged("velY");
            }
        }

        public double x
        {
            get { return posX; }
            set
            {
                posX = value;
                OnPropertyChanged("x");
            }
        }

        public double y
        {
            get { return posY; }
            set
            {
                posY = value;
                OnPropertyChanged("y");
            }
        }

        public Shape shapeFigura
        {
            get { return this.shape; }
            set
            {
                shape = value;
               
            }
        }

        /* ========================= PROPERTY EVENT NOTIFICATION METHODS ========================= */


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string PropertyName)
        {
            if (null != PropertyChanged) PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

    }
}
