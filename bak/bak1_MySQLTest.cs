using UnityEngine;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Threading;

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
    private string SERVER = "localhost";
    private string DATABASE = "MySQL56";
    private string USERID = "root";
    private string PASSWORD = "iG54bv7yH*gP";
    private string PORT = "3306";
    private string TABLENAME = "hoge";

    void Start()
    {
        StartCoroutine(SelectData());
    }

    IEnumerator SelectData()
    {
        // SQLコマンドを作成
        string conCmd =
                "server=" + SERVER + ";" +
                "database=" + DATABASE + ";" +
                "userid=" + USERID + ";" +
//                "port=" + PORT + ";" +
                "password=" + PASSWORD;

        // SQLコネクションを作成
        MySqlConnection con = null;
        try
        {
            // SQLへ接続を実施する
            con = new MySqlConnection(conCmd);
            con.Open();
        }
        finally { }

/* TODO　よく分からんけど動かないからパッチングにfinallyした
        catch (MySqlException ex)
        {
            Debug.Log(ex.ToString());
        }
*/
        // データを追加するINSERT文は、通常はMySqlCommandクラスを使います。
        // コマンドを作成
        string selCmd = "SELECT * FROM TABLENAME LIMIT 0, 1200;";
        MySqlCommand cmd = new MySqlCommand(selCmd, con);

        // 非同期処理を開始
        IAsyncResult iAsync = cmd.BeginExecuteReader();

        // 非同期による全データ取得完了まで待ち合わせる
        while (!iAsync.IsCompleted)
        {
            yield return 0;
        }

        // 一応：
        // 上記のwhileで完了を待つ以外に、Beginによる非同期開始時にオーバーロードの
        // cmd.BeginExecuteReader(new AsyncCallback(AsyncCallbackMethod), cmd);
        // として非同期が完了したらコールバックメソッドを呼び、その中で完了EndExcuteReader
        // を呼ぶ事もできる。
        // 第一引数は、非同期処理が終わった際にコールバックされるメソッド。
        // 第二引数はIAsyncResult.AsyncStateオブジェクトとして取得できるオブジェクト。
        // で、コールバックメソッドはvoid AsyncCallbackMethod(IAsyncResult result)
        // resultには第二引数で指定したcmdが入る。

        // 非同期処理を完了する
        MySqlDataReader rdr = cmd.EndExecuteReader(iAsync);

        // 取得したデータからIDを得てみる
        while (rdr.Read())
        {
            if (!rdr.IsDBNull(rdr.GetOrdinal("ID")))
            {
                Debug.Log ( "ID : " + rdr.GetString ("ID") );
            }
        }

        // 全リソースをクローズおよび解放する
        rdr.Close();
        rdr.Dispose();
        con.Close();
        con.Dispose();
    }
}
