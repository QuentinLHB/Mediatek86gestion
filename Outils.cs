using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mediatek86
{
    public static class Outils
    {

        /// <summary>
        /// Ouvre une fenêtre de sélection d'image locale.
        /// </summary>
        /// <returns>Chemin de l'image sélectionnée par l'utilisateur.</returns>
        public static string selectionnerImageLocale()
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            return filePath;
        }
    }
}
