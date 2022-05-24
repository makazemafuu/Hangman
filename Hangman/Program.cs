using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class MainClass
    {
        static void Main(string[] args)
        {
            List<Words> word = new List<Words>();
            word.Add(new Words("Admission"));
            word.Add(new Words("Thanks"));


            GameInstance game = new GameInstance();
            game.Play();

        }
    }
}
