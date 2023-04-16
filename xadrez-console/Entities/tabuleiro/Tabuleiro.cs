using tabuleiro.exceptions;

namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }

        private Peca[,] _pecas;

        public Tabuleiro(int linhas, int colunas) {
            this.Linhas = linhas;
            this.Colunas = colunas;
            this._pecas = new Peca[linhas, colunas];
        }

        public void AddPeca(Peca peca, Posicao posicao) {
            if (ExistePeca(posicao))
                throw new TabuleiroException("Já existe uma peça nessa posição!");

            _pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public Peca RemovePeca(Posicao posicao) {
            if (!ExistePeca(posicao))
                return null;

            Peca auxPeca = _pecas[posicao.Linha, posicao.Coluna];
            _pecas[posicao.Linha, posicao.Coluna] = null;
            return auxPeca;
        }

        public bool ExistePeca(Posicao posicao) {
            ValidarPosicao(posicao);
            return GetPeca(posicao) != null;
        }

        public bool IsPosicaoValida(Posicao posicao) {
            if (posicao.Linha < 0 || posicao.Linha >= Linhas || posicao.Coluna < 0 || posicao.Coluna >= Colunas)
                return false;
            return true;
        }

        public void ValidarPosicao(Posicao posicao) {
            if (!IsPosicaoValida(posicao))
                throw new TabuleiroException("Posição invalida!");
        }

        public Peca GetPeca(int linha, int coluna) {
            return _pecas[linha, coluna];
        }

        public Peca GetPeca(Posicao posicao) {
            return _pecas[posicao.Linha, posicao.Coluna];
        }
    }
}
