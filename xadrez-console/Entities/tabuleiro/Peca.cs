﻿namespace tabuleiro
{
    abstract internal class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            this.Posicao = null;
            this.Cor = cor;
            this.Tabuleiro = tabuleiro;
            this.QuantMovimentos = 0;
        }

        public void IncrementarQuantMovimentos()
        {
            this.QuantMovimentos++;
        }

        public abstract bool[,] MovimentosPossiveis();

        public abstract bool PodeMover(Posicao posicao);

    }
}
