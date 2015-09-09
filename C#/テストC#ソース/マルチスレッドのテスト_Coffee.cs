using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // コレクションクラスの定義に必要
using System;                       // マルチスレッドで使う

public class Coffee : MonoBehaviour {

    public const int PAPER = 0;     // ペーパードリップ
    public const int SAIFON = 1;    // サイフォン

    public int num = 0;             // 人数
    public int hotwater = 0;        // お湯
    public int meshbeen = 0;        // 挽いた豆
    public int meshval = 0;         // メッシュ値
    public int coffee = 0;          // 完成品
    public int cup = 0;             // コーヒーカップ
    public int driptype = 0;        // ドリップタイプ（0:ペーパードリップ 1:サイフォン）
    public List<string> orderlist = new List<string>();

    delegate int Podmeth(int x);                    // デリゲート型
    delegate int Dripmeth(int x, int y, int z);     // デリゲート型
    Podmeth mydel;                                  // 湯沸しメソッド用フィールド
    Dripmeth mydrip;                                // ドリップメソッド用フィールド

	void Start ()
    {
        // 湯沸しとメッシュは普通同時進行だからマルチスレッドでコール
        mydel = new Podmeth(Podmethod);
        IAsyncResult podmeth = mydel.BeginInvoke(num, null, null);

        // メッシュ（豆を挽く）
        meshbeen = Beenmesh(num, meshval);

        // 湯沸し完了を待ち、湯と豆が揃ったらドリップ実施
        mydel.EndInvoke(podmeth);

        // ドリップ手法を選択
        if (PAPER == driptype) // ペーパードリップ
        {
            mydrip = new Dripmeth(PaperDripper);
            //            coffee = PaperDripper(meshbeen, hotwater, num);
        }
        else                   // サイフォン
        {
            mydrip = new Dripmeth(SifonDripper);
            //            coffee = SifonDripper(meshbeen, hotwater, num);                
        }

        // ドリップ実施
        coffee = mydrip(meshbeen, hotwater, num);

        // サーブ
        Server(coffee, num, orderlist);
    }

    // --------------------------
    // 湯沸し器
    // --------------------------
    int Podmethod(int num)
    {
        // 1人分200ml x 人数分沸かす（整数"200"は固定）
        int hotwater = num * 200;

        // 沸くまでの待ち時間をコルーチン化（実数リテラルは固定）
//        StartCoroutine(Wait(1.2f));よく考えたらコルーチンはメインスレッドでしか使えなかったわ

        return hotwater;
    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    // --------------------------
    // メッシュ
    // --------------------------
    int Beenmesh(int num, int meshval)
    {
        // 1人分の豆20g x 人数分を指定メッシュで挽く（整数"20"は固定）
        int meshbeen = (num * 20) * meshval;

        return meshbeen;
    }

    // --------------------------
    // ドリッパー（ペーパードリップ）
    // --------------------------
    int PaperDripper(int been, int hotwater, int num)
    {
        // コーヒーを淹れる
        int coffee = (hotwater / been) * (num * 20);

        return coffee;
    }

    // --------------------------
    // ドリッパー（サイフォンドリップ）
    // --------------------------
    int SifonDripper(int been, int hotwater, int num)
    {
        // コーヒーを淹れる
        int coffee = (hotwater / been) * (num * 20);

        return coffee;
    }

    // --------------------------
    // サーバ
    // --------------------------
    void Server(int coffee, int num, List<string> orderlist)
    {
        int cups = coffee / num;

        // 出来たコーヒーを人数分カップに分ける作業
        for (int x = 0; x < cups; x++)
        {
            orderlist.Add("コーヒー");
        }

    }
}
