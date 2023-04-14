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
                Posicao posicao = new Posicao(1, 3);

                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.AddPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.AddPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                //tab.AddPeca(new Rei(tab, Cor.Preta), new Posicao(6, 9));

                PosicaoXadrez posicaoXadrez = new PosicaoXadrez('c', 7);

                Console.WriteLine(posicaoXadrez);
                Console.WriteLine(posicaoXadrez.toPosicao());

                Tela.ImprimeTabuleiro(tab);
            }catch (TabuleiroException e )
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}