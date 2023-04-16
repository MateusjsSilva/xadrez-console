using tabuleiro;

namespace xadrez
{
    internal class Rainha : Peca
    {
        private bool[,] _matriz;
        private Posicao _posicao;

        public Rainha(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override bool[,] MovimentosPossiveis() {

            _matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            _posicao = new Posicao(0, 0);

            // acima
            _posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            VerificaPosicaoCimaBaixo(-1);

            // abaixo
            _posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            VerificaPosicaoCimaBaixo(+1);

            // direita
            _posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            VerificaPosicaoDireitaEsquerda(+1);

            // esquerda
            _posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            VerificaPosicaoDireitaEsquerda(-1);

            VerificaPosicaoDiagonal(-1, -1); // no
            VerificaPosicaoDiagonal(-1, +1); // ne
            VerificaPosicaoDiagonal(+1, -1); // so
            VerificaPosicaoDiagonal(+1, +1); // se

            return _matriz;
        }

        private void VerificaPosicaoDiagonal(int modLinha, int modColuna) {
            _posicao.DefinirValores(Posicao.Linha + modLinha, Posicao.Coluna + modColuna);
            while (Tabuleiro.IsPosicaoValida(_posicao) && PodeMover(_posicao))
            {
                _matriz[_posicao.Linha, _posicao.Coluna] = true;
                if (Tabuleiro.GetPeca(_posicao) != null && Tabuleiro.GetPeca(_posicao).Cor != Cor)
                    break;
                _posicao.DefinirValores(_posicao.Linha + modLinha, _posicao.Coluna + modColuna);
            }
        }

        private void VerificaPosicaoCimaBaixo(int modLinha) {
            while (Tabuleiro.IsPosicaoValida(_posicao) && PodeMover(_posicao))
            {
                _matriz[_posicao.Linha, _posicao.Coluna] = true;
                if (Tabuleiro.GetPeca(_posicao) != null && Tabuleiro.GetPeca(_posicao).Cor != this.Cor)
                    break;
                _posicao.Linha += modLinha;
            }
        }

        private void VerificaPosicaoDireitaEsquerda(int modColuna) {
            while (Tabuleiro.IsPosicaoValida(_posicao) && PodeMover(_posicao))
            {
                _matriz[_posicao.Linha, _posicao.Coluna] = true;
                if (Tabuleiro.GetPeca(_posicao) != null && Tabuleiro.GetPeca(_posicao).Cor != this.Cor)
                    break;
                _posicao.Coluna += modColuna;
            }
        }

        public override bool PodeMover(Posicao posicao) {
            Peca p = Tabuleiro.GetPeca(posicao);
            return p == null || p.Cor != Cor;
        }

        public override string ToString() {
            return "D";
        }
    }
}
