using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO; //Input Output

namespace FoldersManagement
{
    
    public partial class Form1 : Form
    {
        FileStream fileStream, fileStreamTemp;
        StreamReader streamReader;
        StreamWriter streamWriter;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            fileStream = new FileStream(@"D:\fichier1.txt", FileMode.Append, FileAccess.Write); //Append = Ajouter
            streamWriter = new StreamWriter(fileStream);
            string ligne = txtCIN.Text + " - " + txtNom.Text + " - " + txtPrenom.Text +
                            " - " + txtSalaire.Text + " Dhs. ";
            streamWriter.WriteLine(ligne);
            streamWriter.Close();
            fileStream.Close();
            MessageBox.Show("Ajout effecuté avec succès !");

            txtCIN.Clear();
            txtNom.Clear();
            txtPrenom.Clear();
            txtSalaire.Clear();
        }

        private void btnAfficher_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (File.Exists(@"D:\fichier1.txt"))
            {
                fileStream = new FileStream(@"D:\fichier1.txt", FileMode.Open, FileAccess.Read);
                streamReader = new StreamReader(fileStream);
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] table = line.Split('-');
                    listView1.Items.Add(new ListViewItem (table)); //String [] { table[0], table[1], table[2], table[3] }));
                    line = streamReader.ReadLine();

                }
                streamReader.Close();
                fileStream.Close(); 
            }
            else MessageBox.Show("Fichier inexistant !");


        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            fileStream = new FileStream(@"D:\fichier1.txt", FileMode.Open, FileAccess.Read);
            fileStreamTemp = new FileStream(@"D:\fichierTemp.txt", FileMode.Append, FileAccess.Write);
            streamReader = new StreamReader(fileStream);
            streamWriter = new StreamWriter(fileStreamTemp);
            bool supprime = false;
            string ligne = streamReader.ReadLine();

            while (ligne != null)
            {
                string[] table = ligne.Split('-');
                if (txtCIN.Text == table[0]) streamWriter.WriteLine(ligne);
                else
                {
                    supprime = true;
                    ligne = streamReader.ReadLine();
                }
            }
            streamWriter.Close();
            streamReader.Close();
            fileStream.Close();
            fileStreamTemp.Close();
            File.Delete(@"D:\fichier1.txt");
            File.Move(@"D:\fichierTemp.txt", @"D:\fichier1.txt");

            if (supprime == false)       MessageBox.Show("Employé inexistant !");
            else            MessageBox.Show("Employé supprimé avec succès !");
            
        }
    }
}
