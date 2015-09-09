using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour {

    public int idle = 1;   // アイドル状態判定フラグ（ONの場合はランダム画像表示）
    public int judge_Val;        // 出た目
    public int push_Val;         // 押した目
    public int result = 0;       // ジャンケンの判定結果（0:負け　1:勝ち　2:あいこ）
    private JankenArea jankenArea;  // ジャンケンエリア用フィールド
    private int coinval = 5; // 現コイン枚数（プロパティ）
    public int coinVal
    {
        get { return coinval; }
        set
        {
            // コイン数が上限99以上の場合は99に固定する
            if (value >= 100) value = 99;
            // コイン数が下限0以下の場合は0に固定する
            if (value <= 0) value = 0;
            coinval = value; 
        }
    }

    // -----------------------------------
    // 勝利時ルーレットによるクレジット加算メソッド
    // -----------------------------------
    public IEnumerator CreditsUp()
    {
        // クレジットを加算
        int crd = Random.Range(1, 6); // 1は含まれるが、6は含まれない
        if (1 == crd) coinval += 1;
        if (2 == crd) coinval += 2;
        if (3 == crd) coinval += 4;
        if (4 == crd) coinval += 7;
        if (5 == crd) coinval += 20;

        // 待ち時間（表示間隔のマージン値）
        yield return new WaitForSeconds(0.4f);

        // ゲームを再開するため押した目判定フラグをクリア
        push_Val = 0;
    }

}
