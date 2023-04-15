using tabuleiro;

namespace xadrez
{
    internal class Rei : Peca
    {
        private PartidaXadrez partida;

        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaXadrez partida) : base(tabuleiro, cor) {
            this.partida = partida;
        }

        public override bool[,] MovimentosPossiveis() {

            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            // acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // ne
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // se
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // so
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // no
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            // #jogada especial roque
            if (QuantMovimentos == 0 && !partida.Xeque)
            {
                // #jogada especial roque pequeno
                Posicao posT1 = new(Posicao.Linha, Posicao.Coluna + 3);
                if (TesteTorreParaRoque(posT1))
                {
                    Posicao p1 = new(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new(Posicao.Linha, Posicao.Coluna + 2);
                    if(Tabuleiro.GetPeca(p1) == null && Tabuleiro.GetPeca(p2) == null)
                    {
                        matriz[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }

                // #jogada especial roque grande
                Posicao posT2 = new(Posicao.Linha, Posicao.Coluna - 4);
                if (TesteTorreParaRoque(posT2))
                {
                    Posicao p1 = new(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tabuleiro.GetPeca(p1) == null && Tabuleiro.GetPeca(p2) == null && Tabuleiro.GetPeca(p3) == null)
                    {
                        matriz[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }
            }

                return matriz;
        }

        public override bool PodeMover(Posicao posicao) {
            Peca p = Tabuleiro.GetPeca(posicao);
            return p == null || p.Cor != this.Cor;
        }

        public bool TesteTorreParaRoque(Posicao posicao) {
            Peca p = Tabuleiro.GetPeca(posicao);
            return p != null && p is Torre && p.Cor == Cor && p.QuantMovimentos == 0;
        }

        public override string ToString() {
            return "R";
        }
    }
}
