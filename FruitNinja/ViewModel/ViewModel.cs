using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace trabajo
{
    public class ControladorEventArgs : EventArgs //contiene el elemento afectado por la accion que ha disparado el evento
    {
        public object Figura { get; set; }

        public ControladorEventArgs()
        {
            this.Figura = null;
        }
        public ControladorEventArgs(object Figura)
        {
            this.Figura = Figura;
        }
    }

    public delegate void ControladorEventHandler(object sender, ControladorEventArgs e); //le damos nombre

    public class ViewModel
    {
        private Modelo modelo;

        public event ControladorEventHandler FiguraDeleted;
        public event ControladorEventHandler DeleteAll;

        public ViewModel()
        {
            modelo = new Modelo();//iniciamos el constructor de arrayFiguras
        }

        public void AddFiguraC(Figura f)
        {
            modelo.addFigura(f);
        }

        public void RemoveFiguraC(Figura f)
        {
            modelo.RemoveFigura(f);
            OnFiguraDeleted(f);
        }

        public void RemoveLastFiguraC()
        {
            RemoveFiguraC(modelo.GetLastFigura());
        }


        public void RemoveAllFigurasC()
        {
            modelo.RemoveAllFiguras();
            OnDeleteAll();
        }

        public ObservableCollection<Figura> GetCollectionOfFiguraC()
        {
            return modelo.GetCollectionOfFigura();
        }

        public int GetLengthListFiguraC()
        {

            return (modelo.GetLengthListFigura());
        }

        public int GetSegC()
        {
            return modelo.Seg;
        }
        public void SetSegC(int s)
        {
            modelo.Seg = s;
        }

        public int GetBombasC()
        {
            return modelo.Bombas;
        }
        public void SetBombasC(int b)
        {
            modelo.Bombas = b;
        }


        public int GetPuntuacionC()
        {
            return modelo.Puntuacion;
        }
        public void SetPuntuacionC(int puntuacion)
        {
            modelo.Puntuacion = puntuacion;
        }

        public bool GetJuegoAutoBoolC()
        {
            return modelo.JuegoAutoBool;
        }
        public void SetJuegoAutoBoolC(bool j)
        {
            modelo.JuegoAutoBool = j;
        }


        public void SetPartidaPerdidaC(bool p)
        {
            modelo.PartidaPerdida = p;
        }

        public bool GetPartidaPerdidaC()
        {
            return modelo.PartidaPerdida;
        }

        public ObservableCollection<Figura> GetAllFigurasForBinding()
        {
            return modelo.GetAllFigurasForBinding();
        }

        protected virtual void OnFiguraDeleted(Figura Figura)//entra cada que se elimina
        {
            if (null != FiguraDeleted) FiguraDeleted(this, new ControladorEventArgs(Figura));

        }

        protected virtual void OnDeleteAll()
        {
            if (null != DeleteAll) DeleteAll(this, new ControladorEventArgs());
        }


    }
}
