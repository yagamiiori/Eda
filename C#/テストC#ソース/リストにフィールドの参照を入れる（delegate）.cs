using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // コレクションクラスの定義に必要
using System.Linq;

////////////////////////////////////////////////////////////////////////////////////////
//　関数名：デッキ構築クラス（DeckList）
//　機能：初期デッキを構築する
//　継承：MonoBehaviour
//　種別：通常クラス
//　アタッチ先：エンプティオブジェクト
//　保持メソッド：Start、indexPair
//　
//　リダイレクト：
//　　MainCamera：本オブジェクトの位置情報(position)を変更するため
//
//　詳細：
//　　　　ランダム整数を用いて各種カードをランダムにデッキに積む。
//　　　　デッキはListに格納するものとし、Listのインデックスの順に
//　　　　デッキの下から上へ各カード（基底クラス）のインスタンスを
//　　　　格納する。
//　　　　また、格納したデッキリストの読みだしたxxxxxクラス内にて行う。
//
//　履歴：
//　　　　14.6.6 初版
//　　　　14.xx.xx 仕変1.1.2 xxxx対応
//　　　　14.xx.xx 仕変2.1.4 xxxx改修
//
////////////////////////////////////////////////////////////////////////////////////////
public class DeckList : MonoBehaviour
{
    private static DeckList mInstance;                // インスタンス
    public static int[] cardlist = {                  // カードリスト（全16枚）ボツ
                                       Def.SOLDLER,
                                       Def.SOLDLER,
                                       Def.SOLDLER,
                                       Def.SOLDLER,
                                       Def.SOLDLER,
                                       Def.CLOWN,
                                       Def.CLOWN,
                                       Def.KNIGHT,
                                       Def.KNIGHT,
                                       Def.PRIESTESS,
                                       Def.PRIESTESS,
                                       Def.WIZARD,
                                       Def.WIZARD,
                                       Def.GENERAL,
                                       Def.MINLSTER,
                                       Def.PRINCESS
                                   };

    public delegate int CardHandler();                              // デリゲート型の宣言
    public List<int> list = new List<int>();                        // シャッフル用リスト
    public List<CardHandler> listRef = new List<CardHandler>();     // 参照用

    // ----------------------------------------------------------------
    // 初回起動関数
    // ----------------------------------------------------------------
    void Start()
    {
        // シャッフル用リストを構築
        list.Add(Def.SOLDLER);
        list.Add(Def.SOLDLER);
        list.Add(Def.SOLDLER);
        list.Add(Def.SOLDLER);
        list.Add(Def.SOLDLER);
        list.Add(Def.CLOWN);
        list.Add(Def.CLOWN);
        list.Add(Def.KNIGHT);
        list.Add(Def.KNIGHT);
        list.Add(Def.PRIESTESS);
        list.Add(Def.PRIESTESS);
        list.Add(Def.WIZARD);
        list.Add(Def.WIZARD);
        list.Add(Def.GENERAL);
        list.Add(Def.MINLSTER);
        list.Add(Def.PRINCESS);

        // 参照用リストにシャッフル用リストを挿入
        listRef.Add(delegate { return list[0]; });
        listRef.Add(delegate { return list[1]; });
        listRef.Add(delegate { return list[2]; });
        listRef.Add(delegate { return list[3]; });
        listRef.Add(delegate { return list[4]; });
        listRef.Add(delegate { return list[5]; });
        listRef.Add(delegate { return list[6]; });
        listRef.Add(delegate { return list[7]; });
        listRef.Add(delegate { return list[8]; });
        listRef.Add(delegate { return list[9]; });
        listRef.Add(delegate { return list[10]; });
        listRef.Add(delegate { return list[11]; });
        listRef.Add(delegate { return list[12]; });
        listRef.Add(delegate { return list[13]; });
        listRef.Add(delegate { return list[14]; });
        listRef.Add(delegate { return list[15]; });
        
        // シャッフル関数をコール
        Shuffle(cardlist);
    }
    // ----------------------------------------------------------------
    // シャッフル処理実施関数（Fisher-Yates）
    // ----------------------------------------------------------------
    private static void Shuffle(int[] c)
    {
        int[] cardlist = c;             //引数の配列を格納
        var rnd = new System.Random();  //ランダムクラスをインスタンス化

        var n = cardlist.Length;        //インデックス数を取得

        // インデックス数分をループ（n-1しないと(n - cnt)の結果が0になる）
        for (int cnt = n-1; cnt > 0; cnt--)
        {
            // NextDouble()は0～1間の実数リテラルを得る
            int j = (int)(rnd.NextDouble() * (n - cnt));
            int tmp = cardlist[cnt];
            cardlist[cnt] = cardlist[j];
            cardlist[j] = tmp;
        }
    }
}


