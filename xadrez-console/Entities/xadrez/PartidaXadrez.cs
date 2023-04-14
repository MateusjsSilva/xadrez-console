using tabuleiro;
using xadrez_console.Entities.xadrez;

namespace xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        public bool Terminada { get; private set; }

        public PartidaXadrez()
        {
            this.Tabuleiro = new Tabuleiro(8, 8);
            this.Turno = 1;
            this.JogadorAtual = Cor.Branca;
            this.Terminada = false;

            ColocarPecas();
        }

        private void ColocarPecas()
        {
            // Brancas
            Tabuleiro.AddPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('c', 1).toPosicao());
            Tabuleiro.AddPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('e', 1).toPosicao());
            Tabuleiro.AddPeca(new Rei(Tabuleiro, Cor.Branca), new PosicaoXadrez('d', 1).toPosicao());

            // Pretas
            Tabuleiro.AddPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('c', 8).toPosicao());
            Tabuleiro.AddPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('e', 8).toPosicao());
            Tabuleiro.AddPeca(new Rei(Tabuleiro, Cor.Preta), new PosicaoXadrez('d', 8).toPosicao());

        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.RemovePeca(origem);
            p.IncrementarQuantMovimentos();
            Peca pecaCapturada = Tabuleiro.RemovePeca(destino);
            Tabuleiro.AddPeca(p, destino);
        }
    }
}
