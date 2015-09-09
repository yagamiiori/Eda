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

    // ----------------------------------------------------------------
    // 初回起動関数
    // ----------------------------------------------------------------
    void Start()
    {
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
