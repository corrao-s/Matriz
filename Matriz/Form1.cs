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
        private Point actual = new Point(0, 0);
        private int[,] M =
        {
            {2,0,1,0,0 },
            {0,0,1,0,0 },
            {0,0,1,0,0 },
            {0,0,1,0,0 },
            {0,0,0,0,0 },

        };
        public Form1()
        {
            InitializeComponent();
            Redibujar();
            //this.ActiveControl = this;
        }

        private void Redibujar()
        {
            SuspendLayout();
            this.Controls.Clear();
            for (int j = 0; j < size; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    this.Controls.Add(CrearLabel(i, j, M[j, i]));
                }
            }
            ResumeLayout();
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
                default:
                    label.BackColor = Color.White;
                    break;
            }

            return label;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Point final = new Point(0,0);
            if (e.KeyCode==Keys.Down)
            {
                final = new Point(actual.X, actual.Y + 1);
            }
            if (e.KeyCode == Keys.Up)
            {
                final = new Point(actual.X, actual.Y - 1);
            }
            if (e.KeyCode == Keys.Left)
            {
                final = new Point(actual.X-1, actual.Y);
            }
            if (e.KeyCode == Keys.Right)
            {
                final = new Point(actual.X+1, actual.Y);
            }

            if (M[final.Y, final.X] == 0)
            {
                M[actual.Y, actual.X] = 0;
                M[final.Y, final.X] = 2;
                actual = final;
            }
            Redibujar();
        }
    }
}
