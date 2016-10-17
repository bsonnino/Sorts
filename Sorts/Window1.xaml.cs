using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Sorts
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private static readonly DispatcherOperationCallback exitFrameCallback = ExitFrame;
        private readonly List<Rectangle> listRect = new List<Rectangle>();
        private bool finished1;
        private bool finished2;
        private GenericSorts sort;
        private bool terminou;

        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DesenhaBarras();
        }

        private void DesenhaBarras()
        {
            var rnd = new Random();
            rectPanel.Children.Clear();
            listRect.Clear();
            for (int i = 0; i < 300; i++)
            {
                var rect = new Rectangle();
                rect.Width = 2;
                rect.Height = rnd.Next(300);
                rect.Stroke = Brushes.Red;
                rect.Name = string.Format("rect{0}", i);
                Canvas.SetLeft(rect, i * 2);
                Canvas.SetTop(rect, rectPanel.ActualHeight - rect.Height);
                listRect.Add(rect);
                rectPanel.Children.Add(rect);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnClassifica.Content == "Para")
            {
                sort.Termina();
                return;
            }
            if (comboBox1.SelectedIndex == 0)
                sort = new BubbleSort(listRect);
            else if (comboBox1.SelectedIndex == 1)
                sort = new SelectionSort(listRect);
            else
                sort = new QuickSort(listRect);
            sort.TrocouRetangulos += sort_TrocouRetangulos;
            btnClassifica.Content = "Para";
            btnMistura.IsEnabled = false;
            comboBox1.IsEnabled = false;
            slider1.IsEnabled = false;
            terminou = false;
            var sw = new Stopwatch();
            sw.Start();
            sort.Execute();
            btnClassifica.Content = "Classifica";
            btnMistura.IsEnabled = true;
            comboBox1.IsEnabled = true;
            slider1.IsEnabled = true;
            velText.Text = string.Format("Tempo: {0}ms", sw.Elapsed.Milliseconds);
            sort = null;
        }

        private void sort_TrocouRetangulos(object o, TrocaRetangulosEventArgs e)
        {
            if (!IsVisible)
                return;
            double leftMenor = Canvas.GetLeft(listRect[e.Menor]);
            double leftMaior = Canvas.GetLeft(listRect[e.Maior]);

            if (slider1.Value == 0 || terminou)
            {
                Canvas.SetLeft(listRect[e.Menor], leftMaior);
                Canvas.SetLeft(listRect[e.Maior], leftMenor);
            }
            else
            {
                var duration = new Duration(TimeSpan.FromMilliseconds(slider1.Value));
                finished1 = finished2 = false;
                var da1 = new DoubleAnimation(leftMenor, duration);
                var da2 = new DoubleAnimation(leftMaior, duration);
                da1.Completed += da1_Completed;
                da2.Completed += da2_Completed;

                listRect[e.Maior].BeginAnimation(Canvas.LeftProperty, da1);
                listRect[e.Menor].BeginAnimation(Canvas.LeftProperty, da2);
                while (!finished1 && !finished2)
                {
                    DoEvents();
                    if (!IsVisible)
                        return;
                }
            }
        }

        public static void DoEvents()
        {
            // Create new nested message pump.
            var nestedFrame = new DispatcherFrame();

            // Dispatch a callback to the current message queue, when getting called, 
            // this callback will end the nested message loop.
            // note that the priority of this callback should be lower than the that of UI event messages.
            DispatcherOperation exitOperation = Dispatcher.CurrentDispatcher.BeginInvoke(
                DispatcherPriority.Background, exitFrameCallback, nestedFrame);

            // pump the nested message loop, the nested message loop will 
            // immediately process the messages left inside the message queue.
            Dispatcher.PushFrame(nestedFrame);

            // If the "exitFrame" callback doesn't get finished, Abort it.
            if (exitOperation.Status != DispatcherOperationStatus.Completed)
            {
                exitOperation.Abort();
            }
        }

        private static Object ExitFrame(Object state)
        {
            var frame = state as DispatcherFrame;
            // Exit the nested message loop.
            if (frame != null) frame.Continue = false;
            return null;
        }

        private void da2_Completed(object sender, EventArgs e)
        {
            finished2 = true;
        }

        private void da1_Completed(object sender, EventArgs e)
        {
            finished1 = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DesenhaBarras();
            velText.Text = "";
        }
    }
}