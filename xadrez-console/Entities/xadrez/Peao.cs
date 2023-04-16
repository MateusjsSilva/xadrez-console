using tabuleiro;

namespace xadrez
{
    internal class Peao : Peca
    {
        private PartidaXadrez _partida;

        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaXadrez partida) : base(tabuleiro, cor) {
            this._partida = partida;
        }

        public override bool[,] MovimentosPossiveis() {

            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new(0, 0);

            if (Cor == Cor.Branca)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.IsPosicaoValida(pos) && Livre(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.IsPosicaoValida(pos) && Livre(pos) && QuantMovimentos == 0)
                    matriz[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.IsPosicaoValida(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.IsPosicaoValida(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // #jogada especial en passant
                if (Posicao.Linha == 3)
                {
                    Posicao esquerda = new(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.IsPosicaoValida(esquerda) && PodeMover(esquerda) && Tabuleiro.GetPeca(esquerda) == _partida.VulneravelEnPassant)
                    {
                        matriz[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.IsPosicaoValida(direita) && PodeMover(direita) && Tabuleiro.GetPeca(direita) == _partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.IsPosicaoValida(pos) && Livre(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.IsPosicaoValida(pos) && Livre(pos) && QuantMovimentos == 0)
                    matriz[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.IsPosicaoValida(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.IsPosicaoValida(pos) && PodeMover(pos))
                    matriz[pos.Linha, pos.Coluna] = true;

                // #jogada especial en passant
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tabuleiro.IsPosicaoValida(esquerda) && PodeMover(esquerda) && Tabuleiro.GetPeca(esquerda) == _partida.VulneravelEnPassant)
                    {
                        matriz[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tabuleiro.IsPosicaoValida(direita) && PodeMover(direita) && Tabuleiro.GetPeca(direita) == _partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return matriz;
        }

        private bool Livre(Posicao posicao) {
            return Tabuleiro.GetPeca(posicao) == null;
        }

        public override bool PodeMover(Posicao posicao) {
            Peca p = Tabuleiro.GetPeca(posicao);
            return p != null && p.Cor != Cor;
        }

        public override string ToString() {
            return "P";
        }
    }
}
