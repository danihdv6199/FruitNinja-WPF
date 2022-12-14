using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace trabajo
{
    class Modelo
    {
        /*
         * Esta clase solamente inicia
         * listFiguras para su posterior
         * uso
         */
        private ObservableCollection<Figura> listFiguras;

        public Modelo()
        {
            this.listFiguras = new ObservableCollection<Figura>();
        }

        public void addFigura(Figura f)
        {
            this.listFiguras.Add(f);
        }


        public void RemoveFigura(Figura f)
        {

            this.listFiguras.Remove(f);

        }

        public ObservableCollection<Figura> GetCollectionOfFigura()
        {
            return this.listFiguras;
        }


        public int GetLengthListFigura()
        {
            return listFiguras.Count() - 1;
        }

        public ObservableCollection<Figura> GetAllFigurasForBinding()
        {
            return this.listFiguras;
        }

        public Figura GetLastFigura()
        {
            return this.listFiguras[this.listFiguras.Count() - 1];
        }

        public void RemoveAllFiguras()
        {
            if (!listFiguras.Equals(null))
            {
                this.listFiguras.Clear();
            }
        }
        private bool partidaPerdida = false;
        public bool PartidaPerdida
        {
            get
            {
                return this.partidaPerdida;
            }
            set
            {
                this.partidaPerdida = value;
            }
        }


        private bool juegoAutoBool;
        public bool JuegoAutoBool
        {
            get
            {
                return this.juegoAutoBool;
            }
            set
            {
                this.juegoAutoBool = value;
            }
        }

        private int seg;
        public int Seg
        {
            get
            {
                return this.seg;
            }
            set
            {
                this.seg = value;
            }
        }

        private int puntuacion;
        public int Puntuacion
        {
            get
            {
                return this.puntuacion;
            }
            set
            {
                this.puntuacion = value;
            }
        }

        private int bombas;
        public int Bombas
        {
            get
            {
                return this.bombas;
            }
            set
            {
                this.bombas = value;
            }
        }
    }
}
