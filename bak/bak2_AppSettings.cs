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
    private int[] unitidInXml = new int[16];
    /// <summary>クラス</summary>
    private int[] classidInXml = new int[16];
    /// <summary>ユニットにつけた名前</summary>
    private string[] unitNameInXml = new string[16];
    /// <summary>アビリティ</summary>
    private int[] abilityInXml = new int[16];
    /// <summary>エレメント</summary>
    private int[] elementInXml = new int[16];

    /// <summary>コンスタント</summary>
    public AppSettings() { }

    void Start()
    {
        // マネージャコンポを取得
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// GUIDフィールド設定メソッド
    /// <para>　GUIDをxmlより取得し、LoginシーンのGUIDフィールドに設定するため</para>
    /// <para>　コール元メソッドへGUID値を返す。</para>
    /// </summary>
    public string GuidSetForInputFieldInLogin()
    {
        // xmlファイルを取得
        XElement doc = XElement.Load("var.xml");

        // 要素に対するクエリを作成
        var query = from p in doc.Elements("UserParams")
                    select new
                    {
                        // 各要素とそれに対応する変数を設定
                        _guid = (string)p.Element("Guid")
                    };

        // xmlより要素を取得する
        foreach (var elem in query)
        {
            guid = elem._guid;
        }
        return guid;
    }

    /// <summary>
    /// ユーザ関連パラメータ取得メソッド
    /// <para>　ユーザ名やGUID等ユーザ関連のパラメータをxmlより取得する。</para>
    /// </summary>
    public void UserStatusLoadFromXml()
    {
        // マネージャコンポを取得
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // xmlファイルを取得
        XElement doc = XElement.Load("var.xml");

        // 要素に対するクエリを作成
        var query = from p in doc.Elements("UserParams")
        select new
        {
            // 各要素とそれに対応する変数を設定
            _username = (string)p.Element("UserName"),
            _guid = (string)p.Element("Guid")
        };

        // xmlより要素を取得する
        foreach (var elem in query)
        {
            userName = elem._username;
            guid = elem._guid;
        }

        // xmlより取得したユーザー名とGUIDをゲームマネージャーコンポに設定する
        gameManager.userName = userName;
        gameManager.userGuid = guid;
    }

    /// <summary>
    /// ユニットリスト取得メソッド
    /// <para>　ユニットリストをxmlより取得する。</para>
    /// <para>　UnitStateSetFromXmlとセットで使用する。</para>
    /// </summary>
    public void UnitStateLoadFromXml()
    {
        // xmlファイルを取得
        XElement doc = XElement.Load("var.xml");

        // 要素に対するクエリを作成
        var query0 = from p
                        in doc.Elements("UnitStatus_0")
                        select new
                        {
                            // 各要素とそれに対応する変数を設定
                            _unitId = (int)p.Element("UnitID"),
                            _unitClass = (int)p.Element("UnitClass"),
                            _unitName = (string)p.Element("UnitName"),
                            _unitAbility = (int)p.Element("UnitAbility1"),
                            _unitElement = (int)p.Element("UnitElement")
                        };
        // xmlより要素を取得する
        foreach (var elem in query0)
        {
            unitidInXml[0] = elem._unitId;
            classidInXml[0] = elem._unitClass;
            unitNameInXml[0] = elem._unitName;
            abilityInXml[0] = elem._unitAbility;
            elementInXml[0] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query1 = from p
                        in doc.Elements("UnitStatus_1")
                    select new
                    {
                        // 各要素とそれに対応する変数を設定
                        _unitId = (int)p.Element("UnitID"),
                        _unitClass = (int)p.Element("UnitClass"),
                        _unitName = (string)p.Element("UnitName"),
                        _unitAbility = (int)p.Element("UnitAbility1"),
                        _unitElement = (int)p.Element("UnitElement")
                    };
        // xmlより要素を取得する
        foreach (var elem in query1)
        {
            unitidInXml[1] = elem._unitId;
            classidInXml[1] = elem._unitClass;
            unitNameInXml[1] = elem._unitName;
            abilityInXml[1] = elem._unitAbility;
            elementInXml[1] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query2 = from p
                        in doc.Elements("UnitStatus_2")
                     select new
                     {
                         // 各要素とそれに対応する変数を設定
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xmlより要素を取得する
        foreach (var elem in query2)
        {
            unitidInXml[2] = elem._unitId;
            classidInXml[2] = elem._unitClass;
            unitNameInXml[2] = elem._unitName;
            abilityInXml[2] = elem._unitAbility;
            elementInXml[2] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query3 = from p
                        in doc.Elements("UnitStatus_3")
                     select new
                     {
                         // 各要素とそれに対応する変数を設定
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xmlより要素を取得する
        foreach (var elem in query3)
        {
            unitidInXml[3] = elem._unitId;
            classidInXml[3] = elem._unitClass;
            unitNameInXml[3] = elem._unitName;
            abilityInXml[3] = elem._unitAbility;
            elementInXml[3] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query4 = from p
                        in doc.Elements("UnitStatus_4")
                     select new
                     {
                         // 各要素とそれに対応する変数を設定
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xmlより要素を取得する
        foreach (var elem in query4)
        {
            unitidInXml[4] = elem._unitId;
            classidInXml[4] = elem._unitClass;
            unitNameInXml[4] = elem._unitName;
            abilityInXml[4] = elem._unitAbility;
            elementInXml[4] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query5 = from p
                        in doc.Elements("UnitStatus_5")
                     select new
                     {
                         // 各要素とそれに対応する変数を設定
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xmlより要素を取得する
        foreach (var elem in query5)
        {
            unitidInXml[5] = elem._unitId;
            classidInXml[5] = elem._unitClass;
            unitNameInXml[5] = elem._unitName;
            abilityInXml[5] = elem._unitAbility;
            elementInXml[5] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query6 = from p
                        in doc.Elements("UnitStatus_6")
                     select new
                     {
                         // 各要素とそれに対応する変数を設定
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xmlより要素を取得する
        foreach (var elem in query6)
        {
            unitidInXml[6] = elem._unitId;
            classidInXml[6] = elem._unitClass;
            unitNameInXml[6] = elem._unitName;
            abilityInXml[6] = elem._unitAbility;
            elementInXml[6] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query7 = from p
                        in doc.Elements("UnitStatus_7")
                     select new
                     {
                         // 各要素とそれに対応する変数を設定
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xmlより要素を取得する
        foreach (var elem in query7)
        {
            unitidInXml[7] = elem._unitId;
            classidInXml[7] = elem._unitClass;
            unitNameInXml[7] = elem._unitName;
            abilityInXml[7] = elem._unitAbility;
            elementInXml[7] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query8 = from p
                        in doc.Elements("UnitStatus_8")
                     select new
                     {
                         // 各要素とそれに対応する変数を設定
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xmlより要素を取得する
        foreach (var elem in query8)
        {
            unitidInXml[8] = elem._unitId;
            classidInXml[8] = elem._unitClass;
            unitNameInXml[8] = elem._unitName;
            abilityInXml[8] = elem._unitAbility;
            elementInXml[8] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query9 = from p
                        in doc.Elements("UnitStatus_9")
                     select new
                     {
                         // 各要素とそれに対応する変数を設定
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xmlより要素を取得する
        foreach (var elem in query9)
        {
            unitidInXml[9] = elem._unitId;
            classidInXml[9] = elem._unitClass;
            unitNameInXml[9] = elem._unitName;
            abilityInXml[9] = elem._unitAbility;
            elementInXml[9] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query10 = from p
                        in doc.Elements("UnitStatus_10")
                     select new
                     {
                         // 各要素とそれに対応する変数を設定
                         _unitId = (int)p.Element("UnitID"),
                         _unitClass = (int)p.Element("UnitClass"),
                         _unitName = (string)p.Element("UnitName"),
                         _unitAbility = (int)p.Element("UnitAbility1"),
                         _unitElement = (int)p.Element("UnitElement")
                     };
        // xmlより要素を取得する
        foreach (var elem in query10)
        {
            unitidInXml[10] = elem._unitId;
            classidInXml[10] = elem._unitClass;
            unitNameInXml[10] = elem._unitName;
            abilityInXml[10] = elem._unitAbility;
            elementInXml[10] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query11 = from p
                        in doc.Elements("UnitStatus_11")
                      select new
                      {
                          // 各要素とそれに対応する変数を設定
                          _unitId = (int)p.Element("UnitID"),
                          _unitClass = (int)p.Element("UnitClass"),
                          _unitName = (string)p.Element("UnitName"),
                          _unitAbility = (int)p.Element("UnitAbility1"),
                          _unitElement = (int)p.Element("UnitElement")
                      };
        // xmlより要素を取得する
        foreach (var elem in query11)
        {
            unitidInXml[11] = elem._unitId;
            classidInXml[11] = elem._unitClass;
            unitNameInXml[11] = elem._unitName;
            abilityInXml[11] = elem._unitAbility;
            elementInXml[11] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query12 = from p
                        in doc.Elements("UnitStatus_12")
                      select new
                      {
                          // 各要素とそれに対応する変数を設定
                          _unitId = (int)p.Element("UnitID"),
                          _unitClass = (int)p.Element("UnitClass"),
                          _unitName = (string)p.Element("UnitName"),
                          _unitAbility = (int)p.Element("UnitAbility1"),
                          _unitElement = (int)p.Element("UnitElement")
                      };
        // xmlより要素を取得する
        foreach (var elem in query12)
        {
            unitidInXml[12] = elem._unitId;
            classidInXml[12] = elem._unitClass;
            unitNameInXml[12] = elem._unitName;
            abilityInXml[12] = elem._unitAbility;
            elementInXml[12] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query13 = from p
                        in doc.Elements("UnitStatus_13")
                      select new
                      {
                          // 各要素とそれに対応する変数を設定
                          _unitId = (int)p.Element("UnitID"),
                          _unitClass = (int)p.Element("UnitClass"),
                          _unitName = (string)p.Element("UnitName"),
                          _unitAbility = (int)p.Element("UnitAbility1"),
                          _unitElement = (int)p.Element("UnitElement")
                      };
        // xmlより要素を取得する
        foreach (var elem in query13)
        {
            unitidInXml[13] = elem._unitId;
            classidInXml[13] = elem._unitClass;
            unitNameInXml[13] = elem._unitName;
            abilityInXml[13] = elem._unitAbility;
            elementInXml[13] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query14 = from p
                        in doc.Elements("UnitStatus_14")
                      select new
                      {
                          // 各要素とそれに対応する変数を設定
                          _unitId = (int)p.Element("UnitID"),
                          _unitClass = (int)p.Element("UnitClass"),
                          _unitName = (string)p.Element("UnitName"),
                          _unitAbility = (int)p.Element("UnitAbility1"),
                          _unitElement = (int)p.Element("UnitElement")
                      };
        // xmlより要素を取得する
        foreach (var elem in query14)
        {
            unitidInXml[14] = elem._unitId;
            classidInXml[14] = elem._unitClass;
            unitNameInXml[14] = elem._unitName;
            abilityInXml[14] = elem._unitAbility;
            elementInXml[14] = elem._unitElement;
        }

        // 要素に対するクエリを作成
        var query15 = from p
                        in doc.Elements("UnitStatus_15")
                      select new
                      {
                          // 各要素とそれに対応する変数を設定
                          _unitId = (int)p.Element("UnitID"),
                          _unitClass = (int)p.Element("UnitClass"),
                          _unitName = (string)p.Element("UnitName"),
                          _unitAbility = (int)p.Element("UnitAbility1"),
                          _unitElement = (int)p.Element("UnitElement")
                      };
        // xmlより要素を取得する
        foreach (var elem in query15)
        {
            unitidInXml[15] = elem._unitId;
            classidInXml[15] = elem._unitClass;
            unitNameInXml[15] = elem._unitName;
            abilityInXml[15] = elem._unitAbility;
            elementInXml[15] = elem._unitElement;
        }
    }

    /// <summary>
    /// ユニットリスト設定メソッド
    /// <para>　ユニットリスト取得メソッド（UnitStateLoadFromXml）でxmlより取得した</para>
    /// <para>　ユニット名やエレメント等を、ゲームマネージャー内のユニットリストに追加する。</para>
    /// <para>　また、ユニットGOを作成しUnitStateコンポ内のフィールドへの設定も行う。</para>
    /// <para>　UnitStateLoadFromXmlとセットで使用する。</para>
    /// </summary>
    public void UnitStateSetFromXml()
    {
        for (int i = 0; 16 > i; i++)
        {
            // ユニットステート用GOのインスタンス化とコンポ取得
            GameObject unitGO = Instantiate(Resources.Load("UnitGO"), transform.position, Quaternion.identity) as GameObject;
            UnitState unitstate = unitGO.GetComponent<UnitState>();
            unitstate.unitID = unitidInXml[i];
            unitstate.classType = classidInXml[i];
            unitstate.unitName = unitNameInXml[i];
            unitstate.ability_A = abilityInXml[i];
            unitstate.element = elementInXml[i];
            gameManager.unitStateList.Add(unitstate);
        }
        /*
        // IDをUnitStateとユニットリストに設定
        foreach (var id in unitidInXml)
        {
            // ユニットステート用GOのインスタンス化とコンポ取得
            unitGO = Instantiate(Resources.Load("UnitGO"), transform.position, Quaternion.identity) as GameObject;
            UnitState unitstate = unitGO.GetComponent<UnitState>();
            unitstate.unitID = id;
            gameManager.unitStateList.Add(unitstate);
        }
         */
    }

        /// <summary>
    /// xmlファイル生成メソッド
    /// <para>　取得するxmlが存在しない場合に生成を行うメソッド。</para>
    /// </summary>
    public void CreateXmlFile()
    {
        // xmlインスタントを作成
        XmlDocument document = new XmlDocument();
        XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
        XmlElement root = document.CreateElement("UBTProject");  // ルート要素
        document.AppendChild(declaration);                       // 指定したノードを子ノードとして追加
        document.AppendChild(root);

        // ユーザー情報の要素を作成
        XmlElement elementUserPrm = document.CreateElement("UserParams");
        root.AppendChild(elementUserPrm);
        XmlElement userName = document.CreateElement("UserName");
        userName.InnerText = "NONE";
        elementUserPrm.AppendChild(userName);
        XmlElement guID = document.CreateElement("Guid");
        guID.InnerText = "NONE";
        elementUserPrm.AppendChild(guID);

        // ユニットリストの要素を作成
        for (int i = 0; 16 > i; i++)
        {
            XmlElement elementUnitSts0 = document.CreateElement("UnitStatus_" + i.ToString());
            root.AppendChild(elementUnitSts0);
            XmlElement UnitID_0 = document.CreateElement("UnitID");
            UnitID_0.InnerText = "99";
            elementUnitSts0.AppendChild(UnitID_0);
            XmlElement UnitClass_0 = document.CreateElement("UnitClass");
            UnitClass_0.InnerText = "99";
            elementUnitSts0.AppendChild(UnitClass_0);
            XmlElement UnitName_0 = document.CreateElement("UnitName");
            UnitName_0.InnerText = "NONE";
            elementUnitSts0.AppendChild(UnitName_0);
            XmlElement UnitAbility1_0 = document.CreateElement("UnitAbility1");
            UnitAbility1_0.InnerText = "99";
            elementUnitSts0.AppendChild(UnitAbility1_0);
            XmlElement UnitAbility2_0 = document.CreateElement("UnitAbility2");
            UnitAbility2_0.InnerText = "99";
            elementUnitSts0.AppendChild(UnitAbility2_0);
            XmlElement UnitElement_0 = document.CreateElement("UnitElement");
            UnitElement_0.InnerText = "99";
            elementUnitSts0.AppendChild(UnitElement_0);
        }
        // ファイルへ保存する
        document.Save("var.xml");
    }
}
