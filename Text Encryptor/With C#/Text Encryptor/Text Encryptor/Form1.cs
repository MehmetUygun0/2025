using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Text_Encryptor
{
    public partial class Form1: Form
    {
        static char[] main_characters =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'ç', 'd', 'e', 'f', 'g', 'ğ', 'h', 'ı', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'ö', 'p', 'q', 'r', 's', 'ş', 't', 'u', 'ü', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'Ç', 'D', 'E', 'F', 'G', 'Ğ', 'H', 'İ', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'Ö', 'P', 'Q', 'R', 'S', 'Ş', 'T', 'U', 'Ü', 'V', 'W', 'X', 'Y', 'Z',
            '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~', ' '
        };
        Random random = new Random();
        char[] characters;
        string code = "";
        string code2 = "";
        int codeInt;
        int code2Int;
        string cipherText = "";
        string CorrectedText = "";
        string text = "";
        void CodeMakeSuitable()
        {
            codeInt = Math.Abs(code.GetHashCode());
            code2Int = Math.Abs(code2.GetHashCode());
        }
        void Code2Create()
        {
            code2 = "";
            random = new Random(codeInt);
            for (int i = 0; i < 11; i++)
            {
                int numberPower = (int)Math.Pow(int.Parse(code[i].ToString()), 3);
                int newNumber = numberPower % random.Next(1, 10);
                code2+=newNumber; 
            }
            code2 = code2.TrimStart('0');
            CodeMakeSuitable();
            
        }
        void CodeCreate()
        {
            code = "";
            for (int i = 0; i < 11; i++)
            {
                code += random.Next(1, 10);
            }
            int singles = Enumerable.Range(0, 9).Where(i => i % 2 == 0).Sum(i => int.Parse(code[i].ToString()));
            int doubles = Enumerable.Range(1, 8).Where(i => i % 2 == 1).Sum(i => int.Parse(code[i].ToString()));
            int total = Enumerable.Range(0, 10).Sum(i => int.Parse(code[i].ToString()));
            if ((singles * 7 - doubles) % 10 == int.Parse(code[9].ToString()) && total % 10 == int.Parse(code[10].ToString()))
            {
                CodeMakeSuitable();
                Code2Create();
            }
            else
            {
                code = "";
                CodeCreate();
            }
            label1.Text= code;
        }
        void CharactersMix()
        {
            characters = (char[])main_characters.Clone();
            random = new Random(code2Int);
            for (int i = 0; i < characters.Length; i++)
            {
                int index = random.Next(0, 95);
                char temp = characters[i];
                characters[i] = characters[index];
                characters[index] = temp;
            }
        }
        void Mix()
        {
            cipherText = "";
            List<int> indices = new List<int>();
            foreach (var item in text)
            {
                indices.Add(Array.IndexOf(main_characters, item));
            }
            foreach (var item in indices)
            {
                cipherText += characters[item];
            }

            random = new Random(codeInt);
            char[] cipherTextArray = cipherText.ToCharArray();
            for (int i = 0; i < cipherText.Length; i++)
            {
                int index = random.Next(0, cipherText.Length);
                char temp = cipherTextArray[i];
                cipherTextArray[i] = cipherTextArray[index];
                cipherTextArray[index] = temp;
            }
            cipherText = new string(cipherTextArray);
        }

        void Arrange()
        {
            cipherText = richTextBox2.Text;
            random = new Random(codeInt);
            List<List<int>> swaps = new List<List<int>>();
            for (int i = 0; i < cipherText.Length; i++)
            {
                int index = random.Next(0, cipherText.Length);
                swaps.Add(new List<int> { i, index });
            }

            char[] cipherTextArray = cipherText.ToCharArray();
            for (int i = swaps.Count - 1; i >= 0; i--)
            {
                int index1 = swaps[i][0];
                int index2 = swaps[i][1];
                (cipherTextArray[index1], cipherTextArray[index2]) = (cipherTextArray[index2], cipherTextArray[index1]);
            }
            cipherText = new string(cipherTextArray);

            List<int> indices = new List<int>();
            foreach (var item in cipherText)
            {
                indices.Add(Array.IndexOf(characters, item));
            }

            CorrectedText = "";
            for (int i = 0; i < indices.Count; i++)
            {
                CorrectedText += main_characters[indices[i]];
            }
        }
        public Form1()
        {
            InitializeComponent();
            characters = (char[])main_characters.Clone();
            MaximizeBox = false;
        }

        private void decode_Click(object sender, EventArgs e)
        {
            text = richTextBox1.Text;
            if (text != "")
            {
                code = textBox1.Text;
                CodeMakeSuitable();
                Code2Create();
                CharactersMix();
                Arrange();
                richTextBox2.Text = CorrectedText;
            }
            else 
                MessageBox.Show("Cannot be left blank", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void encrypt_Click(object sender, EventArgs e)
        {
            text = richTextBox1.Text;
            if (text!= "")
            {
                CodeCreate();
                CharactersMix();
                Mix();
            }
            else
                MessageBox.Show("Cannot be left blank", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            richTextBox1.Text = cipherText;
        }

        private void copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(cipherText==""?"NULL":cipherText);
            Confirmation((Button)sender);
        }

        private void copyCode_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label1.Text);
            Confirmation((Button)sender);
        }

        private void copyDecode_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(CorrectedText == "" ? "NULL" : CorrectedText);
            Confirmation((Button)sender);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        public async void Confirmation(Button btn)
        {
            btn.BackColor = Color.Green;
            await Task.Delay(750);
            btn.BackColor = SystemColors.Control;
        }
    }
}
