using tabuleiro;
using tabuleiro.exceptions;
using xadrez;
using xadrez_console.Entities.xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaXadrez partidaXadrez = new PartidaXadrez();

                while (!partidaXadrez.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimeTabuleiro(partidaXadrez.Tabuleiro);

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().toPosicao();

                    bool[,] posicoesPossiveis = partidaXadrez.Tabuleiro.GetPeca(origem).MovimentosPossiveis();

                    Console.Clear();
                    Tela.ImprimeTabuleiro(partidaXadrez.Tabuleiro, posicoesPossiveis);

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().toPosicao();

                    partidaXadrez.ExecutaMovimento(origem, destino);
                }
            }catch (TabuleiroException e )
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}