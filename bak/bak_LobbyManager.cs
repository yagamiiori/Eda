using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;    //CP専用Hashtable

// =======================================================================================
// ロビーシーンマネージャー
//
// ロビーシーンで起動され、ロビーに入室する
// ルームへの入室はバトルフィールドシーンで行う。
//
// 入室ボタンは取得後、即非アクティブ化しロビー入室確定後に
// アクティブ化を行う（ロビー入室前にボタンを押される事を抑止する）。
//
// ルームCP設定契機：バトルフィールドシーンにてCreateRoomの直前
// プレイヤーCP設定契機：ロビーシーン起動直後のStartメソッド内
//
// 【ロビーおよびルーム入室フロー (今んとこ) 】
// Lobbyシーン起動直後にロビー入室
// ↓
// 入室ボタン押すとLoadLevelでバトルフィールドシーンへ遷移
// ↓
// バトルフィールドシーン起動直後にCreateRoom+JoinRoomで入室
//
// =======================================================================================
public class LobbyManager : MonoBehaviour
{
    private GameManager gameManager;         // マネージャコンポ
    private GameObject canVas;               // ゲームオブジェクト"Canvas"
    private GameObject insideRoomButton;     // 入室ボタンオブジェクト
    private Text playerAllText;              // 全ユーザー数表示用テキストコンポ
    private Text roomAllText;                // 全ルーム数表示用テキストコンポ

    // ---- プレイヤーCP用フィールド ----
    public string name = "Guest";            // ユーザー名
    public string rank = "";                 // ランク
    public int battlePoint = 0;              // バトルポイント
    public int battleCnt = 0;                // 戦闘回数

    void Awake()
    {
        // ロビーに入室するためマスターサーバーへ接続
        PhotonNetwork.ConnectUsingSettings("v0.1");
    }

	void Start ()
    {
        // マネージャコンポ取得
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        // ゲームオブジェクト"Canvas"取得
        canVas = GameObject.FindWithTag("Canvas");

        // 入室ボタンオブジェクト取得し非アクティブ化
        insideRoomButton = GameObject.FindWithTag("InsideRoomButton");
        insideRoomButton.SetActive(false);

        // 全ユーザ数表示Textコンポを取得
        playerAllText = GameObject.FindWithTag("Roby_PlayersNum").GetComponent<Text>();

        // 全ルーム数表示Textコンポを取得
        roomAllText = GameObject.FindWithTag("Roby_RoomsNum").GetComponent<Text>();
        

        // プレイヤーCP要素を定義
        gameManager.customPropeties = new Hashtable()
                                        {
                                            { "UserName", name },
                                            { "BP", battlePoint },
                                            { "BattleCnt", battleCnt },
                                            { "Rank", rank }
                                        };

        // プレイヤーCPを設定
        PhotonNetwork.SetPlayerCustomProperties(gameManager.customPropeties);

        // ゲーム中プレイヤー数取得メソッドをコール
        StartCoroutine(GetPlayerAll());
	}

    // -------------------------------------------------------------
    // マスターサーバーのロビーに入った場合にコールされる
    // ロビーに入ったらとりあえず部屋を生成する
    // -------------------------------------------------------------
    void OnJoinedLobby()
    {
        Debug.Log("ロビーに入室");

        // ロビー入室確定後、入室ボタンをアクティブ化
        insideRoomButton.SetActive(true);
    }

    // -------------------------------------------------------------------
    // 入室ボタンがクリックした時に入室ボタンのOnClickからコールされ、
    // バトルフィールドへ遷移する。
    // -------------------------------------------------------------------
    public void RoomIn()
    {
        Application.LoadLevel("BattleStage");
    }

    // -------------------------------------------------------------------
    // ユニット編成ボタンがクリックした時にユニット編成ボタンのOnClickから
    // コールされ、ユニット編成シーンへ遷移する。
    // ★★★★★★★★★　ボツメソッド　★★★★★★★★★★
    // -------------------------------------------------------------------
    public void UnitForm()
    {
        // メインサーバから一度切断
        PhotonNetwork.Disconnect();
        Application.LoadLevel("UnitForm");
    }

    // -------------------------------------------------------------
    // 全プレイヤー数取得メソッド
    // 全ゲーム中のプレイヤー数を取得し、Textコンポに表示する
    // Start()メソッドからコールされ、コルーチンとして定期的に更新する
    // -------------------------------------------------------------
    private IEnumerator GetPlayerAll()
    {
        while (true)
        {
            // プレイヤー数を取得
            playerAllText.text = PhotonNetwork.countOfPlayers.ToString();

            // ルーム数を取得
            roomAllText.text = PhotonNetwork.countOfRooms.ToString();

            // 一時停止
            yield return new WaitForSeconds(20.0f);
        }
    }
}
