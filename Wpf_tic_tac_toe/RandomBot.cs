using System;

namespace Wpf_tic_tac_toe {
    public class RandomBot : Bot {
        private readonly int _size;
        private Random _rnd;

        public RandomBot(int size) {
            _size = size;
            _rnd = new Random();
        }
        
        public override void make_move(int numStep, Field field) {
            var symbol = numStep % 2 == 0 ? 'X' : 'O';
            do {
                Cell[0] = _rnd.Next(0, _size);
                Cell[1] = _rnd.Next(0, _size);
            } while (field.CheckSymbol(Cell[0], Cell[1], false));

            field.SetSymbol(Cell[0], Cell[1], symbol);
        }
    }
}