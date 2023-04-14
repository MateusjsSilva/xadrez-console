using tabuleiro;
using tabuleiro.exceptions;
using xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args) {

            PartidaXadrez partidaXadrez = new PartidaXadrez();

            while (!partidaXadrez.Terminada)
            {
                try
                {
                    Tela.ImprimirPartida(partidaXadrez);

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().toPosicao();
                    partidaXadrez.ValidarPosicaoDeOrigem(origem);

                    bool[,] posicoesPossiveis = partidaXadrez.Tabuleiro.GetPeca(origem).MovimentosPossiveis();

                    Tela.ImprimirPartida(partidaXadrez, posicoesPossiveis);

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().toPosicao();
                    partidaXadrez.ValidarPosicaoDeDestino(origem, destino);

                    partidaXadrez.RealizaJogada(origem, destino);
                }
                catch (TabuleiroException e)
                {
                    Console.WriteLine($"Error: {e.Message} Pressione [ENTER] para continuar!");
                    Console.ReadLine();
                }
            }

        }
    }
}