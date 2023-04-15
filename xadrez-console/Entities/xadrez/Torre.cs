using tabuleiro;

namespace xadrez
{
    internal class Torre : Peca
    {
        public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override bool[,] MovimentosPossiveis() {

            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            // acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.GetPeca(pos) != null && Tabuleiro.GetPeca(pos).Cor != this.Cor)
                    break;

                pos.Linha = pos.Linha - 1;
            }

            // abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.GetPeca(pos) != null && Tabuleiro.GetPeca(pos).Cor != this.Cor)
                    break;

                pos.Linha = pos.Linha + 1;
            }

            // direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.GetPeca(pos) != null && Tabuleiro.GetPeca(pos).Cor != this.Cor)
                    break;

                pos.Coluna = pos.Coluna + 1;
            }

            // esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                matriz[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.GetPeca(pos) != null && Tabuleiro.GetPeca(pos).Cor != this.Cor)
                    break;

                pos.Coluna = pos.Coluna - 1;
            }

            return matriz;
        }

        public override bool PodeMover(Posicao posicao) {
            Peca p = Tabuleiro.GetPeca(posicao);
            return p == null || p.Cor != this.Cor;
        }

        public override string ToString() {
            return "T";
        }
    }
}
