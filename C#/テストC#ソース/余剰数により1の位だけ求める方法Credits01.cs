using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Credits01 : MonoBehaviour {

    private Image image;
    private GameSystem gameSystem;

    public Sprite c00;           // クレジット数字「0」
    public Sprite c01;           // クレジット数字「1」
    public Sprite c02;           // クレジット数字「2」
    public Sprite c03;           // クレジット数字「3」
    public Sprite c04;           // クレジット数字「4」
    public Sprite c05;           // クレジット数字「5」
    public Sprite c06;           // クレジット数字「6」
    public Sprite c07;           // クレジット数字「7」
    public Sprite c08;           // クレジット数字「8」
    public Sprite c09;           // クレジット数字「9」

    void Start()
    {
        // Imageコンポ取得
        image = this.GetComponent<Image>();

        // ゲームシステムコンポ取得
        gameSystem = GameObject.FindWithTag("Canvas").GetComponent<GameSystem>();
    }

	void Update()
    {
        // クレジットの一の位を取得
        int creditFirst = (int)(gameSystem.coinVal) % 10;

        // クレジットの十の位を取得
        int creditFirst = (int)(gameSystem.coinVal / 10) % 10;

        // クレジットの百の位を取得
        int creditFirst = (int)(gameSystem.coinVal / 100) % 10;

        // クレジット表示一の位を表示
        switch(creditFirst)
        {
            case 0: // 0の場合
                image.sprite = c00;
                break;
            case 1: // 1の場合
                image.sprite = c01;
                break;
            case 2: // 2の場合
                image.sprite = c02;
                break;
            case 3: // 3の場合
                image.sprite = c03;
                break;
            case 4: // 4の場合
                image.sprite = c04;
                break;
            case 5: // 5の場合
                image.sprite = c05;
                break;
            case 6: // 6の場合
                image.sprite = c06;
                break;
            case 7: // 7の場合
                image.sprite = c07;
                break;
            case 8: // 8の場合
                image.sprite = c08;
                break;
            case 9: // 9の場合
                image.sprite = c09;
                break;
        }
	
	}
}
