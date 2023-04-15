using tabuleiro;

namespace xadrez
{
    internal class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

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

            return matriz;
        }

        public override bool PodeMover(Posicao posicao) {
            Peca p = Tabuleiro.GetPeca(posicao);
            return p == null || p.Cor != this.Cor;
        }

        public override string ToString() {
            return "R";
        }
    }
}
