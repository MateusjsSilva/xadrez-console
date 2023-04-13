using tabuleiro;
using xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Posicao posicao = new Posicao(1, 3);

            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.AddPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
            tab.AddPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
            tab.AddPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));

            Tela.ImprimeTabuleiro(tab);

        }
    }
}