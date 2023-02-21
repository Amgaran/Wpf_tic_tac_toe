using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Wpf_tic_tac_toe {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private static int _numStep = 0;
        private static Field field;
        private static bool bot_flag;
        private static Bot bot;
        
        public MainWindow() {
            InitializeComponent();
        }

        private void startGame_clickButton(object sender, RoutedEventArgs e) {
            int size;
            _numStep = 0;
            while (!int.TryParse(InputSize.Text, out size) || size is < 3 or > 10) {
                MessageBox.Show("Invalid size");
                return;
            }
            GameGrid.ColumnDefinitions.Clear();
            GameGrid.RowDefinitions.Clear();
            GameGrid.Children.Clear();
            field = new Field(size);
            if (bot_flag) bot = new RandomBot(size);
            for (var i = 0; i < size; i++) {
                var row = new RowDefinition();
                var column = new ColumnDefinition();
                GameGrid.RowDefinitions.Add(row);
                GameGrid.ColumnDefinitions.Add(column);
            }

            for (var i = 0; i < size; i++) {
                for (var j = 0; j < size; j++) {
                    var label = new Label {
                        Content = ' '
                    };
                    label.Name = "Name_" + i.ToString() + "_" + j.ToString();
                    label.MouseLeftButtonUp += new MouseButtonEventHandler(ClickOnLabel);
                    Grid.SetColumn(label, j);
                    Grid.SetRow(label, i);
                    GameGrid.Children.Add(label);
                }
            }
        }

        private void EmenyChooise_Checked(object sender, RoutedEventArgs e) {
            RadioButton pressed = (RadioButton)sender;
            if (pressed.Content.ToString() == "Random Bot") {
                bot_flag = true;
            } else bot_flag = false;
        }

        private void ClickOnLabel(object sender, RoutedEventArgs e) {
            var lbl = sender as Label;
            var symbol = _numStep % 2 == 0 ? 'X' : 'O';
            if (lbl != null) {
                lbl.Content = symbol;
                var x = 0;
                var y = 0;
                for (var i = 0; i < field.Size; i++) {
                    for (var j = 0; j < field.Size; j++) {
                        if (lbl.Name != "Name_" + i.ToString() + "_" + j.ToString()) continue;
                        x = i;
                        y = j;
                    }
                }
                field.SetSymbol(x,y, symbol);
                lbl.FontSize = 64;
                lbl.HorizontalAlignment = HorizontalAlignment.Center;
                lbl.VerticalAlignment = VerticalAlignment.Center;
                lbl.IsEnabled = false;
                if (field.checkWin()) {
                    symbol = _numStep % 2 == 0 ? 'X' : 'O';
                    foreach (var item in GameGrid.Children) {
                        var label = item as Label;
                        label.IsEnabled = false;
                    }
                    MessageBox.Show(symbol + " win");
                    return;
                }
                _numStep++;
                if (bot_flag) {
                    bot.make_move(_numStep, field);
                    for (var i = 0; i < field.Size; i++) {
                        for (var j = 0; j < field.Size; j++) {
                            foreach (var item in GameGrid.Children) {
                                var label = item as Label;
                                if (label.Name[5].ToString() == i.ToString() &&
                                    label.Name[7].ToString() == j.ToString()) {
                                    label.Content = field.Cells[i, j];
                                    label.FontSize = 64;
                                    label.HorizontalAlignment = HorizontalAlignment.Center;
                                    label.VerticalAlignment = VerticalAlignment.Center;
                                    if (label.Name[5].ToString() == bot.Cell[0].ToString() &&
                                        label.Name[7].ToString() == bot.Cell[1].ToString()) label.IsEnabled = false;
                                }
                            }
                        }
                    }
                    if (field.checkWin()) {
                        symbol = _numStep % 2 == 0 ? 'X' : 'O';
                        foreach (var item in GameGrid.Children) {
                            var label = item as Label;
                            label.IsEnabled = false;
                        }
                        MessageBox.Show(symbol + " win");
                        return;
                    }
                    _numStep++;

                }
                
            };
        }
    }
}