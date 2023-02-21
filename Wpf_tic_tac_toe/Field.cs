using System;

namespace Wpf_tic_tac_toe {
    public class Field {
        public readonly int Size;
        public char[, ] Cells;

        public Field(int size) {
            Size = size;
            Cells = new char[Size, Size];
            for (var i = 0; i < Size; i++) {
                for (var j = 0; j < Size; j++) {
                    Cells[i, j] = ' ';
                }
            }
        }

        public void SetSymbol(int x, int y, char symbol) {
            Cells[x, y] = symbol;
        }

        public Field Copy() {
            var other = new Field(Size);
            for (var j = 0; j < Size; j++) {
                for (var i = 0; i < Size; i++) {
                    other.Cells[i, j] = Cells[i, j];
                }
            }

            return other;
        }

        public bool CheckSymbol(int x, int y, bool playerMove) {
            if (x < 0 || x > Size-1 || y < 0 || y > Size-1) {
                Console.WriteLine("Такой точки нет на поле. Введите другие координаты.");
                return true;
            } 
            var flag = Cells[x, y] != ' ';
            if (flag && playerMove) {
                Console.WriteLine("В данной клетке уже стоит другой символ. Введите другие координаты.");
            }
            return flag;
        }

        private bool CheckRow(int x, int y) {
            return (Cells[x - 1, y] == Cells[x, y]) && (Cells[x, y] == Cells[x + 1, y]);
        }

        private bool CheckColumn(int x, int y) {
            return (Cells[x, y-1] == Cells[x, y]) && (Cells[x, y] == Cells[x, y+1]);
        }

        private bool CheckDiags(int x, int y) {
            var top = (Cells[x - 1, y - 1] == Cells[x, y]) && (Cells[x, y] == Cells[x + 1, y + 1]);
            var bottom = (Cells[x - 1, y + 1] == Cells[x, y]) && (Cells[x, y] == Cells[x + 1, y - 1]);
            return top || bottom;
        }

        public bool checkWin() {
            var flag = false;

            for (var i = 1; i < Size-1; i++) {
                if (Cells[i, 0] == ' ') {
                    continue;
                }
                flag = CheckRow(i, 0);
                if (flag) {
                    return flag;
                }
            }
            
            for (var i = 1; i < Size-1; i++) {
                if (Cells[i, Size-1] == ' ') {
                    continue;
                }
                flag = CheckRow(i, Size-1);
                if (flag) {
                    return flag;
                }
            }
            
            for (var i = 1; i < Size-1; i++) {
                if (Cells[0, i] == ' ') {
                    continue;
                }
                flag = CheckColumn(0, i);
                if (flag) {
                    return flag;
                }
            }
            
            for (var i = 1; i < Size-1; i++) {
                if (Cells[Size-1, i] == ' ') {
                    continue;
                }
                flag = CheckColumn(Size-1, i);
                if (flag) {
                    return flag;
                }
            }
            
            for (var i = 1; i < Size-1; i++) {
                for (var j = 1; j < Size-1; j++) {
                    if (Cells[i, j] == ' ') {
                        continue;
                    }
                    var flagRow = CheckRow(i,j);
                    var flagColumn = CheckColumn(i,j);
                    var flagDiags = CheckDiags(i, j);
                    flag = flagRow || flagColumn || flagDiags;
                    if (flag) {
                        return flag;
                    }
                }
            }

            return flag;
        }

        public void printField() {
            for (var j = 0; j < Size; j++) {
                for (var i = 0; i < Size; i++) {
                    Console.Write(Cells[i, j]);
                    Console.Write('|');
                }
                Console.Write('\n');
                for (var i = 0; i < Size; i++) {
                    Console.Write('-');
                    Console.Write('-');
                }
                Console.Write('\n');
            }
        }
    }
}