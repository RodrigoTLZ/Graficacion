using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Unidad4
{
    /// <summary>
    /// Lógica de interacción para Elipse.xaml
    /// </summary>
    public partial class Elipse : Window
    {
        public Elipse()
        {
            InitializeComponent();
        }

        Ellipse elipse;
        private void cnvElipses_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point posicion = e.GetPosition(cnvElipses);
            Random r = new Random();
            elipse = new Ellipse
            {
                Width = 30,
                Height = 30,
                Fill = new SolidColorBrush(Color.FromRgb((byte)r.Next(0,256), (byte)r.Next(0,256), (byte)r.Next(0,256)))
            };
            cnvElipses.Children.Add(elipse);
            Canvas.SetLeft(elipse, posicion.X);
            Canvas.SetTop(elipse, posicion.Y);


            DoubleAnimation animacion = new DoubleAnimation
            {
                To = cnvElipses.ActualHeight - elipse.Height,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(2),
                EasingFunction=new BounceEase
                {
                    Bounces=5,
                    EasingMode=EasingMode.EaseOut
                }
                
            };

            elipse.BeginAnimation(Canvas.TopProperty, animacion);
        }

        private void cnvElipses_SizeChanged(object sender, SizeChangedEventArgs e)
        {
          if(elipse != null)
          {
            if (e.HeightChanged && cnvElipses.ActualHeight > e.PreviousSize.Height)
            {
                DoubleAnimation animacion = new DoubleAnimation
                {
                    To = cnvElipses.ActualHeight - elipse.Height,
                    BeginTime = TimeSpan.FromSeconds(0),
                    Duration = TimeSpan.FromSeconds(2),
                    EasingFunction = new BounceEase
                    {
                        Bounces = 5,
                        EasingMode = EasingMode.EaseOut
                    }

                };

                foreach (var el in cnvElipses.Children)
                {
                    if(el is Ellipse)
                    {
                        ((Ellipse)el).BeginAnimation(Canvas.TopProperty, animacion);
                    }
                }
            }

             if(e.HeightChanged&& cnvElipses.ActualHeight < e.PreviousSize.Height)
                {
                    DoubleAnimation animacion = new DoubleAnimation
                    {
                        To = cnvElipses.ActualHeight - elipse.Height,
                        BeginTime = TimeSpan.FromSeconds(0),
                        Duration = TimeSpan.FromSeconds(0)
                    };

                    foreach (var el in cnvElipses.Children)
                    {
                        if (el is Ellipse)
                        {
                            ((Ellipse)el).BeginAnimation(Canvas.TopProperty, animacion);
                        }
                    }

                }
          }
        }
    }
}
