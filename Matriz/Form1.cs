using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matriz
{
    public partial class Form1 : Form
    {
        private const int A = 20;
        private const int size = 5;
        private Jugador[] jugadores;
        private int[,] M =
        {
            {2,0,0,0,0 },
            {0,0,1,0,0 },
            {1,0,1,0,1 },
            {0,0,1,0,0 },
            {0,0,0,0,3 },

        };
        private Label[,] labels;
        public Form1()
        {
            labels = new Label[size,size];
            jugadores = new Jugador[2] { new Jugador() { Nombre = "1", Posicion = new Point(0, 0) }, new Jugador() { Nombre = "2", Posicion = new Point(4, 4) } };
            InitializeComponent();
            Dibujar();
            //this.ActiveControl = this;
        }

        private void Dibujar()
        {
            SuspendLayout();
            this.Controls.Clear();
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    labels[i, j] = CrearLabel(i, j, M[j, i]);
                    this.Controls.Add(labels[i, j]);
                }
            }
            ResumeLayout();
        }
        private void Redibujar(Point[] points)
        {
            foreach (Point p in points)
            {
                this.Controls.Remove(labels[p.X, p.Y]);
                labels[p.X, p.Y] = CrearLabel(p.X,p.Y, M[p.Y, p.X]);
                this.Controls.Add(labels[p.X, p.Y]);
            }

        }
        private Label CrearLabel(int j, int i, int value)
        {
        
            Label label = new Label();
            label.BorderStyle = BorderStyle.FixedSingle;
            label.Text = value.ToString();
            label.Location = new Point(A*j, A*i);
            label.Size = new Size(A, A);
            label.TextAlign = ContentAlignment.MiddleCenter;
            switch (value)
            {
                case 1:
                    label.BackColor = Color.Red;
                    break;
                case 2:
                    label.BackColor = Color.Green;
                    break;
                case 3:
                    label.BackColor = Color.Yellow;
                    break;
                case 4:
                    label.BackColor = Color.Black;
                    break;
                default:
                    label.BackColor = Color.White;
                    break;
            }

            return label;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    Mover(jugadores[0], Direccion.Arriba);
                    break;
                case Keys.Down:
                    Mover(jugadores[0], Direccion.Abajo);
                    break;
                case Keys.Left:
                    Mover(jugadores[0], Direccion.Izquierda);
                    break;
                case Keys.Right:
                    Mover(jugadores[0], Direccion.Derecha);
                    break;
                case Keys.Enter:
                    Atacar(jugadores[0], jugadores[1]);
                    break;

                case Keys.W:
                    Mover(jugadores[1], Direccion.Arriba);
                    break;
                case Keys.S:
                    Mover(jugadores[1], Direccion.Abajo);
                    break;
                case Keys.D:
                    Mover(jugadores[1], Direccion.Derecha);
                    break;
                case Keys.A:
                    Mover(jugadores[1], Direccion.Izquierda);
                    break;
                case Keys.F:
                    Atacar(jugadores[1], jugadores[0]);
                    break;
            }
        }
        private void Mover(Jugador j, Direccion d)
        {
            if (M[j.Posicion.Y, j.Posicion.X] != 4)
            {
                Point final = new Point(j.Posicion.X, j.Posicion.Y);
                switch (d)
                {
                    case Direccion.Arriba:
                        final = new Point(j.Posicion.X, j.Posicion.Y - 1);
                        break;
                    case Direccion.Abajo:
                        final = new Point(j.Posicion.X, j.Posicion.Y + 1);
                        break;
                    case Direccion.Derecha:
                        final = new Point(j.Posicion.X + 1, j.Posicion.Y);
                        break;
                    case Direccion.Izquierda:
                        final = new Point(j.Posicion.X - 1, j.Posicion.Y);
                        break;
                }
                if (final.Y >= 0 && final.Y < size && final.X >= 0 && final.X < size && M[final.Y, final.X] == 0)
                {
                    var aux = M[j.Posicion.Y, j.Posicion.X];
                    M[j.Posicion.Y, j.Posicion.X] = 0;
                    M[final.Y, final.X] = aux;
                    Redibujar(new Point[] { j.Posicion, final });
                    j.Posicion = final;
                }
            }
        }
        private void Atacar (Jugador Victimario, Jugador Victima)
        {
            if (Victimario.Distancia(Victima.Posicion) < 2)
            {
                M[Victima.Posicion.Y, Victima.Posicion.X] = 4;
                Redibujar(new Point[] { Victima.Posicion });
            }
        }
    }
    public class Jugador
    {
        public string Nombre { get; set; }
        public Point Posicion { get; set; }

        public double Distancia(Point p)
        {
            return Math.Sqrt(Math.Pow(p.X - this.Posicion.X, 2) + Math.Pow(p.Y - this.Posicion.Y, 2));
        }
    }
    public enum Direccion
    {
        Arriba, Abajo, Derecha, Izquierda
    }
}
