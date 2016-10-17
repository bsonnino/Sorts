using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace Sorts
{
    public class QuickSort : GenericSorts
    {
        public QuickSort(List<Rectangle> listRect)
            : base(listRect)
        {

        }

        int Pivot;

        private void Classifica(int esquerdo, int direito)
        {
            int esqTemp = esquerdo;
            int dirTemp = direito;
            Pivot = esquerdo;
            esquerdo++;
            if (terminado)
                return;

            while (direito >= esquerdo)
            {
                if (rectangles[esquerdo].Height >= rectangles[Pivot].Height
                      && rectangles[direito].Height < rectangles[Pivot].Height)
                    Troca(esquerdo, direito);
                else if (rectangles[esquerdo].Height >= rectangles[Pivot].Height)
                    direito--;
                else if (rectangles[direito].Height < rectangles[Pivot].Height)
                    esquerdo++;
                else
                {
                    direito--;
                    esquerdo++;
                }
            }

            Troca(Pivot, direito);
            Pivot = direito;
            if (Pivot > esqTemp)
                Classifica(esqTemp, Pivot);
            if (dirTemp > Pivot + 1)
                Classifica(Pivot + 1, dirTemp);
        }

        public override void Execute()
        {
            base.Execute();
            Classifica(0, rectangles.Count - 1);
        }

    }
}
