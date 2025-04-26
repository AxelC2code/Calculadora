using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//Enumeraremos las operaciones que soportará la calculadora
namespace CalculadoraProject
{
    public enum Operacion
    {
        NoDefinida = 0,
        Suma = 1,
        Resta = 2,
        Division = 3,
        Multiplicacion = 4,
        Modulo = 5,
        Seno = 6,
        Coseno = 7,
        Tangente = 8,
        Logaritmo = 9,
        Raiz = 10,
        Exponente = 11
    }

    public partial class Form1 : Form
    {
        // Definimos las variables que usaremos para almacenar los valores y el operador

        double valor1 = 0;      //primera variable
        double valor2 = 0;      //segunda variable
        Operacion operador = Operacion.NoDefinida;
        bool unNumeroLeido = false;

        public Form1()
        {
            InitializeComponent(); //se inicializa el formulario
        }

        // Método para leer un número y mostrarlo en la caja de resultado
        private void LeerNumero(string numero)
        {
            unNumeroLeido = true;
            if (cajaResultado.Text == "0" && cajaResultado.Text != null)
            {
                cajaResultado.Text = numero;
            }
            else
            {
                cajaResultado.Text += numero;
            }
        }

        /*
        private void btn1(object sender, EventArgs e)
        {
            if (cajaResultado.Text == "0" && cajaResultado.Text != null)
            {
                cajaResultado.Text = "1";
            }
            else
            {
                cajaResultado.Text += "1";
            }
         }
         */

        // Metodo para preparar la primera operacion (valor 1)
        private void ObtenerValor(string operador)
        {
            valor1 = Convert.ToDouble(cajaResultado.Text);
            lblHistorial.Text = cajaResultado.Text + operador;
            cajaResultado.Text = "0";   // Se limpia la caja y se prepara para el segundo valor
        }

        private void ObtenerValorUnario(string operadorTexto)
        {
            valor2 = Convert.ToDouble(cajaResultado.Text);
            lblHistorial.Text = operadorTexto + cajaResultado.Text + ")";
            cajaResultado.Text = "0";
            // Ejecutamos inmediatamente pues son operaciones unarias
            double resultado = EjecutarOperacion();
            cajaResultado.Text = Convert.ToString(resultado);
            unNumeroLeido = false;
            valor1 = 0;
            valor2 = 0;
        }

        private double EjecutarOperacion()
        {
            double resultado = 0;

            switch (operador)
            {
                case Operacion.Suma:
                    resultado = valor1 + valor2;
                    break;

                case Operacion.Resta:
                    resultado = valor1 - valor2;
                    break;

                case Operacion.Multiplicacion:
                    resultado = valor1 * valor2;
                    break;

                case Operacion.Division:
                    if (valor2 == 0)
                    {
                        lblHistorial.Text = "No se puede dividir entre 0";
                        resultado = 0;
                    }
                    else
                    {
                        resultado = valor1 / valor2;
                    }
                    break;

                case Operacion.Exponente:

                    // Exponente con base 0 y exponente 0 → indeterminado
                    if (valor1 == 0 && valor2 == 0)
                    {
                        lblHistorial.Text = "0^0 indeterminado";
                        resultado = 0;
                    }
                    // Base negativa con exponente no entero → resultado complejo
                    else if (valor1 < 0 && valor2 % 1 != 0)
                    {
                        lblHistorial.Text = "Exponente no entero en base negativa";
                        resultado = 0;
                    }
                    else
                    {
                        resultado = Math.Pow(valor1, valor2);
                    }
                    break;

                case Operacion.Modulo:
                    if (valor2 == 0)
                    {
                        lblHistorial.Text = "Módulo por cero no permitido";
                        resultado = 0;
                    }
                    else
                    {
                        resultado = valor1 % valor2;
                    }
                    break;

                case Operacion.Logaritmo:
                    if (valor1 <= 0 || valor1 == 1 || valor2 <= 0)
                    {
                        lblHistorial.Text = "Base/arg.log inválido";
                        resultado = 0;
                    }
                    else
                    {
                        resultado = Math.Log(valor2, valor1); 
                    }
                    break;

                case Operacion.Raiz:
                    if (valor1 == 0)
                    {
                        lblHistorial.Text = "Índice raíz no puede ser 0";
                        resultado = 0;
                    }
                    else if (valor2 < 0 && valor1 % 2 == 0)
                    {
                        lblHistorial.Text = "Raíz par de negativo";
                        resultado = 0;
                    }
                    else
                    {
                        resultado = Math.Pow(valor2, 1.0 / valor1); 
                    }
                    break;

                // Operaciones unarias
                // Coonversion a radianes de cada funcncion
                case Operacion.Seno:
                    resultado = Math.Sin(valor2 * Math.PI / 180);
                    break;

                case Operacion.Coseno:
                    resultado = Math.Cos(valor2 * Math.PI / 180);
                    break;
                case Operacion.Tangente:
                    resultado = Math.Tan(valor2 * Math.PI / 180);
                    break;

            }

            return resultado;
        }

