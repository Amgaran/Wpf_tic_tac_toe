﻿
namespace Wpf_tic_tac_toe {
    public abstract class Bot {
        public int[] Cell;

        protected Bot() {
            Cell = new int[2];
        }

        public abstract void make_move(int numStep, Field field);
    }
}

