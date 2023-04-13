using tabuleiro;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Posicao posicao = new Posicao(1, 3);

            Tabuleiro tab = new Tabuleiro(40, 10);

            Tela.ImprimeTabuleiro(tab);

        }
    }
}