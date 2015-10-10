using UnityEngine;
using System.Collections;
using MySql.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Threading;
using System.Data; //TODO namespaceに無いと怒られる。多分nuget関係のせいだけど詳細不明。諦めた。

public class MySQLTest : MonoBehaviour
{
    // SQL Connector　.NETからMySQLデータベースにアクセスできるADO.NETドライバ
    // MySqlCommand　　データの追加
    // MySqlConnection コネクションの作成
    // DbConnection　データベースへの接続を作成
    // ExecuteReader　CommandTextをConnectionに送信し、SqlDataReaderを構築する
    // IAsyncResult　非同期処理の様々な状態（ステータス）を持つ
    // BeginExecuteReader　非同期でデータを読み出し、SqlDataReaderを構築する
    // EndExecuteReader　BeginExecuteReaderによる非同期を完了する

    /// <summary>SQLコマンド</summary>
    private string SERVER = "127.0.0.1";            // サーバアドレス
    private string DATABASE = "ubtSchema";          // データベース名
    private string USERID = "root";                 // ユーザ名
    private string PASSWORD = "iG54bv7yH*gP";       // パスワード
    private string PORT = "3306";                   // 接続ポート
    private string TABLENAME = "guidlist";          // テーブル名

    void Start()
    {
        SelectData();
    }

    void SelectData()
    {
        // SQLコマンドを作成
        string conCmd =
                "server=" + SERVER + ";" +
                "database=" + DATABASE + ";" +
                "userid=" + USERID + ";" +
                "port=" + PORT + ";" +
                "password=" + PASSWORD;

        // SQLコネクションを作成
        MySqlConnection con = null;
        try
        {
            // SQLへ接続を実施する
            con = new MySqlConnection(conCmd);
            con.Open();
        }
        finally
        { 
        }

        // SQL文と接続情報を指定し、データアダプタを作成
        MySqlDataAdapter da = new MySqlDataAdapter("select * from guidlist", con);
    }
}
