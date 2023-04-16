using tabuleiro;
using tabuleiro.exceptions;
using xadrez;

namespace xadrez_console
{
    internal class Tela
    {
        public static void ImprimirPartida(PartidaXadrez partidaXadrez, bool[,] posicoesPossiveis) {

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("----------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                XADREZ             ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("----------------------------------------\n");

            ImprimeTabuleiro(partidaXadrez.Tabuleiro, posicoesPossiveis);
            ImprimirPecasCapturadas(partidaXadrez);

            if (!partidaXadrez.Terminada)
            {
                Console.WriteLine($" Turno: {partidaXadrez.Turno}     Aguardando: {partidaXadrez.JogadorAtual}");
                Console.ForegroundColor = ConsoleColor.Red;
                if (partidaXadrez.Xeque) Console.WriteLine("Você está em XEQUE!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" Turno: {partidaXadrez.Turno}    XEQUEMATE!  Vencedor: {partidaXadrez.JogadorAtual}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine("----------------------------------------");
        }

        public static void ImprimeTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis) {

            ConsoleColor fundoOriginal = ConsoleColor.Black;
            ConsoleColor fundo1 = ConsoleColor.Blue;
            ConsoleColor fundo2 = ConsoleColor.DarkBlue;

            byte[,] fundo =
            {
                {1,0,1,0,1,0,1,0},
                {0,1,0,1,0,1,0,1},
                {1,0,1,0,1,0,1,0},
                {0,1,0,1,0,1,0,1},
                {1,0,1,0,1,0,1,0},
                {0,1,0,1,0,1,0,1},
                {1,0,1,0,1,0,1,0},
                {0,1,0,1,0,1,0,1}
            };

            if (posicoesPossiveis != null)
            {
                for (int i = 0; i < tabuleiro.Linhas; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("       " + (8 - i) + " ");

                    for (int j = 0; j < tabuleiro.Colunas; j++)
                    {
                        if (posicoesPossiveis[i, j])
                        {
                            if (tabuleiro.ExistePeca(new Posicao(i, j)))
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                            else
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                        }
                        else
                        {
                            if (fundo[i, j] == 1)
                                Console.BackgroundColor = fundo1;
                            else
                                Console.BackgroundColor = fundo2;
                        }
                        ImprimirPeca(tabuleiro.GetPeca(i, j));
                        Console.BackgroundColor = fundoOriginal;
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                for (int i = 0; i < tabuleiro.Linhas; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("       " + (8 - i) + " ");

                    for (int j = 0; j < tabuleiro.Colunas; j++)
                    {
                        if (fundo[i, j] == 1)
                            Console.BackgroundColor = fundo1;
                        else
                            Console.BackgroundColor = fundo2;
                        ImprimirPeca(tabuleiro.GetPeca(i, j));
                    }
                    Console.WriteLine();
                    Console.BackgroundColor = fundoOriginal;
                }
            }

            Console.BackgroundColor = fundoOriginal;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("          a  b  c  d  e  f  g  h\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void ImprimirPecasCapturadas(PartidaXadrez partida) {
            Console.WriteLine("----------------------------------------");
            Console.Write(" Branca: ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.GetCapturadas(Cor.Branca));
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write(" Pretas: ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.GetCapturadas(Cor.Preta));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("----------------------------------------");
        }

        private static void ImprimirConjunto(HashSet<Peca> pecas) {
            bool mostrar = false;
            Console.Write("[");
            foreach (Peca peca in pecas)
            {
                if (mostrar)
                {
                    Console.Write("," + peca);
                }
                else
                {
                    Console.Write(peca);
                    mostrar = true;
                }
            }
            Console.WriteLine("]");
        }

        public static PosicaoXadrez LerPosicaoXadrez() {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca) {
            if (peca == null)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("   ");
            }
            else
            {
                if (peca.Cor == Cor.Preta)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" " + peca + " ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" " + peca + " ");
                }
            }
        }
    }
}
