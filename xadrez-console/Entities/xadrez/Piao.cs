﻿using tabuleiro;

namespace xadrez
{
    internal class Piao : Peca
    {
        public Piao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) {
        }

        public override bool[,] MovimentosPossiveis() {
            throw new NotImplementedException();
        }

        public override bool PodeMover(Posicao posicao) {
            throw new NotImplementedException();
        }

        public override string ToString() {
            return "P";
        }
    }
}