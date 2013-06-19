using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace psEncrypter
{
    public partial class psEncrypter : Form
    {
        public psEncrypter()
        {
            InitializeComponent();
        }

        private void encrypt()
        {
            string pass = this.txtPassword.Text==""?"AlphaBravo72*":this.txtPassword.Text;
            string cipher = "";
            string plainText = this.txtPlainText.Text;

            if(plainText == "")
                return;
                
            try
            {
                if(!plainText.Contains("EXPIRE="))
                {
                    plainText += "EXPIRE=" + DateTime.Now.AddDays(30);
                }
                cipher = DialAmerica.Utilities.Encryption.StringEncryption.Encrypt(plainText,pass);
            
                this.txtCipherText.Text = cipher;
            }catch{shiver();}
        }

        private void decrypt()
        {
            string plainText = "";
            string pass = this.txtPassword.Text==""?"AlphaBravo72*":this.txtPassword.Text;
            string cipherText = this.txtCipherText.Text;

            if(cipherText == "")
                return;

            try
            {
                plainText = DialAmerica.Utilities.Encryption.StringEncryption.Decrypt(cipherText,pass);

                string expire = plainText.Substring(plainText.IndexOf("EXPIRE="),plainText.Length - plainText.IndexOf("EXPIRE="));
                DateTime exp;

                if(DateTime.TryParse(expire.Replace("EXPIRE=",""),out exp) && exp>DateTime.Now)
                {
                    this.txtPlainText.Text = plainText.Replace(expire,"");
                }else
                {
                    this.txtPlainText.Text = "Your message has expired";
                }

            }catch{shiver();}

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            encrypt();
            if(this.txtCipherText.Text.Length>0)
            {
                Clipboard.SetText(this.txtCipherText.Text);
                this.txtPlainText.Text = "";
                this.txtCipherText.Text = "";
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            this.txtCipherText.Text = Clipboard.GetText();
            decrypt();
        }

        private void shiver()
        {
            this.Left += -5;
            System.Threading.Thread.Sleep(20);
            this.Left += 5;
            System.Threading.Thread.Sleep(20);
        }
    }
}