using tabuleiro;
using xadrez_console.Entities.xadrez;

namespace xadrez_console
{
    internal class Tela
    {

        public static void ImprimeTabuleiro(Tabuleiro tabuleiro)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine("===============================");
            Console.WriteLine("            XADREZ             ");
            Console.WriteLine("===============================");
            Console.WriteLine();

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("     ==========================");
            Console.WriteLine("       a  b  c  d  e  f  g  h ");
            Console.WriteLine("     ==========================");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca.Cor == Cor.Preta)
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }
}
