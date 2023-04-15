using tabuleiro;
using tabuleiro.exceptions;

namespace xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        public bool Xeque { get; private set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaXadrez() {

            this.Tabuleiro = new Tabuleiro(8, 8);
            this.pecas = new HashSet<Peca>();
            this.capturadas = new HashSet<Peca>();

            this.Turno = 1;
            this.JogadorAtual = Cor.Branca;
            this.Terminada = false;

            ColocarPecas();
        }

        public void AddNewPeca(char coluna, int linha, Peca p) {
            Tabuleiro.AddPeca(p, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(p);
        }

        public static Cor Adversaria(Cor cor) {
            if (cor == Cor.Branca)
                return Cor.Preta;
            return Cor.Branca;
        }

        private Peca? Rei(Cor cor) {
            foreach (Peca peca in GetEmJogo(cor))
                if (peca is Rei) return peca;
            return null;
        }

        public bool EstaEmXeque(Cor cor) {
            Peca? r = Rei(cor) ?? throw new TabuleiroException("Não tem Rei da cor " + cor + " no tabuleiro!");

            foreach (Peca peca in GetEmJogo(Adversaria(cor)))
            {
                bool[,] matriz = peca.MovimentosPossiveis();
                if (matriz[r.Posicao.Linha, r.Posicao.Coluna])
                    return true;
            }
            return false;
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino) {
            Peca? p = Tabuleiro.RemovePeca(origem);
            p.IncrementarQuantMovimentos();

            Peca? pecaCapturada = Tabuleiro.RemovePeca(destino);
            Tabuleiro.AddPeca(p, destino);

            if (pecaCapturada != null)
                capturadas.Add(pecaCapturada);

            // #jogada especial roque pequeno
            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RemovePeca(origemT);
                T.IncrementarQuantMovimentos();
                Tabuleiro.AddPeca(T, destinoT);
            }

            // #jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RemovePeca(origemT);
                T.IncrementarQuantMovimentos();
                Tabuleiro.AddPeca(T, destinoT);
            }

            return pecaCapturada;
        }

        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca p = Tabuleiro.RemovePeca(destino);
            p.DecrementarQuantMovimentos();
            if (pecaCapturada != null)
            {
                Tabuleiro.AddPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            Tabuleiro.AddPeca(p, origem);

            // #jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RemovePeca(destinoT);
                T.DecrementarQuantMovimentos();
                Tabuleiro.AddPeca(T, origemT);
            }

            // #jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RemovePeca(destinoT);
                T.IncrementarQuantMovimentos();
                Tabuleiro.AddPeca(T, origemT);
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino) {
            Peca? pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
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
                            Posicao destino = new Posicao(i, j);
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

        public HashSet<Peca> GetCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
                    aux.Add(x);
            }
            return aux;
        }

        public HashSet<Peca> GetEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
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

            AddNewPeca('a', 2, new Peao(Tabuleiro, Cor.Branca));
            AddNewPeca('b', 2, new Peao(Tabuleiro, Cor.Branca));
            AddNewPeca('c', 2, new Peao(Tabuleiro, Cor.Branca));
            AddNewPeca('d', 2, new Peao(Tabuleiro, Cor.Branca));
            AddNewPeca('e', 2, new Peao(Tabuleiro, Cor.Branca));
            AddNewPeca('f', 2, new Peao(Tabuleiro, Cor.Branca));
            AddNewPeca('g', 2, new Peao(Tabuleiro, Cor.Branca));
            AddNewPeca('h', 2, new Peao(Tabuleiro, Cor.Branca));

            // Pretas
            AddNewPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            AddNewPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            AddNewPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            AddNewPeca('d', 8, new Rainha(Tabuleiro, Cor.Preta));
            AddNewPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
            AddNewPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            AddNewPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            AddNewPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));

            AddNewPeca('a', 7, new Peao(Tabuleiro, Cor.Preta));
            AddNewPeca('b', 7, new Peao(Tabuleiro, Cor.Preta));
            AddNewPeca('c', 7, new Peao(Tabuleiro, Cor.Preta));
            AddNewPeca('d', 7, new Peao(Tabuleiro, Cor.Preta));
            AddNewPeca('e', 7, new Peao(Tabuleiro, Cor.Preta));
            AddNewPeca('f', 7, new Peao(Tabuleiro, Cor.Preta));
            AddNewPeca('g', 7, new Peao(Tabuleiro, Cor.Preta));
            AddNewPeca('h', 7, new Peao(Tabuleiro, Cor.Preta));
        }
    }
}
