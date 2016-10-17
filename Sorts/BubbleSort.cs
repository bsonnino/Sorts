using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace Sorts
{
    public class BubbleSort : GenericSorts
    {
        public BubbleSort(List<Rectangle> listRect) : base(listRect)
        {

        }

        public override void Execute()
        {
            base.Execute();
            for (int i = rectangles.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < rectangles.Count - 1; j++)
                {
                    if (terminado)
                        return;

                    if (rectangles[j].Height > rectangles[j + 1].Height)
                    {
                        Troca(j, j + 1);
                    }
                }
            }
        }
    }
}
