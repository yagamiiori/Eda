using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// LINQ to XMLクラス
/// <para>　LINQ to XMLによりxml内のデータを操作する。</para>
/// </summary>
public class AppSettings : MonoBehaviour
{
    /// <summary>マネージャーコンポ</summary>
    private GameManager gameManager;
    /// <summary>ユーザ名</summary>
    private string userName;
    /// <summary>GUID</summary>
    private string guid;
    /// <summary>ゲーム言語</summary>
    private int language;
    /// <summary>ユニットID</summary>
    private int unitidInXml;
    /// <summary>クラス</summary>
    private int classidInXml;
    /// <summary>ユニットにつけた名前</summary>
    private string unitNameInXml;
    /// <summary>アビリティ</summary>
    private int abilityInXml;
    /// <summary>エレメント</summary>
    private int elementInXml;

    /// <summary>コンスタント</summary>
    private AppSettings() { }

    void Start()
    {
        // マネージャコンポを取得
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        ReadStartOfUnitListInXml();
    }

    /// <summary>
    /// ユーザ関連パラメータ取得メソッド
    /// <para>　ユーザ名やGUID等ユーザ関連のパラメータをxmlより取得する。</para>
    /// </summary>
    private void ReadStartOfUserParamInXml()
    {
        
        // xmlファイルを取得
        XElement doc = XElement.Load("var.xml");

        // 要素に対するクエリを作成
        // Elements:「子要素」を取得する
        var query = from p in doc.Elements("UserParams")
        select new
        {
            // 各要素とそれに対応する変数を設定
            _username = (string)p.Element("UserName"),
            _guid = (string)p.Element("GUID")
        };

        // xmlより要素を取得する
        foreach (var elem in query)
        {
            userName = elem._username;
            guid = elem._guid;
        }
    }

    /// <summary>
    /// ユニットリスト取得メソッド
    /// <para>　ユニットリストをxmlより取得する。</para>
    /// </summary>
    private void ReadStartOfUnitListInXml()
    {
        // xmlファイルを取得
        XElement doc = XElement.Load("var.xml");

        // 要素に対するクエリを作成
        var query = from p
                        in doc.Elements("UnitStatus_1")
                        select new
                        {
                            // 各要素とそれに対応する変数を設定
                            _unitId = (int)p.Element("UnitID"),
                            _unitClass = (int)p.Element("UnitClass"),
                            _unitName = (string)p.Element("UnitName"),
                            _unitAbility = (int)p.Element("UnitAbility"),
                            _unitElement = (int)p.Element("UnitElement")
                        };

        // xmlより要素を取得する
        foreach (var elem in query)
        {
            unitidInXml = elem._unitId;
            classidInXml = elem._unitClass;
            unitNameInXml = elem._unitName;
            abilityInXml = elem._unitAbility;
            elementInXml = elem._unitElement;
        }
    }

    /// <summary>
    /// xmlユニットリスト→ゲームマネージャーユニットリスト設定メソッド
    /// <para>　xmlより取得したユニットリスト(エレメントやユニット名等)を、</para>
    /// <para>　ゲームマネージャー内のユニットリストに載せ換える。</para>
    /// </summary>
    private void SettingsUnitListForXml()
    {
    }

        /// <summary>
    /// xmlファイル生成メソッド
    /// <para>　取得するxmlがない場合に生成を行うメソッド。</para>
    /// </summary>
    private void CreateXmlFile()
    {
        // xmlインスタントを作成
        XmlDocument document = new XmlDocument();
        XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
        XmlElement root = document.CreateElement("root");  // ルート要素
        document.AppendChild(declaration);                 // 指定したノードを子ノードとして追加
        document.AppendChild(root);

        // 要素を作成
        XmlElement element = document.CreateElement("element");
        element.InnerText = "text";                        // 要素の内容
        element.SetAttribute("attribute", "256");          // 要素に属性を設定
        root.AppendChild(element);

        // ファイルとして保存する
        document.Save("sample.xml");
    }
}
