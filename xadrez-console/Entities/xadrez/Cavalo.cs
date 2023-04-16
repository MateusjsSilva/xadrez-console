using tabuleiro;

namespace xadrez
{
    internal class Cavalo : Peca
    {
        private bool[,] _matriz;
        private Posicao _posicao;

        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override bool[,] MovimentosPossiveis() {

            _matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            _posicao = new(0, 0);

            VerificaPosicao(-1, -2);
            VerificaPosicao(-2, -1);
            VerificaPosicao(-2, +1);
            VerificaPosicao(-1, +2);
            VerificaPosicao(+1, +2);
            VerificaPosicao(+2, +1);
            VerificaPosicao(+2, -1);
            VerificaPosicao(+1, -2);

            return _matriz;
        }

        private void VerificaPosicao(int modLinha, int modColuna) {
            _posicao.DefinirValores(Posicao.Linha + modLinha, Posicao.Coluna + modColuna);
            if (Tabuleiro.IsPosicaoValida(_posicao) && PodeMover(_posicao))
                _matriz[_posicao.Linha, _posicao.Coluna] = true;
        }

        public override bool PodeMover(Posicao posicao) {
            Peca p = Tabuleiro.GetPeca(posicao);
            return p == null || p.Cor != Cor;
        }

        public override string ToString() {
            return "C";
        }
    }
}
