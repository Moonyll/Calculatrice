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
        //Déclaration d'une liste d'opérations numériques possibles
        private enum Operations
        {
        None, // Rien
        Division, // Diviser
        Multiplication, // Multiplier
        Subtraction, // Soustraire
        Sum // Additionner
        }
        //Gestion des boutons
        private void Number_Button(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (calculation) //Si le calcul est déj effectué
            {
                result.Content = $"{button.Content}"; // Récupération de la valeur entrée par l'utilisateur
                calculation = false;
            }
            else // Calcul non effectué
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
        private void Operation(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            //Cas selon le boutton choisi 
            switch (button.Content.ToString()) // Conversion de la valeur du boutton en chaîne de caractères
            {
                case "Reset": // Réinitialiser la calcultrice
                    result.Content = "0";
                    break;
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
                    switch (operation) // Cas des opérations
                    {
                        case Operations.Division: // Division
                            if (calculation)
                            {
                                if (!(result.Content.ToString() == "ERROR"))
                                {
                                    result.Content = Convert.ToDouble(result.Content) / secondNumber;
                                }
                            }
                            else
                            {
                                // Cas de la dividion par 0 !!!
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
                        case Operations.Multiplication: // Multiplication
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
                        case Operations.Subtraction: // Soustraction
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
                        case Operations.Sum: // Addition
                            if (calculation)
                            {
                                result.Content = Convert.ToDouble(result.Content) + secondNumber;
                            }
                            else
                            {
                                secondNumber = Convert.ToDouble(result.Content);
                                //MessageBox.Show($"{firstNumber} + {secondNumber}");
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
