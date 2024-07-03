using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ScientificCalculator
{
    public partial class Form1 : Form
    {
        private double result = 0;
        private string currentOperator = "";
        private bool isOperatorClicked = false;
        private bool radiansMode = true;

        public Form1()
        {
            InitializeComponent();
        }


        // -------------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------------





        // --------------------------------------------------------------------------------------------------------
        private double EvaluateExpression(string expression)
        {
            var tokens = Tokenize(expression); 
            var rpn = ConvertToRPN(tokens); 
            return EvaluateRPN(rpn); 
        }



        //Tokenize (["(", "2", "+", "3", ")", "*", "2"])
        private List<string> Tokenize(string expression)
        {
            var tokens = new List<string>(); 
            int pos = 0; 

            while (pos < expression.Length) 
            {
                if (char.IsDigit(expression[pos]) || expression[pos] == '.') 
                {
                    string number = "";
                    while (pos < expression.Length && (char.IsDigit(expression[pos]) || expression[pos] == '.'))
                    {
                        number += expression[pos]; 
                        pos++; 
                    }
                    tokens.Add(number); 
                }
                else if ("+-*/()".Contains(expression[pos])) 
                {
                    tokens.Add(expression[pos].ToString()); 
                    pos++; 
                }
                else
                {
                    pos++; 
                }
            }

            return tokens; 
        }

        //ConvertToRPN (["2", "3", "+", "2", "*"])

        private List<string> ConvertToRPN(List<string> tokens)
        {
            var output = new List<string>();
            var operators = new Stack<string>(); 

            foreach (var token in tokens) 
            {
                if (double.TryParse(token, out _)) 
                {
                    output.Add(token); 
                }
                else if ("+-*/".Contains(token))
                {
                    while (operators.Count > 0 && Precedence(operators.Peek()) >= Precedence(token) && operators.Peek() != "(")
                    {
                        output.Add(operators.Pop()); 
                    }
                    operators.Push(token); 
                }
                else if (token == "(") 
                {
                    operators.Push(token); 
                }
                else if (token == ")") 
                {
                    while (operators.Count > 0 && operators.Peek() != "(") 
                    {
                        output.Add(operators.Pop()); 
                    }
                    if (operators.Count > 0 && operators.Peek() == "(")
                    {
                        operators.Pop(); 
                    }
                    else
                    {
                        throw new ArgumentException("Mismatched parentheses in the expression.");
                    }
                }
            }

            while (operators.Count > 0) 
            {
                var op = operators.Pop();
                if (op == "(" || op == ")")
                {
                    throw new ArgumentException("Mismatched parentheses in the expression.");
                }
                output.Add(op);
            }

            return output; 
        }




        //EVALUETE RPN 

        private double EvaluateRPN(List<string> rpn) {
            var stack = new  Stack<double>(); 

            foreach (var token in rpn) 
            {
                if (double.TryParse(token, out double number)) 
                {
                    stack.Push(number); 
                }
                else if ("+-*/".Contains(token)) 
                {
                    if (stack.Count < 2)
                    {
                        throw  new InvalidOperationException("Invalid expression.");
                    }

                    double b = stack.Pop(); 
                    double a = stack.Pop(); 

                    switch (token) 
                    {
                        case "+": stack.Push(a + b); break;
                        case " -": stack.Push(a - b); break;
                        case "*": stack.Push(a * b); break;
                        case "/": stack.Push(a / b); break;
                        default: throw new InvalidOperationException("Invalid operator.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Invalid token.");
                }
            }

            if (stack.Count != 1)
            {
                throw new InvalidOperationException("Invalid expression.");
            }

            return stack.Pop(); 
        }


        //prioritize 
        private int Precedence(string op)
        {
            return op == "+" || op == "-" ? 1 : 2; 
        }


        // -------------------------------------------------------------------------------------------
        // -------------------------------------------------------------------------------------------









        private void button1_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "9";
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "0";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "+";
        }

        private void buttonMin_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "-";
        }

        private void buttonDiv_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "/";
        }

        private void buttonMulti_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "*";
        }

        private void buttonCloseBracket_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += ")";
        }

        private void buttonOpenBracket_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text += "(";
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            if (!textBoxDisplay.Text.Contains("."))
            {
                textBoxDisplay.Text += ".";
            }
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            string input = textBoxDisplay.Text;
            double result = EvaluateExpression(input);
            textBoxDisplay.Text = result.ToString();
            isOperatorClicked = false;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Clear();
            result = 0;
            currentOperator = "";
            isOperatorClicked = false;
        }

        private void buttonNegate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxDisplay.Text))
            {
                double value = double.Parse(textBoxDisplay.Text);
                value = -value;
                textBoxDisplay.Text = value.ToString();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxDisplay.Text))
            {
                textBoxDisplay.Text = textBoxDisplay.Text.Substring(0, textBoxDisplay.Text.Length - 1);
            }
        }

        private void buttonPower_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxDisplay.Text))
            {
                double value = double.Parse(textBoxDisplay.Text);
                value = Math.Pow(value, 2);
                textBoxDisplay.Text = value.ToString();
            }
        }

        private void buttonFactorial_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxDisplay.Text))
            {
                int value = int.Parse(textBoxDisplay.Text);
                textBoxDisplay.Text = Factorial(value).ToString();
            }
        }

        private int Factorial(int n)
        {
            if (n <= 1)
                return 1;
            else
                return n * Factorial(n - 1);
        }

        private void buttonInverse_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxDisplay.Text))
            {
                double value = double.Parse(textBoxDisplay.Text);
                if (value != 0)
                {
                    value = 1 / value;
                    textBoxDisplay.Text = value.ToString();
                }
                else
                {
                    MessageBox.Show("ERROR");
                }
            }
        }

        private void buttonLn_Click(object sender, EventArgs e)
        {
            double operand = double.Parse(textBoxDisplay.Text);

            if (operand > 0)
            {
                double result = Math.Log(operand);
                textBoxDisplay.Text = result.ToString();
            }
            else
            {
                MessageBox.Show("The natural logarithm can only be calculated for positive numbers!!");
            }
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            double operand = double.Parse(textBoxDisplay.Text);

            if (operand > 0)
            {
                double result = Math.Log10(operand);
                textBoxDisplay.Text = result.ToString();
            }
            else
            {
                MessageBox.Show("The decimal logarithm can only be calculated for positive numbers!!");
            }
        }

        // Sin, Cos, Tg, Ctg
        private double GetOperand()
        {
            double operand = 0.0;
            if (double.TryParse(textBoxDisplay.Text, out operand))
            {
                return operand;
            }
            else
            {
                return 0.0;
            }
        }

        private void buttonSin_Click(object sender, EventArgs e)
        {
            double degrees = GetOperand();
            double radians = degrees * Math.PI / 180.0;
            double result = Math.Sin(radians);
            textBoxDisplay.Text = result.ToString();
        }

        private void buttonCos_Click(object sender, EventArgs e){
            double degrees = GetOperand();
            double radians = degrees * Math.PI / 180.0;
            double result = Math.Cos(radians);
            textBoxDisplay.Text = result.ToString();
        }

        private void buttonTan_Click(object sender, EventArgs e){
            double degrees = GetOperand();
            double radians = degrees * Math.PI / 180.0;
            double result = Math.Tan(radians);
            textBoxDisplay.Text = result.ToString();
        }

        private void buttonCtg_Click(object sender, EventArgs e)
        {
            double degrees = GetOperand();
            double radians = degrees * Math.PI / 180.0;
            double tanValue = Math.Tan(radians);

            if (tanValue != 0)
            {
                double result = 1 / tanValue;
                textBoxDisplay.Text = result.ToString();
            }
            else
            {
                MessageBox.Show("Cannot compute cotangent: tan(x) is zero.");
                textBoxDisplay.Text = "Error";
            }
        }

        // Scientific Notation
        private void ConvertToScientificNotation()
        {
            double currentValue;
            if (double.TryParse(textBoxDisplay.Text, out currentValue))
            {
                textBoxDisplay.Text = currentValue.ToString("0.######E+0");
            }
            else
            {
                MessageBox.Show("Invalid input! Please enter a valid number.");
            }
        }

        private void ScientificNotation_CheckedChanged(object sender, EventArgs e)
        {
            if (ScientificNotation.Checked)
            {
                ConvertToScientificNotation();
            }
            else
            {
                DisplayResult(result);
            }
        }

        private void DisplayResult(double value)
        {
            textBoxDisplay.Text = value.ToString();
        }

        // Memory
        private double memory = 0;

        private void buttonMPlus_Click(object sender, EventArgs e)
        {
            double currentValue;
            if (double.TryParse(textBoxDisplay.Text, out currentValue))
            {
                memory += currentValue;
                textBoxDisplay.Clear();
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }

        private void buttonMMinus_Click(object sender, EventArgs e)
        {
            double currentValue;
            if (double.TryParse(textBoxDisplay.Text, out currentValue))
            {
                memory -= currentValue;
            }
            else
            {
                MessageBox.Show("ERROR");
            }
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = memory.ToString();
        }

        private void buttonMC_Click(object sender, EventArgs e)
        {
            memory = 0;
        }

        private void textBoxDisplay_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
