using tabuleiro;

namespace xadrez_console
{
    internal class Tela
    {

        public static void ImprimeTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("| " + (8 - i) + " | ");
      
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (tabuleiro.GetPeca(i, j) == null)
                    {
                        Console.ForegroundColor = aux;
                        Console.Write(" - ");
                    }
                    else
                    {
                        Console.Write(" ");
                        Tela.ImprimirPeca(tabuleiro.GetPeca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("     ==========================");
            Console.WriteLine("       a  b  c  d  e  f  g  h ");
            Console.WriteLine("     ==========================");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca.Cor == Cor.Preta)
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
