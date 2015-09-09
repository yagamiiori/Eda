using System;
using System.Collections.Generic;
using System.Text;

namespace Shanten
{
    class MjUtil
    {
        public static Dictionary<int, string> PAI;	// 牌テーブル
        public static int PAI_MAX;					// 最大牌数

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string s = "111345m45p12379s7z";
            //List<int> tehai = HairiTextToArray(s);

            //State result = ShantenCheck2(tehai);

            //Console.WriteLine(result.PartitionString);

            TestShantenCheck2();
        }

		/////////////////////////////////////
		// シャンテン数確認関数
		/////////////////////////////////////
        static void TestShantenCheck2()
        {
            DateTime startTime = DateTime.Now;										// 処理開始時間を取得
            string[] lines = System.IO.File.ReadAllLines(@"problems\normal.txt");	// ファイルから手牌を取得

			// 取得した手牌分をループ
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Trim() == "")
                    continue;

                string[] a = line.Split(' ');
                List<int> tehai = new List<int>();
                for (int j = 0; j < 14; j++)
                {
                    tehai.Add(int.Parse(a[j]));
                }
                int shanten = int.Parse(a[14]);

                Console.Write(string.Format("{0} : {1}", i + 1, ArrayToString(tehai)));

                State result = ShantenCheck2(tehai);
                if (result.Shanten != shanten)
                {
                    Console.WriteLine(string.Format("ERROR! expected={0} actual={1} partition={2}", shanten, result.Shanten, result.PartitionString));
                    break;
                }

