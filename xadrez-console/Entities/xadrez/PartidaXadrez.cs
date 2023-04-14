using tabuleiro;
using tabuleiro.exceptions;
using xadrez_console.Entities.xadrez;

namespace xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
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

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao posicao)
        {
            if(Tabuleiro.GetPeca(posicao) == null)
            {
                throw new TabuleiroException("Não existe peça na posição escolhida!");
            }

            if(JogadorAtual != Tabuleiro.GetPeca(posicao).Cor)
            {
                throw new TabuleiroException("A peça escolhida não é sua!");
            }

            if (!Tabuleiro.GetPeca(posicao).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não é possivel mover a peça escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.GetPeca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }
    }
}
