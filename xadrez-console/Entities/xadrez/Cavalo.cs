﻿using tabuleiro;

namespace xadrez
{
    internal class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override bool[,] MovimentosPossiveis() {

            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new(0, 0);

            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
                matriz[pos.Linha, pos.Coluna] = true;

            return matriz;
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
