using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MVC.Properties;

namespace MVC
{
    public partial class Form1 : Form
    {
        Model abc;

        public Form1()
        {
            InitializeComponent();

            abc = new Model();
            abc.observes += new System.EventHandler(this.UpdateFromModel);

            abc.setA(Int32.Parse(Settings.Default["C"].ToString())%100);
            abc.setC(Int32.Parse(Settings.Default["C"].ToString())/100);
            //abc.setB(0);
        }

        private void num_inputA_ValueChanged(object sender, EventArgs e)
        {
            abc.setA(Decimal.ToInt32(num_inputA.Value));
        }

        private void num_inputB_ValueChanged(object sender, EventArgs e)
        {
            abc.setB(Decimal.ToInt32(num_inputB.Value));
        }

        private void num_inputC_ValueChanged(object sender, EventArgs e)
        {
            abc.setC(Decimal.ToInt32(num_inputC.Value));
        }

        private void tb_inputA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                abc.setA(Int32.Parse(tb_inputA.Text));
        }

        private void tb_inputB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                abc.setB(Int32.Parse(tb_inputB.Text));
        }

        private void tb_inputC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                abc.setC(Int32.Parse(tb_inputC.Text));
        }

        private void track_inputA_ValueChanged(object sender, EventArgs e)
        {
            abc.setA(track_inputA.Value);
        }

        private void track_inputB_ValueChanged(object sender, EventArgs e)
        {
            abc.setB(track_inputB.Value);
        }

        private void track_inputC_ValueChanged(object sender, EventArgs e)
        {
            abc.setC(track_inputC.Value);
        }

        private void UpdateFromModel(object sender, EventArgs e)
        {
            tb_inputA.Text = abc.getA().ToString();
            tb_inputB.Text = abc.getB().ToString();
            tb_inputC.Text = abc.getC().ToString();

            num_inputA.Value = abc.getA();
            num_inputB.Value = abc.getB();
            num_inputC.Value = abc.getC();

            track_inputA.Value = abc.getA();
            track_inputB.Value = abc.getB();
            track_inputC.Value = abc.getC();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default["C"] = abc.getC() * 100 + abc.getA();
            Settings.Default.Save();
        }
    }

    public class Model
    {
        private int A;
        private int B;
        private int C;

        public System.EventHandler observes;

        public void setA(int A)
        {
            if (A > 0 && A < 100)
            {
                if (A <= C)
                {
                    this.A = A;

                    if (B < A)
                    {
                        B = A;
                    }
                }
                else
                {
                    this.A = A;
                    C = A;
                    B = A;
                }
            }
            else if (A >= 100)
            {

                this.A = 100;
                C = 100;
                B = 100;
            }
            else if (A <= 0)
            {
                this.A = 0;
            }

            observes.Invoke(this, null);
        }

        public void setB(int B)
        {
            if (B >= A && B <= C)
            {
                this.B = B;
            }

            observes.Invoke(this, null);
        }

        public void setC(int C)
        {
            if (C > 0 && C < 100)
            {
                if (C >= A)
                {
                    this.C = C;
                    if (B > C)
                    {
                        B = C;
                    }
                }
                else
                {
                    this.C = C;
                    A = C;
                    B = C;
                }

            }
            else if (C >= 100)
            {
                this.C = 100;
            }
            else if (C <= 0)
            {
                this.C = 0;
                B = 0;
                A = 0;
            }

            observes.Invoke(this, null);
        }

        public int getA()
        {
            return A;
        }

        public int getB()
        {
            return B;
        }

        public int getC()
        {
            return C;
        }
    }
}
