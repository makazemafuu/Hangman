using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    //classe ajouté pour créer la liste de mots; accessible à tous que se soit l'utilisateur de la classe le concepteur
    public class Words
    {
        //deux propriétés publiques
        public string Text { get; set; } //accessible en lecture et en écriture
        public int Length { get; } //que en lecture pour ne pas qu'un utilisateur puisse modifier la taille du texte, définie par la propriété Text
        public Words(string text)
        {
            Text = text.ToUpper(); //mettre en majuscule
            Length = text.Length;
        }

        //méthode pour récupérer l'index de notre caractère dans notre Text
        public int GetIndexOf(char letter)
        {
            return Text.IndexOf(letter); //pour trouver le caractère qu'on a tapé sur la console, si il est bien dans ce mot ou non
        }
    }
}
