using tabuleiro;
using xadrez;

namespace xadrez_console
{
    internal class Tela
    {

        public static void ImprimirPartida(PartidaXadrez partidaXadrez) {
            Console.Clear();
            ImprimeTabuleiro(partidaXadrez.Tabuleiro);
            ImprimirPecasCapturadas(partidaXadrez);
            Console.WriteLine("----------------------------------------");
           
            if (!partidaXadrez.Terminada)
            {
                Console.WriteLine($"Turno: {partidaXadrez.Turno}     Aguardando: {partidaXadrez.JogadorAtual}");
                Console.ForegroundColor = ConsoleColor.Red;
                if (partidaXadrez.Xeque) Console.WriteLine("Você está em XEQUE!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Turno: {partidaXadrez.Turno} XEQUEMATE!  Vencedor: {partidaXadrez.JogadorAtual}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine("----------------------------------------");
        }

        public static void ImprimirPartida(PartidaXadrez partidaXadrez, bool[,] posicoesPossiveis) {
            Console.Clear();
            ImprimeTabuleiro(partidaXadrez.Tabuleiro, posicoesPossiveis);
            ImprimirPecasCapturadas(partidaXadrez);
            Console.WriteLine("----------------------------------------");

            if (!partidaXadrez.Terminada)
            {
                Console.WriteLine($"Turno: {partidaXadrez.Turno}     Aguardando: {partidaXadrez.JogadorAtual}");
                Console.ForegroundColor = ConsoleColor.Red;
                if (partidaXadrez.Xeque) Console.WriteLine("Você está em XEQUE!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Turno: {partidaXadrez.Turno} XEQUEMATE!  Vencedor: {partidaXadrez.JogadorAtual}");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.WriteLine("----------------------------------------");
        }

        private static void ImprimirPecasCapturadas(PartidaXadrez partida) {
            Console.WriteLine("----------------------------------------");
            Console.Write("Branca: ");
            Console.ForegroundColor = ConsoleColor.Green;
            imprimirConjunto(partida.GetCapturadas(Cor.Branca));

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Pretas: ");
            Console.ForegroundColor = ConsoleColor.Red;
            imprimirConjunto(partida.GetCapturadas(Cor.Preta));
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void imprimirConjunto(HashSet<Peca> pecas) {
            Console.Write("[");
            foreach (Peca peca in pecas)
            {
                Console.Write(peca + ",");
            }
            Console.WriteLine("]");
        }

        public static void ImprimeTabuleiro(Tabuleiro tabuleiro) {

            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine("----------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                XADREZ             ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("       " + (8 - i) + " ");

                for (int j = 0; j < tabuleiro.Colunas; j++)
                    Tela.ImprimirPeca(tabuleiro.GetPeca(i, j));

                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("          a  b  c  d  e  f  g  h");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void ImprimeTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis) {


            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine("----------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                XADREZ             ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkMagenta;

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.BackgroundColor = fundoOriginal;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("       " + (8 - i) + " ");

                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        if(tabuleiro.ExistePeca(new Posicao(i, j)))
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.BackgroundColor = fundoAlterado;
                        }
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
            Console.WriteLine("          a  b  c  d  e  f  g  h");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static PosicaoXadrez LerPosicaoXadrez() {
            try
            {
                string s = Console.ReadLine();
                char coluna = s[0];
                int linha = int.Parse(s[1] + "");

                return new PosicaoXadrez(coluna, linha);
            }
            catch(Exception e)
            {
                Console.WriteLine("Você inseriu um dado inválido!");
            }
            return null;
        }

        public static void ImprimirPeca(Peca peca) {
            if (peca == null)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" - ");
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.Write(" ");
                if (peca.Cor == Cor.Preta)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(peca);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(peca);
                }
                Console.ForegroundColor = aux;
                Console.Write(" ");
            }
        }
    }
}
