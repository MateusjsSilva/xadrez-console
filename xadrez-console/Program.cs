using tabuleiro;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Posicao posicao = new Posicao(1, 3);

            Console.WriteLine("Posição: " + posicao);
        }
    }
}