using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace Sorts
{
    public delegate void TrocaRetangulosEventHandler(object o, TrocaRetangulosEventArgs e);

    public class TrocaRetangulosEventArgs : EventArgs
    {
        public int Menor { get; set; }
        public int Maior { get; set; }
    }

    public class GenericSorts
    {
        public event TrocaRetangulosEventHandler TrocouRetangulos;

        protected void OnTrocouRetangulos(int menor, int maior)
        {
            if (TrocouRetangulos != null)
                TrocouRetangulos(this, new TrocaRetangulosEventArgs() { Menor = menor, Maior = maior });
        }

        protected void Troca(int i, int j)
        {
            var rectTemp = rectangles[i];
            rectangles[i] = rectangles[j];
            rectangles[j] = rectTemp;
            OnTrocouRetangulos(i, j);
        }

        protected List<Rectangle> rectangles;
        protected bool terminado;

        public GenericSorts(List<Rectangle> listRect)
        {
            rectangles = listRect;
        }

        public virtual void Execute()
        {
            terminado = false;
        }

        public virtual void Termina()
        {
            terminado = true;
        }
    }
}