                Console.WriteLine(string.Format("  : {0}シャンテン", result.Shanten));
            }

            DateTime endTime = DateTime.Now;
            Console.WriteLine("Finish: {0} seconds", (endTime - startTime).TotalSeconds.ToString());

            Console.ReadLine();
        }

		//////////////////////
		// コンストラクタ
		//////////////////////
        static MjUtil()
        {
            PAI = new Dictionary<int, string>();
            PAI[1] = "一";
            PAI[2] = "二";
            PAI[3] = "三";
            PAI[4] = "四";
            PAI[5] = "五";
            PAI[6] = "六";
            PAI[7] = "七";
            PAI[8] = "八";
            PAI[9] = "九";
            PAI[11] = "①";
            PAI[12] = "②";
            PAI[13] = "③";
            PAI[14] = "④";
            PAI[15] = "⑤";
            PAI[16] = "⑥";
            PAI[17] = "⑦";
            PAI[18] = "⑧";
            PAI[19] = "⑨";
            PAI[21] = "１";
            PAI[22] = "２";
            PAI[23] = "３";
            PAI[24] = "４";
            PAI[25] = "５";
            PAI[26] = "６";
            PAI[27] = "７";
            PAI[28] = "８";
            PAI[29] = "９";
            PAI[31] = "東";
            PAI[32] = "南";
            PAI[33] = "西";
            PAI[34] = "北";
            PAI[35] = "白";
            PAI[36] = "発";
            PAI[37] = "中";
            PAI_MAX = 38;
        }

        public static List<int> HairiTextToArray(string s)
        {
            s = s.ToLower();
            List<int> result = new List<int>();

            string type = "z";
            for (int i = s.Length - 1; i >= 0; i--)
            {
                string v = s.Substring(i, 1);
                if (v == "m" || v == "p" || v == "s" || v == "z")
                {
                    type = v;
                }
                else
                {
                    if (type == "m")
                    {
                        result.Add(int.Parse(v) + 0);
                    }
                    else if (type == "p")
                    {
                        result.Add(int.Parse(v) + 10);
                    }
                    else if (type == "s")
                    {
                        result.Add(int.Parse(v) + 20);
                    }
                    else if (type == "z")
                    {
                        result.Add(int.Parse(v) + 30);
                    }
                    else
                    {
                        throw new Exception("incorrect type");
                    }
                }
            }

            result.Reverse();

            return result;
        }

        public static string ArrayToString(List<int> a)
        {
            string result = "";

            foreach (int v in a)
            {
                result += MjUtil.PAI[v];
            }

            return result;
        }

		// テーブル構成関数
        public static Dictionary<int, int> GroupByKind(List<int> tehai)
        {
            Dictionary<int, int> kind = new Dictionary<int, int>();

            for (int i = 0; i <= PAI_MAX; i++)
            {
                kind[i] = 0;
            }

            foreach (int v in tehai)
            {
                kind[v] += 1;
            }

            return kind;
        }

		//=====================================================================
        // 向聴数を求めるアルゴリズム - あらの（一人）麻雀研究所
        // http://mahjong.ara3.net/etc/shanten/index.htm
        // を実装
        //
        // テーブルは使用していない
		//=====================================================================
        public static State ShantenCheck2(List<int> tehai)
        {
			// テーブル構成関数より手牌を構築
            Dictionary<int, int> kind = GroupByKind(tehai);

            List<State> results = new List<State>();

            foreach (int pai in kind.Keys)
            {
                int cnt = kind[pai];
                
                // 雀頭(同じ牌が2つ)ある場合
                if (cnt >= 2)
                {
                    Dictionary<int, int> tmp = new Dictionary<int,int>(kind);
                    tmp[pai] -= 2;

					// 雀頭判定フラグをtrueにして面子構成関数を起動
                    State state = new State();
                    state.hasAtama = true;
                    State subResult = CountMentsuAndKouho(tmp, 0, state);
                    results.Add(subResult);	// リストresults
                }
            }

			// 雀頭判定フラグをfalseにして面子構成関数を起動
            State state2 = new State();
            state2.hasAtama = false;
            State subResultr2 = CountMentsuAndKouho(kind, 0, state2);
            results.Add(subResult2);

            int min_shanten = 999;
            State ret = null;
            foreach (State r in results)
            {
                if (min_shanten > r.getShanten())
                {
                    min_shanten = r.getShanten();
                    ret = r;
                }
            }

            return ret;
        }

		//=====================================================================
		// 面子構成判定関数
        // このメソッドの中で、stateクラスのメンバを変更しないこと！
		//=====================================================================
        public static State CountMentsuAndKouho(Dictionary<int, int> kind, int level, State state)
        {

            State result = state;
            int minShanten = state.getShanten();

            for (int j = 1; j < PAI_MAX; j++)
            {
                if (!kind.ContainsKey(j))
                    continue;

                if (kind[j] >= 3)
                {
                    Dictionary<int, int> tmp = new Dictionary<int, int>(kind);
                    tmp[j] -= 3;

                    string partition = string.Format("{0} {1} {2} /", j, j, j);

                    State st = state.Clone();
                    st.mentsu += 1;
                    st.partition += partition;

                    // とった残りの手牌を再帰的に計算
                    State subResult = CountMentsuAndKouho(tmp, level + 1, st);

                    if (subResult.getShanten() < minShanten)
                    {
                        minShanten = subResult.getShanten();
                        result = subResult;
                    }
                }

                if (j < 29 && kind[j] >= 1 && kind[j + 1] >= 1 && kind[j + 2] >= 1)
                {
                    Dictionary<int, int> tmp = new Dictionary<int, int>(kind);
                    tmp[j] -= 1;
                    tmp[j+1] -= 1;
                    tmp[j+2] -= 1;

                    string partition = string.Format("{0} {1} {2} /", j, j+1, j+2);

                    State st = state.Clone();
                    st.mentsu += 1;
                    st.partition += partition;

                    // とった残りの手牌を再帰的に計算
                    State subResult = CountMentsuAndKouho(tmp, level + 1, st);

                    if (subResult.getShanten() < minShanten)
                    {
                        minShanten = subResult.getShanten();
                        result = subResult;
                    }
                }
                
                // 刻子候補があればとる
                if (kind[j] >= 2)
                {
                    Dictionary<int, int> tmp = new Dictionary<int, int>(kind);
                    tmp[j] -= 2;

                    string partition = string.Format("{0} {1} /", j, j);

                    State st = state.Clone();
                    st.kouho += 1;
                    st.partition += partition;

                    // とった残りの手牌を再帰的に計算
                    State subResult = CountMentsuAndKouho(tmp, level + 1, st);

                    if (subResult.getShanten() < minShanten)
                    {
                        minShanten = subResult.getShanten();
                        result = subResult;
                    }
                }

                // 順子候補があればとる（ペンチャンorリャンメン）
                if (j <= 29 && kind[j] >= 1 && kind[j + 1] >= 1)
                {
                    Dictionary<int, int> tmp = new Dictionary<int, int>(kind);
                    tmp[j] -= 1;
                    tmp[j+1] -= 1;

                    string partition = string.Format("{0} {1} /", j, j+1);

                    State st = state.Clone();
                    st.kouho += 1;
                    st.partition += partition;

                    // とった残りの手牌を再帰的に計算
                    State subResult = CountMentsuAndKouho(tmp, level + 1, st);

                    if (subResult.getShanten() < minShanten)
                    {
                        minShanten = subResult.getShanten();
                        result = subResult;
                    }
                }

                // 順子候補があればとる（カンチャン）
                if (j <= 29 && kind[j] >= 1 && kind[j + 2] >= 1 && (j/10 == (j+2)/10))
                {
                    Dictionary<int, int> tmp = new Dictionary<int, int>(kind);
                    tmp[j] -= 1;
                    tmp[j + 2] -= 1;

                    string partition = string.Format("{0} {1} /", j, j + 2);

					// Stateクラスインスタンスの簡易コピーを生成
                    State st = state.Clone();

					// 面子候補数を+1
                    st.kouho += 1;

					// 手牌分割文字列を+1
                    st.partition += partition;

                    // とった残りの手牌を再帰的に計算
                    State subResult = CountMentsuAndKouho(tmp, level + 1, st);

                    if (subResult.getShanten() < minShanten)
                    {
                        minShanten = subResult.getShanten();
                        result = subResult;
                    }
                }
            }
            return result;
        }
    }

	//=====================================================================
	// ステータスクラス
	//=====================================================================
    class State
    {
        public int mentsu;              // 面子の個数
        public int kouho;               // 面子候補の個数
        public bool hasAtama;           // 頭が確定しているか
        public string partition;        // 現在の手牌の分け方を表す文字列

        public int Shanten				// シャンテン数
        {
            get
            {
                return this.getShanten();
            }
        }
        
        public string PartitionString	// 手牌分割用文字列
        {
            get
            {
                return this.getPartitionString();
            }
        }

		/////////////////////////////////////
		// コンストラクタ
		/////////////////////////////////////
        public State()
        {
            this.mentsu = 0;
            this.kouho = 0;
            this.hasAtama = false;
            this.partition = "";
        }

		/////////////////////////////////////
		// シャンテン数カウント関数
		/////////////////////////////////////
        public int getShanten()
        {
            int mentsu = this.mentsu;
            int kouho = this.kouho;

            if (mentsu + kouho > 4)
                kouho = 4 - mentsu;

			// 最大シャンテン数は8シャンテン
            int shanten = 8 - 2 * this.mentsu - kouho;

			// 雀頭がある場合はシャンテン数に-1
            if (this.hasAtama) shanten -= 1;

            return shanten;
        }

		/////////////////////////////////////
		// 手牌の分け方を決定する文字列関数
		/////////////////////////////////////
        public string getPartitionString()
        {
            string[] a = this.partition.Split(new char[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            string ret = "";
            foreach (string aa in a)
            {
                string[] sa = aa.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<int>ia = new List<int>();
                for (int i = 0; i < sa.Length; i++)
                {
                    int x;
                    int.TryParse(sa[i], out x);
                    ia.Add(x);
                }
                ret += MjUtil.ArrayToString(ia) + " / ";
            }
            return ret;
        }

		/////////////////////////////////////
		// インスタンスクローン生成関数
		/////////////////////////////////////
        public State Clone()
        {

			// 同じクラスインスタンスを簡易コピーする。
			// フィールドが参照型の場合、参照はコピーされますが、
			// 参照先オブジェクトはコピーされないため、元のオブジェクトとその複製は
			// 同じオブジェクトを参照します。
            return (State)this.MemberwiseClone();
        }

    }
}
