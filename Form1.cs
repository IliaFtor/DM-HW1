using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DM_HW1
{
    public partial class Form1 : Form
    {
        private static HashSet<string> set1 = new HashSet<string>();
        private static HashSet<string> set2 = new HashSet<string>();
        private static HashSet<string> universe = new HashSet<string>();
        public Form1()
        {
            InitializeComponent();
            ClearsForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputText = textBox1.Text.Trim();

            if (radioButton1.Checked)
            {
                if (!set1.Contains(inputText))
                {
                    set1.Add(inputText);
                    listBox1.Items.Add(inputText);
                }
                else
                {
                    MessageBox.Show("Этот элемент уже существует в множестве 1 ", "Повторение элемента");
                }
            }
            else if (radioButton2.Checked)
            {
                if (!set2.Contains(inputText))
                {
                    set2.Add(inputText);
                    listBox2.Items.Add(inputText);
                }
                else
                {
                    MessageBox.Show("Этот элемент уже существует в множестве 2", "Повторение элемента");
                }
            }
            universe.Clear();
            universe.UnionWith(set1);
            universe.UnionWith(set2);
            listBox3.Items.Clear();
            listBox3.Items.AddRange(universe.ToArray());
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            ClearsForm();
        }
        private void ClearsForm()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            textBox1.Text = "";
        }

        private void PowerA_Click(object sender, EventArgs e)
        {
            MessageBox.Show(listBox1.Items.Count.ToString());
        }

        private void PowerB_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            MessageBox.Show(listBox2.Items.Count.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int[] set1 = listBox1.Items.Cast<string>().Select(int.Parse).ToArray();
            int[] set2 = listBox2.Items.Cast<string>().Select(int.Parse).ToArray();
            MessageBox.Show((set1.Length == set1.Length).ToString());
        }
       /* static bool AreArraysEqual<T>(T[] arr1, T[] arr2)
        {
            if (arr1.Length != arr2.Length)
                return false;

            var set1 = new HashSet<T>(arr1);
            var set2 = new HashSet<T>(arr2);

            return set1.SetEquals(set2);
        }*/

        private void button6_Click(object sender, EventArgs e)
        {
            int[] set1 = listBox1.Items.Cast<string>().Select(int.Parse).ToArray();
            int[] set2 = listBox2.Items.Cast<string>().Select(int.Parse).ToArray();
            MessageBox.Show((set1.Length != set1.Length).ToString());
        }
        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(IsSubset(set1, set2).ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show(IsSubset(set2, set1).ToString());
        }
        private static bool IsSubset<T>(IEnumerable<T> setA, IEnumerable<T> setB)
        {
            return setA.All(elementA => setB.Contains(elementA));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string[] set1 = listBox1.Items.Cast<string>().ToArray();
            string[] set2 = listBox2.Items.Cast<string>().ToArray();
            MessageBox.Show(string.Join(", ", Intersection(set1, set2)));
        }
        static string[] Intersection(string[] set1, string[] set2)
        {
            HashSet<string> hashSetA = new HashSet<string>(set1);
            HashSet<string> hashSetB = new HashSet<string>(set2);
            hashSetA.IntersectWith(hashSetB);
            return hashSetA.ToArray();
        }


        private void button10_Click(object sender, EventArgs e)
        {
            string[] set1 = listBox1.Items.Cast<string>().ToArray();
            string[] set2 = listBox2.Items.Cast<string>().ToArray();
            MessageBox.Show(string.Join(", ", Union(set1, set2)));
        }
        static string[] Union(string[] setA, string[] setB)
        {
            HashSet<string> hashSetA = new HashSet<string>(setA);
            HashSet<string> hashSetB = new HashSet<string>(setB);

            hashSetA.UnionWith(hashSetB);

            return hashSetA.ToArray();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string result = string.Join(", ", Divide(set1, set2));
            MessageBox.Show(result);
        }
        static string[] Divide(HashSet<string> setA, HashSet<string> setB)
        {
            setA.ExceptWith(setB);
            return setA.ToArray();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string result = string.Join(", ", Divide(set2, set1));
            MessageBox.Show(result);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string result = string.Join(", ", Complement(set1));
            MessageBox.Show(result);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string result = string.Join(", ", Complement(set2));
            MessageBox.Show(result);
        }
        public static IEnumerable<string> Complement(HashSet<string> setA)
        {
            var complement = universe.Except(setA);
            return complement;
        }


        private void button12_Click(object sender, EventArgs e)
        {
            string[] result = SymmetricDifference(set1, set2);
            string resultString = string.Join(", ", result);
            MessageBox.Show(resultString);
        }
        static string[] SymmetricDifference(HashSet<string> setA, HashSet<string> setB)
        {
            HashSet<string> hashSetA = new HashSet<string>(setA);
            HashSet<string> hashSetB = new HashSet<string>(setB);

            hashSetA.SymmetricExceptWith(hashSetB);

            return hashSetA.ToArray();
        }

        /*Тут можно откулючить Mbox*/
        private bool isClosing = false;
        private Form movingForm;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClosing)
            {
                e.Cancel = true;

                movingForm = new Form();
                movingForm.Text = "Ну что попалься мышь";
                movingForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                movingForm.Size = new Size(400, 100);

                
                movingForm.FormClosed += MovingForm_FormClosed;

                Timer timer = new Timer();
                timer.Interval = 10; 
                timer.Tick += Timer_Tick;
                timer.Start();

                movingForm.Show();

                isClosing = true;
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (movingForm != null && !movingForm.IsDisposed)
            {
                Point cursorPosition = Cursor.Position;
                cursorPosition.Offset(-movingForm.Width / 2, -movingForm.Height / 2);
                movingForm.Location = cursorPosition;
            }
        }
        private void MovingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            isClosing = false;
            this.Close();
        }
    }
}
