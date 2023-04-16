using tabuleiro;

namespace xadrez
{
    internal class Bispo : Peca
    {
        private bool[,] _matriz;
        private Posicao _posicao;

        public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override bool[,] MovimentosPossiveis() {

            _matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            _posicao = new(0, 0);

            VerificaPosicao(-1, -1); // no
            VerificaPosicao(-1, +1); // ne
            VerificaPosicao(+1, -1); // so
            VerificaPosicao(+1, +1); // se

            return _matriz;
        }

        private void VerificaPosicao(int modLinha, int modColuna) {
            _posicao.DefinirValores(Posicao.Linha + modLinha, Posicao.Coluna + modColuna);
            while (Tabuleiro.IsPosicaoValida(_posicao) && PodeMover(_posicao))
            {
                _matriz[_posicao.Linha, _posicao.Coluna] = true;
                if (Tabuleiro.GetPeca(_posicao) != null && Tabuleiro.GetPeca(_posicao).Cor != Cor)
                    break;
                _posicao.DefinirValores(_posicao.Linha + modLinha, _posicao.Coluna + modColuna);
            }
        }

        public override bool PodeMover(Posicao posicao) {
            Peca p = Tabuleiro.GetPeca(posicao);
            return p == null || p.Cor != Cor;
        }

        public override string ToString() {
            return "B";
        }
    }
}
