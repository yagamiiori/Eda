using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;   // コレクションクラスの定義に必要
using System.Linq;
using System;

public class OnClickOkForLogin :
    MonoBehaviour,
    IMessageWriteToMW                                 // メッセージウィンドウ書き込みIF
{
    public AudioClip clickSE;                         // OKボタンクリックSE
    public InputField nameField;                      // 名前のインプットフィールド
    private GameManager gameManager;                  // マネージャコンポ
    private GameObject warningParentGO;               // メッセージウィンドウCanvas
    private Text warningText;                         // メッセージウィンドウのTextコンポ
    private bool IsWindow = false;                    // メッセージウィンドウ表示有無判定フラグ
    private string nextScene = "UnitSelect";          // 遷移先シーン名
    private AudioSource audioCompo;                   // オーディオコンポ

    /// <summary>コンストラクタ</summary>
    private OnClickOkForLogin() { }

    void Start()
    {
        // マネージャコンポ取得
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // 名前入力フィールド取得
        nameField = GameObject.FindWithTag("Login_InputField_Name").GetComponent<InputField>();

        // ワーニングウィンドウの親GOをワーニングウィンドウ管理クラスより取得
        warningParentGO = GameObject.Find("Canvas_WarningWindow").GetComponent<WarningWindowActiveManager>().warningWindowParentGO;

        // オーディオコンポ取得とOKボタンクリック時SEの設定
        audioCompo = GameObject.Find("PlayersParent").transform.FindChild("SEPlayer").gameObject.GetComponent<AudioSource>();
        // TODO 本当はリクワイヤードコンポ属性を使うべき。上手く動いてくれなかったのでとりあえず
        if (null == audioCompo) audioCompo = GameObject.Find("PlayersParent").transform.FindChild("SEPlayer").gameObject.GetComponent<AudioSource>();
        clickSE = (AudioClip)Resources.Load("Sounds/SE/Click7");
    }

    // =====================================
    // メッセージウィンドウ書き込みIF
    // メッセージウィンドウのTextコンポに文字を書き込む
    // =====================================
    public void MessageWriteToWindow(string a)
    {
        // メッセージウィンドウをアクティブ化
        warningParentGO.SetActive(true);

        // テキストコンポを取得
        warningText = warningParentGO.transform.FindChild("WarningText").gameObject.GetComponent<Text>();

        // メッセージウィンドウ表示有無判定フラグを変更
        IsWindow = true;

        // メッセージ表示
        warningText.text = a;
    }

    // -------------------------------------------------------------------
    // OKボタンがクリックした時にOKボタンのOnClickからコールされ、
    // ロビーへ遷移する。
    // -------------------------------------------------------------------
    public void OnClickOK()
    {
        // メッセージウィンドウ未表示の場合
        if (!IsWindow)
        {
            // IDフィールドに何も入力されていない場合
            if ("" == nameField.text)
            {
                MessageWriteToWindow("未入力。\nログインIDを入力して下さい。");
                return;
            }
            // 入力されたIDが「NameLess」の場合
            else if ("NameLess" == nameField.text)
            {
                gameManager.userName = "NameLess";
            }
            // IDが正常に入力された場合
            else
            {
                // LinkToXMLクラスを作成
                var t = new AppSettings();

                string xmlFile = "sample.xml";
                if (false == System.IO.File.Exists(xmlFile))
                {
                    // ファイルがなければ作成する
                    t.CreateXmlFile();
                }

                // ID検索して一致したらロードする
                // 処理はまだ書いてない
                // 一致するIDがなければエラー文をメッセージウィンドウで表示
                // 入力されたIDから名前を逆引きしてGMのフィールドに格納
                gameManager.userName = nameField.text.ToString();
            }

            // 次回からの入力を自動化するため、入力された文字列をファイルに書き出し
            var streamWriter = new StreamWriterSingleLine();
            string fileName = "iid.txt";
            string filetxt = nameField.text;
            bool result = streamWriter.WriteToStream(fileName, filetxt);

            // シーン遷移メソッドコール
            NextScene();
        }
        // メッセージウィンドウが表示されている場合
        else
        {
            // メッセージウィンドウを非アクティブ化
            warningParentGO.SetActive(false);

            // メッセージウィンドウ表示有無判定フラグを変更
            IsWindow = false;
        }
    }

    // -------------------------------
    // シーン遷移メソッド
    // -------------------------------
    public void NextScene()
    {
        // クリックSEを設定および再生
        audioCompo.PlayOneShot(clickSE);

        // Scene遷移
        // ﾌｪｰﾄﾞｱｳﾄ時間、ﾌｪｰﾄﾞ中待機時間、ﾌｪｰﾄﾞｲﾝ時間、ｶﾗｰ、遷移先Pos情報(Vector3)、遷移先ｼｰﾝ
        gameManager.GetComponent<FadeToScene>().FadeOut(0.1f, 0.4f, 0.1f, Color.black, nextScene);
    }
}
