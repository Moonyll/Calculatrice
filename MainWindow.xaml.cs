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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace Calculatrice
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Déclaration des variables
        double firstNumber, secondNumber, resultNumber = 0;
        bool calculation = false;
        Operations operation = Operations.None;
        string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator; // Séparateur pour les nombres décimaux
        //
        public MainWindow()
        {
            InitializeComponent();

            //Assign to the decimal button the separator from the current culture
            //dec.Content = separator;
        }

        //List the possible numeric operations
        private enum Operations
        {
        None,
        Division,
        Multiplication,
        Subtraction,
        Sum
        }
        //Gestion des boutons
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (calculation) //Si le calcul est déj effectué
            {
                result.Content = $"{button.Content}";
                calculation = false;
            }
            else // Calucl non effectué
            {
                if (result.Content.ToString() == "0")
                {
                    result.Content = $"{button.Content}";
                }
                else
                {
                    result.Content = $"{result.Content}{button.Content}";
                }
            }

        }

        //Gestion des Opérations
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            //Cas selon le boutton choisi 
            switch (button.Content.ToString()) // Conversion de la valeur du boutton en chaîne de caractères
            {
                case "Reset": // Réinitiliaser la calcultrice
                    result.Content = "0";
                    break;
                //case "+/-":
                //    if (!(result.Content.ToString() == "0"))
                //    {
                //        result.Content = Convert.ToDouble(result.Content) * -1;
                //    }
                //    break;
                //case "%":
                //    if (!(result.Content.ToString() == "0"))
                //    {
                //        result.Content = Convert.ToDouble(result.Content) / 100;
                //    }
                //    break;
                case "/": // Cas de la division
                    firstNumber = Convert.ToDouble(result.Content);
                    operation = Operations.Division;
                    result.Content = "0";
                    break;
                case "*": // Cas de la multiplication
                    firstNumber = Convert.ToDouble(result.Content);
                    operation = Operations.Multiplication;
                    result.Content = "0";
                    break;
                case "-": // Cas de la soustraction
                    firstNumber = Convert.ToDouble(result.Content);
                    operation = Operations.Subtraction;
                    result.Content = "0";
                    break;
                case "+": // Cas de l'addition
                    firstNumber = Convert.ToDouble(result.Content);
                    operation = Operations.Sum;
                    result.Content = "0";
                    break;
                case "=":
                    switch (operation)
                    {
                        case Operations.Division:
                            if (calculation)
                            {
                                if (!(result.Content.ToString() == "ERROR"))
                                {
                                    result.Content = Convert.ToDouble(result.Content) / secondNumber;
                                }
                            }
                            else
                            {
                                //Check if division by 0
                                if (result.Content.ToString() == "0")
                                {
                                    result.Content = "ERROR";
                                    calculation = true;
                                }
                                else
                                {
                                    secondNumber = Convert.ToDouble(result.Content);
                                    resultNumber = firstNumber / secondNumber;
                                    result.Content = resultNumber;
                                    calculation = true;
                                }
                            }
                            break;
                        case Operations.Multiplication:
                            if (calculation)
                            {
                                result.Content = Convert.ToDouble(result.Content) * secondNumber;
                            }
                            else
                            {
                                secondNumber = Convert.ToDouble(result.Content);
                                resultNumber = firstNumber * secondNumber;
                                result.Content = resultNumber;
                                calculation = true;
                            }
                            break;
                        case Operations.Subtraction:
                            if (calculation)
                            {
                                result.Content = Convert.ToDouble(result.Content) - secondNumber;
                            }
                            else
                            {
                                secondNumber = Convert.ToDouble(result.Content);
                                resultNumber = firstNumber - secondNumber;
                                result.Content = resultNumber;
                                calculation = true;
                            }
                            break;
                        case Operations.Sum:
                            if (calculation)
                            {
                                result.Content = Convert.ToDouble(result.Content) + secondNumber;
                            }
                            else
                            {
                                secondNumber = Convert.ToDouble(result.Content);
                                MessageBox.Show($"{firstNumber} + {secondNumber}");
                                resultNumber = firstNumber + secondNumber;
                                result.Content = resultNumber;
                                calculation = true;
                            }
                            break;
                    }
                    break;
                default:
                    if (!result.Content.ToString().Contains(separator))
                    {
                        result.Content = $"{result.Content}{button.Content}";
                    }
                    break;
            }
        }
    }
}
