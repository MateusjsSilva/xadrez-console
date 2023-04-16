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
                    Tela.ImprimirPartida(partidaXadrez, null);

                    Console.Write(" Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    partidaXadrez.ValidarPosicaoDeOrigem(origem);

                    Tela.ImprimirPartida(partidaXadrez, partidaXadrez.Tabuleiro.GetPeca(origem).MovimentosPossiveis());

                    Console.Write(" Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                    partidaXadrez.ValidarPosicaoDeDestino(origem, destino);

                    partidaXadrez.RealizaJogada(origem, destino);
                }
                catch (TabuleiroException e)
                {
                    Console.WriteLine($"Error: {e.Message} Pressione [ENTER] para continuar!");
                    Console.ReadLine();
                }
            }
            Tela.ImprimirPartida(partidaXadrez, null);
        }
    }
}