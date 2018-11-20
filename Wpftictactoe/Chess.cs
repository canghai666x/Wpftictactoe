using System.Collections;
namespace Wpftictactoe
{
    class Chess
    {
        public int turn;
        public int maxdepth;
        public int[] ButtonArray=new int[10];
        public int alpha;
        public int beta;
        public int index;
        
        public void init()
        {
            index = 0;
            turn = 0;
            maxdepth = 2;//初始化设置为向下递归两层
            alpha = -1000;
            beta = 1000;
            for(int i=0;i<ButtonArray.Length; i++)
            {
                ButtonArray[i] = 0;
            }
        }
        //换走子对象
        public int changeturn(int turn)
        {
            if (turn == 1)
                return 0;
            else return 1;
        }
        //判断输赢
        public int evatutate(int[] vs)
        {
            int mark = 0;
            if (Check(vs,1, 2, 3) == 1)
                mark = 1;
            if (Check(vs, 4, 5, 6) == 1)
                mark = 1;
            if (Check(vs, 7, 8, 9) == 1)
                mark = 1;
            if (Check(vs, 1, 5, 9) == 1)
                mark = 1;
            if (Check(vs, 1, 4, 7) == 1)
                mark = 1;
            if (Check(vs, 2, 5, 8) == 1)
                mark = 1;
            if (Check(vs, 3, 6, 9) == 1)
                mark = 1;
            if (Check(vs, 3, 5, 7) == 1)
                mark = 1;
            if (Check(vs, 1, 2, 3) == -1)
                mark = -1;
            if (Check(vs, 4, 5, 6) == -1)
                mark = -1;
            if (Check(vs, 7, 8, 9) == -1)
                mark = -1;
            if (Check(vs, 1, 5, 9) == -1)
                mark = -1;
            if (Check(vs, 3, 5, 7) == -1)
                mark = -1;
            if (Check(vs, 1, 4, 7) == -1)
                mark = -1;
            if (Check(vs, 2, 5, 8) == -1)
                mark = -1;
            if (Check(vs, 3, 6, 9) == -1)
                mark = -1;
            return mark;
        }
        //计算评估值
        private int evalutation(int[] vs)
        {
            int[] tempMax = new int[10];
            int[] tempMin = new int[10];
            int max = 0;
            int min = 0;
            for (int i=0;i<vs.Length;i++)
            {
                tempMax[i] = vs[i];
                tempMin[i] = vs[i];
            }
            for (int j=1;j < 10;j++)
            {
                if (tempMax[j] == 0)
                {
                    tempMax[j] = 1;
                    tempMin[j] = -1;
                }                    
            }
            if (Check(tempMax,1, 2, 3) == 1)
                max += 1;
            if (Check(tempMax, 4, 5, 6) == 1)
                max += 1;
            if (Check(tempMax, 7, 8, 9) == 1)
                max += 1;
            if (Check(tempMax, 1, 5, 9) == 1)
                max += 1;
            if (Check(tempMax, 3, 5, 7) == 1)
                max += 1;
            if (Check(tempMax, 1, 4, 7) == 1)
                max += 1;
            if (Check(tempMax, 2, 5, 8) == 1)
                max += 1;
            if (Check(tempMax, 3, 6, 9) == 1)
                max += 1;
            if (Check(tempMin,1, 2, 3) == -1)
                min += -1;
            if (Check(tempMin, 4, 5, 6) == -1)
                min += -1;
            if (Check(tempMin, 7, 8, 9) == -1)
                min += -1;
            if (Check(tempMin, 1, 5, 9) == -1)
                min += -1;
            if (Check(tempMin, 3, 5, 7) == -1)
                min += -1;
            if (Check(tempMax, 1, 4, 7) == -1)
                max += -1;
            if (Check(tempMax, 2, 5, 8) == -1)
                max += -1;
            if (Check(tempMax, 3, 6, 9) == -1)
                max += -1;
            return max - min;
        }
        //AI逻辑
        public int AlphaBeta(int[] Board,int depth,int turn,ref int alpha, ref int beta,ref int index)
        {
            ArrayList arrayList = getAvailablePos(Board);
            if (depth == 0)
                return evalutation(Board);
            if (turn == 1)
            {
                foreach (int i in arrayList)
                {
                    int[] newboard = generateNewBoard(Board, i, turn);
                    int value = AlphaBeta(newboard, depth - 1, changeturn(turn), ref alpha, ref beta,ref index);
                    if (value > alpha)
                    {
                        alpha = value;
                        index = i;
                    }
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return alpha;
            }
            else
            {
                foreach(int j in arrayList)
                {
                    int[] newboard = generateNewBoard(Board, j, turn);
                    int value = AlphaBeta(newboard, depth - 1, changeturn(turn), ref alpha, ref beta,ref index);
                    if(value<beta)
                    {
                        beta = value;
                        index = j;
                    }
                    if(alpha>=beta)
                    {
                        break;
                    }
                }
                return beta;
            }
        }
        //获得棋盘上空位
        private ArrayList getAvailablePos(int[] Board)
        {
            ArrayList arrayList = new ArrayList();
            for(int i=1;i<10;i++)
            {
                if (Board[i] == 0)
                    arrayList.Add(i);
            }
            return arrayList;
        }
        //新建临时棋盘
        private int[] generateNewBoard(int[] Board,int num,int turn)
        { 
            int[] newboard = new int[10];
            for(int i=1;i<Board.Length;i++)
            {
                newboard[i] = Board[i];
            }
            if (turn == 0)
                newboard[num] = -1;
            else
                newboard[num] = 1;
            return newboard;
        }

        private int Check(int[] Array, int a, int b, int c)
        {
            if (Array[a] == Array[b]&& Array[a] == Array[c] && Array[a] == 1)
                return 1;
            else if (Array[a] == Array[b]&& Array[a] == Array[c]&& Array[a] == -1)
                return -1;
            return 0;
        }
    }
}
