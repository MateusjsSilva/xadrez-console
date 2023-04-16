using tabuleiro;

namespace xadrez
{
    internal class Torre : Peca
    {
        private bool[,] _matriz;
        private Posicao _posicao;

        public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override bool[,] MovimentosPossiveis() {

            _matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            _posicao = new(0, 0);

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

            return _matriz;
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
            return "T";
        }
    }
}