        // Metodo para evitar el evento de ceros a la izquierda 
        private void btnCero_Click(object sender, EventArgs e)
        {
            unNumeroLeido = true;
            if (cajaResultado.Text == "0")
            {
                return;
            }
            else
            {
                cajaResultado.Text += "0";
            }
        }

        // Método para leer el número 1 al 9 y mostrarlo en la caja de resultado
        private void btnUno_Click(object sender, EventArgs e) => LeerNumero("1");
        private void btnDos_Click(object sender, EventArgs e) => LeerNumero("2");
        private void btnTres_Click(object sender, EventArgs e) => LeerNumero("3");
        private void btnCuatro_Click(object sender, EventArgs e) => LeerNumero("4");
        private void btnCinco_Click(object sender, EventArgs e) => LeerNumero("5");
        private void btnSeis_Click(object sender, EventArgs e) => LeerNumero("6");
        private void btnSiete_Click(object sender, EventArgs e) => LeerNumero("7");
        private void btnOcho_Click(object sender, EventArgs e) => LeerNumero("8");
        private void btnNueve_Click(object sender, EventArgs e) => LeerNumero("9");

        private void btnPunto_Click(object sender, EventArgs e)
        {
            if (cajaResultado.Text.Contains("."))
            {
                return;
            }
            cajaResultado.Text += ".";
        }

        private void btnSuma_Click(object sender, EventArgs e)
        {
            operador = Operacion.Suma;
            ObtenerValor(" + ");
        }

        private void btnResta_Click(object sender, EventArgs e)
        {
            operador = Operacion.Resta;
            ObtenerValor(" - ");
        }

        private void btnMultiplicar_Click(object sender, EventArgs e)
        {
            operador = Operacion.Multiplicacion;
            ObtenerValor(" x ");
        }

        private void btnDivision_Click(object sender, EventArgs e)
        {
            operador = Operacion.Division;
            ObtenerValor(" ÷ ");
        }

        private void btnExp_Click(object sender, EventArgs e)
        {
            operador = Operacion.Exponente;
            ObtenerValor("^");
        }

        private void btnModulo_Click(object sender, EventArgs e)
        {
            operador = Operacion.Modulo;
            ObtenerValor(" % ");
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            operador = Operacion.Logaritmo;
            ObtenerValor("log base ");
        }

        private void btnRaiz_Click(object sender, EventArgs e)
        {
            operador = Operacion.Raiz;
            ObtenerValor("raíz ");
        }

        private void btnSen_Click(object sender, EventArgs e)
        {
            operador = Operacion.Seno;
            ObtenerValorUnario("sin(");
        }

        private void btnCos_Click(object sender, EventArgs e)
        {
            operador = Operacion.Coseno;
            ObtenerValorUnario("cos(");
        }

        private void btnTan_Click(object sender, EventArgs e)
        {
            operador = Operacion.Tangente;
            ObtenerValorUnario("tan(");
        }

        private void btnResultado_Click(object sender, EventArgs e)
        {
            if (unNumeroLeido)
            {
                // Solo para operaciones binarias (que necesitan valor2)
                if (operador >= Operacion.Suma && operador <= Operacion.Exponente)
                {
                    valor2 = Convert.ToDouble(cajaResultado.Text);
                    lblHistorial.Text += valor2 + " =";
                }

                double resultado = EjecutarOperacion();
                cajaResultado.Text = Convert.ToString(resultado);
                unNumeroLeido = false;

                // Resetear solo para operaciones binarias
                if (operador >= Operacion.Suma && operador <= Operacion.Exponente)
                {
                    valor1 = 0;
                    valor2 = 0;
                }
            }
        }

        // Boton para limpiar la pantalla
        private void btnReset_Click(object sender, EventArgs e)
        {
            cajaResultado.Text = "0";
            lblHistorial.Text = "";
        }

        // Boton para borrar el ultimo digito
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (cajaResultado.Text.Length > 1)
            {
                string txtResultado = cajaResultado.Text;
                txtResultado = txtResultado.Substring(0, txtResultado.Length - 1);

                if (txtResultado.Length == 1 && txtResultado.Contains("-"))
                {
                    cajaResultado.Text = "0";
                }
                else
                {
                    cajaResultado.Text = txtResultado;
                }
            }
            else
            {
                cajaResultado.Text = "0";
            }
        } 
    }
}
