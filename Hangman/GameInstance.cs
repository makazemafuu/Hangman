using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class GameInstance
    {

        private int healthPlayer { get; set; }

        //deux listes pour définir si nos caractères sont correctes ou incorrectes selon le mot
        //get = lecture & set = écriture
        //init; existe aussi : This proposal adds the concept of init only properties and indexers to C#.
        //These properties and indexers can be set at the point of object creation but become effectively get only once object creation has completed.
        //This allows for a much more flexible immutable model in C#.
        public List<char> playerGuesses { get; }

        //notre programme va piocher dans notre liste de mot aléatoirement pour faire deviner le joueur
        public List<char> playerMisses { get; }
        public List<Words> Word { get; }
        public Words WordToGuess { get; set; }

        //privé car on ne veut cette propriété uniquement dans notre GameInstance et non ailleurs
        private Random rnd;

        //on définit si le joueur à gagné ou non dans notre programm
        private bool playerWins { get; set; }

        //variable temporaire permettant de travailler sur les lettres que l'utilisateur à actuellement trouvé
        private string currentWordGuessed;

        //nombre d'erreurs possibles
        public GameInstance(int healthPlayer = 0)
        {
            //génère un chiffre aléatoire
            rnd = new Random();

            Word = new List<Words>
            {
                new Words("Programming"),
                new Words("Computer"),
                new Words("Application"),
                new Words("Sword"),
                new Words("Drawing"),
                new Words("Sushi"),
                new Words("Ramen"),
                new Words("Velociraptor"),
                new Words("Streamer"),
                new Words("Gamer"),
                new Words("Player")
            };

            //listes pour le moment vide tant que la partie n'a pas commencé
            playerGuesses = new List<char>();
            playerMisses = new List<char>();

            //Random.Next est une méthode pour renvoyer un int qui ne sera pas négative
            WordToGuess = Word[rnd.Next(0, Word.Count)];
            //on initialise notre propriété - par convention pour les variables privées on utilise une minuscule et une majuscule pour les variables publiques
            //ici, deux variables ont le même nom mais comme cette propriété appartient à la classe GameInstance "this." signifie que c'est la propriété de CET objet et non d'un autre
            healthPlayer = WordToGuess.Length + 5;
            this.healthPlayer = healthPlayer;

            //Console.WriteLine("The word to guess contains {0} letters", WordToGuess.Length);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"The word to guess contains {WordToGuess.Length} letters");
            Console.ResetColor();

        }

        //mettre List<Words> word en premier permet à l'utilisateur de ne pas saisir le nb d'erreurs
        //d'ailleurs il me semble que ce genre de paramètre ne peu pas être en premier lorsqu'il y a une valeur par défaut dans un autre paramètre
        public GameInstance(List<Words> word, int healthPlayer = 0)
        {
            //génère un chiffre aléatoire
            rnd = new Random();

            //on dit que notre liste sur notre objet est = au paramètre du constructeur
            Word = word;

            //listes pour le moment vide tant que la partie n'a pas commencé
            playerGuesses = new List<char>();
            playerMisses = new List<char>();

            WordToGuess = Word[rnd.Next(0, Word.Count)];

            //on initialise notre propriété - par convention pour les variables privées on utilise une minuscule et une majuscule pour les variables publiques
            //ici, deux variables ont le même nom mais comme cette propriété appartient à la classe GameInstance "this." signifie que c'est la propriété de CET objet et non d'un autre
            healthPlayer = WordToGuess.Length + 5;
            this.healthPlayer = healthPlayer;

            //Console.WriteLine("The word to guess contains {0} letters", WordToGuess.Length);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"The word to guess contains {WordToGuess.Length} letters");
            Console.ResetColor();

        }
        public void Play()
        {
            //demander à l'utilisateur de saisir une lettre
            //de base lorsqu'on définit pas de valeur pour un bool, il est par définition à l'état "false"

            while (!playerWins)

            {

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Type in a letter :");
                Console.ResetColor();
                //char pour 1 caractère, ToUpper pour mettre en majuscule
                //ReadKey pour lire la touche sur laquelle l'utilisateur à appuyé, en "true" pour ne pas afficher la touche appuyé
                //KeyChar qui renvoie donc un char
                char letter = char.ToUpper(Console.ReadKey(true).KeyChar);
                //on aurait pu faire directement avec la propriété WordToGuess.Text.IndexOf puis mettre le char letter mais c'est "plus lourd"
                //cette propriété permet de nous dire si la lettre est bien dans notre mot, sinon ça va nous renvoyer -1 (si c'est -1 c'est que la lettre n'existe pas dans ce mot)
                int letterIndex = WordToGuess.GetIndexOf(letter);

                Console.WriteLine(); //écrire du vide pour faire un espace

                //condition lorsqu'on a trouvé la lettre et lorsque non
                //mettre le debug ici car sinon ça ne précise pas quand la lettre n'est pas bonne le -1
                //Console.WriteLine($"[DEBUG] letterIndex : {letterIndex}");

                currentWordGuessed = PrintWordToGuess();

                if (letterIndex != -1)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Congratulations, you have found the letter : {letter}");
                    playerGuesses.Add(letter); //pour bien ajouter qu'on a trouvé la lettre
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Health :" + healthPlayer + "/{0}", WordToGuess.Length + 5);
                    Console.ResetColor();

                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"The letter {letter} isn't in the word to guess.");
                    playerMisses.Add(letter); //pour bien ajouter que cette lettre n'est pas dans le mot
                    Console.ResetColor();
                    healthPlayer--;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Health :" + healthPlayer + "/{0}", WordToGuess.Length + 5);
                    Console.ResetColor();
                }

                if (playerMisses.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Errors ({playerMisses.Count}) : {string.Join(", ", playerMisses)}");
                    Console.ResetColor();
                }

                currentWordGuessed = PrintWordToGuess();

                if (currentWordGuessed.IndexOf('_') == -1) //si on ne trouve pas de _ car le mot est complet
                {
                    playerWins = true;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Congratulations ! You've won the game !");
                    Console.ResetColor();
                    Console.ReadKey(true); //pour pas que le programme se ferme tout de suite mais une fois qu'on appuie sur une touche

                }

                //condition quand on a perdu
                if (healthPlayer == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Oh no... you've lost the game ! The word to guess was : " + WordToGuess.Text);
                    Console.ResetColor();
                    Console.ReadKey(true);

                    break; //pour annulé la boucle, puisque la condition se base uniquement sur la variable playerWins

                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Would you like to play again ? (press yes to continue and any key to quit)");
            Console.ResetColor();
            string strStay = Console.ReadLine();

            if (strStay == "yes")
            {
                playerWins = false;
                Reset();
                Play();
            }
        }

        private void Reset()
        {
            playerGuesses.Clear();
            playerMisses.Clear();
            rnd = new Random();
            WordToGuess = Word[rnd.Next(0, Word.Count)];
            healthPlayer = WordToGuess.Length + 5;

        }
        private string PrintWordToGuess()
        {
            string currentWordGuessed = ""; //retourne une chaîne de caractère

            for (int i = 0; i < WordToGuess.Length; i++)
            {
                //nous permet de récupérer le caractère du mot actuel, de 0 à la taille maximal du mot, ex : ramen, lorsque i sera 0 se sera R, si i est 1 se sera A, etc
                if (playerGuesses.Contains(WordToGuess.Text[i]))
                {
                    currentWordGuessed += WordToGuess.Text[i]; //on ajoute le caractère trouvé
                }
                else
                {
                    currentWordGuessed += "_"; //si le caractère n'est pas dans le mot
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(currentWordGuessed);
            Console.ResetColor();
            Console.WriteLine();

            //sert à définir les conditions de victoire
            return currentWordGuessed;

        }

    }
}
