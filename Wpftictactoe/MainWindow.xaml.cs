using System;
using System.Windows;
using System.Windows.Controls;

namespace Wpftictactoe
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Chess chess = new Chess();
        public MainWindow()
        {
            InitializeComponent();
            chess.init();
        }
        //判断
        private void judge(int type)
        {
            if (type == 1)
            { 
                MessageBox.Show("You win!");
                clear();
            }
            if (type == -1)
            {
                MessageBox.Show("You failure!");
            }      
        }
        //清空
        private void clear()
        {
            for(int i=1;i<10;i++)
            {
                chess.ButtonArray[i] = 0;
                Button button = FindName("bt" + i) as Button;
                button.Content = null;
            }        
        }
        //棋盘点击
        private void btm_Click(object sender, RoutedEventArgs e)
        {
            var btm = sender as Button;
            int index = Int32.Parse(btm.Name.Substring(2, 1));
            if(chess.ButtonArray[index]==0)
            {
                btm.Content = "X";
                chess.ButtonArray[index] = 1;
                Console.WriteLine(index);
                int type1 = chess.evatutate(chess.ButtonArray);
                judge(type1);
                chess.turn=chess.changeturn(chess.turn);
                Ai_Move();
                int type2 = chess.evatutate(chess.ButtonArray);
                judge(type2);
            } 
        }
        //人机先走选择
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var Rb = sender as RadioButton;
            if(Rb.IsChecked==true&&Rb.Name.Equals("user"))
            {
                chess.turn = 1;
            }
            else
            {
                chess.turn = 0;
            }
        }
        //AI下棋
        private void Ai_Move()
        {
            if(chess.turn==0)
            {
                chess.AlphaBeta(chess.ButtonArray, chess.maxdepth, chess.turn,ref chess.alpha, ref chess.beta, ref chess.index);
                int index = chess.index;
                Console.WriteLine(index);
                chess.ButtonArray[index] = -1;
                Button button = FindName("bt" + index) as Button;
                button.Content = "O";
                chess.turn = chess.changeturn(chess.turn);
                chess.alpha = -1000;
                chess.beta = 1000;
            }
        }
        //难度选择
        private void Sec_Checked(object sender, RoutedEventArgs e)
        {
            var Rb = sender as RadioButton;
            if(Rb.IsChecked==true)
            {
                if (Rb.Name.Equals("high"))
                    chess.maxdepth = 9;
                else if (Rb.Name.Equals("mid"))
                    chess.maxdepth = 6;
                else if (Rb.Name.Equals("low"))
                    chess.maxdepth = 3;        
            }
        }
    }
}
