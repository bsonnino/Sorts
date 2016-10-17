using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace Sorts
{
    public class SelectionSort : GenericSorts
    {
        public SelectionSort(List<Rectangle> listRect)
            : base(listRect)
        {

        }

        public override void Execute()
        {
            base.Execute();
            for (int i = 0; i < rectangles.Count - 1; i++)
            {
                for (int j = rectangles.Count - 1; j > i; j--)
                {
                    if (terminado)
                        return;

                    if (rectangles[i].Height > rectangles[j].Height)
                    {
                        Troca(i, j);
                    }
                }
            }
        }
    }
}
