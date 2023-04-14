using System.Runtime.Intrinsics.X86;
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
                    Tela.ImprimirPeca(tabuleiro.GetPeca(i, j));
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

        public static void ImprimeTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine("===============================");
            Console.WriteLine("            XADREZ             ");
            Console.WriteLine("===============================");
            Console.WriteLine();

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkMagenta;

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.BackgroundColor = fundoOriginal;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("| " + (8 - i) + " | ");

                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    Tela.ImprimirPeca(tabuleiro.GetPeca(i, j));
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = fundoOriginal;
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
            if (peca == null)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" - ");
            }
            else
            {
                Console.Write(" ");
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
                Console.Write(" ");
            }
        }
    }
}
