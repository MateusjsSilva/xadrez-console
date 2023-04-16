using tabuleiro;
using tabuleiro.exceptions;

namespace xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public int Turno { get; private set; }
        public bool Terminada { get; private set; }
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

        private HashSet<Peca> _pecas;
        private HashSet<Peca> _capturadas;

        public PartidaXadrez() {

            this.Tabuleiro = new Tabuleiro(8, 8);
            this._pecas = new HashSet<Peca>();
            this._capturadas = new HashSet<Peca>();

            this.VulneravelEnPassant = null;
            this.Turno = 1;
            this.JogadorAtual = Cor.Branca;
            this.Terminada = false;

            ColocarPecas();
        }

        public void AddNewPeca(char coluna, int linha, Peca peca) {
            Tabuleiro.AddPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            _pecas.Add(peca);
        }

        public static Cor Adversaria(Cor cor) {
            if (cor == Cor.Branca)
                return Cor.Preta;

            return Cor.Branca;
        }

        public bool EstaEmXeque(Cor cor) {
            Peca rei = Rei(cor);
            foreach (Peca peca in GetEmJogo(Adversaria(cor)))
            {
                bool[,] matriz = peca.MovimentosPossiveis();
                if (matriz[rei.Posicao.Linha, rei.Posicao.Coluna])
                    return true;
            }
            return false;
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino) {

            Peca peca = Tabuleiro.RemovePeca(origem);
            Peca pecaCapturada = Tabuleiro.RemovePeca(destino);

            peca.IncrementarQuantMovimentos();
            Tabuleiro.AddPeca(peca, destino);

            if (pecaCapturada != null)
                _capturadas.Add(pecaCapturada);

            // #jogada especial roque pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Peca T = Tabuleiro.RemovePeca(new(origem.Linha, origem.Coluna + 3));
                Tabuleiro.AddPeca(T, new(origem.Linha, origem.Coluna + 1));
                T.IncrementarQuantMovimentos();
            }

            // #jogada especial roque grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Peca T = Tabuleiro.RemovePeca(new(origem.Linha, origem.Coluna - 4));
                Tabuleiro.AddPeca(T, new(origem.Linha, origem.Coluna - 1));
                T.IncrementarQuantMovimentos();
            }

            // #jogada especial en Passant
            if (peca is Peao)
            {
                if(origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if(peca.Cor == Cor.Branca)
                        posP = new(destino.Linha + 1, destino.Coluna);
                    else
                        posP = new(destino.Linha - 1, destino.Coluna);

                    pecaCapturada = Tabuleiro.RemovePeca(posP);
                    _capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {

            Peca peca = Tabuleiro.RemovePeca(destino);
            peca.DecrementarQuantMovimentos();

            if (pecaCapturada != null)
            {
                Tabuleiro.AddPeca(pecaCapturada, destino);
                _capturadas.Remove(pecaCapturada);
            }
            Tabuleiro.AddPeca(peca, origem);

            // #jogada especial roque pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RemovePeca(destinoT);
                T.DecrementarQuantMovimentos();
                Tabuleiro.AddPeca(T, origemT);
            }

            // #jogada especial roque grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RemovePeca(destinoT);
                T.IncrementarQuantMovimentos();
                Tabuleiro.AddPeca(T, origemT);
            }

            // #jogada especial en Passant
            if (peca is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tabuleiro.RemovePeca(destino);

                    Posicao posP;
                    if (peca.Cor == Cor.Branca)
                        posP = new(3, destino.Coluna);
                    else
                        posP = new(4, destino.Coluna);

                    Tabuleiro.AddPeca(peao, posP);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino) {

            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = Tabuleiro.GetPeca(destino);

            // #jogada especial promoção
            if(p is Peao)
            {
                if((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
                {
                    p = Tabuleiro.RemovePeca(destino);
                    _pecas.Remove(p);
                    Peca dama = new Rainha(Tabuleiro, p.Cor);
                    Tabuleiro.AddPeca(dama, destino);
                    _pecas.Add(dama);
                }
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
                Xeque = true;
            else
                Xeque = false;

            if (TesteXequemate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            p = Tabuleiro.GetPeca(destino);

            // #Jogada especial en passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
                VulneravelEnPassant = p;
            else
                VulneravelEnPassant = null;

        }

        public bool TesteXequemate(Cor cor) {

            if (!EstaEmXeque(cor))
                return false;

            foreach (Peca peca in GetEmJogo(cor))
            {
                bool[,] matriz = peca.MovimentosPossiveis();
                for (int i = 0; i < Tabuleiro.Linhas; i++)
                {
                    for (int j = 0; j < Tabuleiro.Colunas; j++)
                    {
                        if (matriz[i, j])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        public void ValidarPosicaoDeOrigem(Posicao posicao) {
            if (Tabuleiro.GetPeca(posicao) == null)
                throw new TabuleiroException("Não existe peça na posição escolhida!");

            if (JogadorAtual != Tabuleiro.GetPeca(posicao).Cor)
                throw new TabuleiroException("A peça escolhida não é sua!");

            if (!Tabuleiro.GetPeca(posicao).ExisteMovimentosPossiveis())
                throw new TabuleiroException("Não é possivel mover a peça escolhida!");
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino) {
            if (!Tabuleiro.GetPeca(origem).MovimentoPossivel(destino))
                throw new TabuleiroException("Não é possivel mover para esta posição!");
        }

        private void MudaJogador() {
            if (JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;
        }

        private Peca Rei(Cor cor) {
            foreach (Peca peca in GetEmJogo(cor))
                if (peca is Rei) return peca;

            throw new TabuleiroException("Não tem Rei da cor " + cor + " no tabuleiro!");
        }

        public HashSet<Peca> GetCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in _capturadas)
            {
                if (x.Cor == cor)
                    aux.Add(x);
            }
            return aux;
        }

        public HashSet<Peca> GetEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in _pecas)
            {
                if (x.Cor == cor)
                    aux.Add(x);
            }
            aux.ExceptWith(GetCapturadas(cor));
            return aux;
        }

        private void ColocarPecas() {

            // Brancas
            AddNewPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            AddNewPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
            AddNewPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
            AddNewPeca('d', 1, new Rainha(Tabuleiro, Cor.Branca));
            AddNewPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
            AddNewPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            AddNewPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            AddNewPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));

            AddNewPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
            AddNewPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
            AddNewPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
            AddNewPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
            AddNewPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
            AddNewPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
            AddNewPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
            AddNewPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));

            // Pretas
            AddNewPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            AddNewPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            AddNewPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            AddNewPeca('d', 8, new Rainha(Tabuleiro, Cor.Preta));
            AddNewPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
            AddNewPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            AddNewPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            AddNewPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));

            AddNewPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
            AddNewPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
            AddNewPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
            AddNewPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
            AddNewPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
            AddNewPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
            AddNewPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
            AddNewPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));
        }
    }
}
