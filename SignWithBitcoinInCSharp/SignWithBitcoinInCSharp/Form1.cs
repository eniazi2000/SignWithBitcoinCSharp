using NBitcoin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignWithBitcoinInCSharp
{
    public partial class Form1 : Form
    {
        public Key key;
        public Form1()
        {
            InitializeComponent();
        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            key = new Key();
            PublickKeyTxt.Text = key.GetAddress(ScriptPubKeyType.Legacy, Network.Main).ToString();
            PrivateKeyTxt.Text = key.GetBitcoinSecret(Network.Main).PrivateKey.ToHex();


        }

        private void button2_Click(object sender, EventArgs e)
        {

            var sign = key.Sign(uint256.Parse(ComputeSha256Hash(MessageTxt.Text)));
            string s = Encoding.Default.GetString(sign.ToDER());
            SignatureTxt.Text = s;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(key.PubKey.Verify(uint256.Parse(ComputeSha256Hash(MessageTxt.Text)), Encoding.Default.GetBytes(SignatureTxt.Text)).ToString());

        }
    }
}
