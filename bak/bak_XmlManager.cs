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
public class XmlManager : MonoBehaviour
{
    /// <summary>マネージャーコンポ</summary>
    private GameManager gameManager;
    /// <summary>ゲーム言語</summary>
    private int language;
    /// <summary>XMLから読み出したユニットID</summary>
    private int[] unitidInXml = new int[16];
    /// <summary>XMLから読み出したクラス</summary>
    private int[] classidInXml = new int[16];
    /// <summary>XMLから読み出したユニット名</summary>
    private string[] unitNameInXml = new string[16];
    /// <summary>XMLから読み出したアビリティ</summary>
    private int[] abilityInXml = new int[16];
    /// <summary>XMLから読み出したエレメント</summary>
    private int[] elementInXml = new int[16];
    /// <summary>永続オブジェクト有無（インスペクタから永続オブジェクトである事を可視化するために設定）</summary>
    [SerializeField]
    private bool isDontDestroy = true;

    /// <summary>コンスタント</summary>
    public XmlManager() { }

    void Awake()
    {
        if (isDontDestroy)
        {
            // TODO Tag + FindGameObjectsWithTagによる検索でなければ個数が取れない。
            // null == Find("Canvas_FadeDisplay")　では自分もFind対象になるため、Find対象自身の中で行うとnullになるケースが無い
            // すでにシーンに画面フェードオブジェクトが存在する場合は重複を抑止するため本オブジェクトを破棄
            if (1 < GameObject.FindGameObjectsWithTag("XmlManager").Length)
            {
                Destroy(this.gameObject);
                return;
            }
            // シーンに画面フェードオブジェクトが存在しない場合は本オブジェクトを永続オブジェクトにする
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        // マネージャコンポ取得
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        string xmlFile = "var.xml";
        if (false == System.IO.File.Exists(xmlFile))
        {
            // XMLファイルがなければ作成する
            CreateXmlFile();
        }

        // ユーザー情報取得、ユニットリスト取得、取得したユニット情報をGMのユニットリストへ設定
        UserStatusLoadFromXml();
        UnitStateLoadFromXml();
        UnitStateSetFromXml();
    }

    /// <summary>
    /// GUID比較メソッド
    /// <para>　Loginシーンにて入力されたGUIDとxmlに保存されたGUIDを比較し、</para>
    /// <para>　一致すればtrueを、不一致であればfalseを返す。</para>
    /// </summary>
    /// <param name="inputGuidString"></param>
    /// <returns>GUIDの比較結果</returns>
    public bool CompareGuid(string inputGuidString)
    {
        // xmlファイルを取得
        XElement document = XElement.Load("var.xml");

        // 要素に対するクエリを作成
        var query = from p in document.Elements("UserParams")
                    select new
                    {
                        // 各要素とそれに対応する変数を設定
                        _guid = (string)p.Element("Guid")
                    };

        // xmlより要素を取得する
        string guidInXml = "";
        foreach (var elem in query)
        {
            guidInXml = elem._guid;
        }

        bool result = false;
        if (inputGuidString == guidInXml)
        {
            // 入力されたGUIDとXMLのGUIDが一致する場合はtrueを返す
            result = true;
        }
        return result;
    }

    /// <summary>
    /// ユニットリスト有無判定メソッド
    /// <para>　ユニット情報がXMLに存在するか否か判定する。</para>
    /// <para>　存在すればLoginシーン以降はロビーに飛ばし、存在しなければUnitSelectシーンへ飛ばす。</para>
    /// </summary>
    /// <returns>GUIDの比較結果</returns>
    public bool JudgeUnitExistInXml()
    {
        // xmlファイルを取得
        XElement document = XElement.Load("var.xml");

        // 要素に対するクエリを作成
        var query = from p in document.Elements("UnitStatus_0")
                    select new
                    {
                        // 各要素とそれに対応する変数を設定
                        _unitid = (string)p.Element("UnitID")
                    };

        // xmlより要素を取得する
        int unitIDinXML = 0;
        int NON_VALUE = 99;
        foreach (var elem in query)
        {
            unitIDinXML = int.Parse(elem._unitid);
        }

        bool result = false;
        if (NON_VALUE != unitIDinXML)
        {
            // XMLより取得したユニットIDが初期値(99)でなければユニット情報有りと判断しtrueを返す
            result = true;
        }
        return result;
    }

    /// <summary>
    /// GUIDフィールド設定メソッド
    /// <para>　GUIDをxmlより取得し、LoginシーンのGUIDフィールドに設定するため</para>
    /// <para>　コール元メソッドへGUID値を返す。</para>
    /// </summary>
    public string GuidSetForInputFieldInLogin()
    {
        // xmlファイルを取得
        XElement document = XElement.Load("var.xml");

        // 要素に対するクエリを作成
        var query = from p in document.Elements("UserParams")
                    select new
                    {
                        // 各要素とそれに対応する変数を設定
                        _guid = (string)p.Element("Guid")
                    };

        // xmlより要素を取得する
        string guidInXml = "";
        foreach (var elem in query)
        {
            guidInXml = elem._guid;
        }
        return guidInXml;
    }

    /// <summary>
    /// ユーザ関連パラメータ取得メソッド
    /// <para>　ユーザ名やGUIDなどのユーザー関連情報をXMLより取得し、GMにへ設定する。</para>
    /// </summary>
    public void UserStatusLoadFromXml()
    {
        // xmlファイルを取得
        XElement document = XElement.Load("var.xml");

        // 要素に対するクエリを作成
        var query = from p in document.Elements("UserParams")
        select new
        {
            // 各要素とそれに対応する変数を設定
            _username = (string)p.Element("UserName"),
            _guid = (string)p.Element("Guid")
        };

        // xmlより要素を取得する
        string guidInXml = "";
        string userNameInXml = "";
        foreach (var elem in query)
        {
            userNameInXml = elem._username;
            guidInXml = elem._guid;
        }

        // xmlより取得したユーザー名とGUIDをゲームマネージャーコンポに設定する
        gameManager.userName = userNameInXml;
        gameManager.userGuid = guidInXml;
    }

    /// <summary>
    /// ユニットリスト取得メソッド
    /// <para>　ユニットリストをxmlより取得する。</para>
    /// <para>　UnitStateSetFromXmlとセットで使用する。</para>
    /// </summary>
    public void UnitStateLoadFromXml()
    {
        // xmlファイルを取得
        XElement document = XElement.Load("var.xml");

        for (int i = 0; 16 > i; i++)
        {
            // 要素に対するクエリを作成
            var query0 = from p
                            in document.Elements("UnitStatus_" + i.ToString())
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
                unitidInXml[i] = elem._unitId;
                classidInXml[i] = elem._unitClass;
                unitNameInXml[i] = elem._unitName;
                abilityInXml[i] = elem._unitAbility;
                elementInXml[i] = elem._unitElement;
            }
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
            if (99 == unitidInXml[0])
            {
                // ユニットリストがエンプティ(空)の場合は終了する
                return;
            }

            // ユニットステート用GOのインスタンス化とコンポ取得
            GameObject unitGO = Instantiate(Resources.Load("UnitGO"), transform.position, Quaternion.identity) as GameObject;
            UnitState unitstate = unitGO.GetComponent<UnitState>();
            unitstate.unitID = unitidInXml[i];
            unitstate.classType = classidInXml[i];
            unitstate.unitName = unitNameInXml[i];
            unitstate.ability_A = abilityInXml[i];
            unitstate.element = elementInXml[i];
            unitGO.transform.parent = gameManager.transform;
            gameManager.unitStateList.Add(unitstate);
        }
    }

    /// <summary>
    /// XML書き込み設定メソッド
    /// <para>　UnitSelect～AbilitySelectシーンで選択した部隊の情報を任意の箇所でXMLへ書き込む。</para>
    /// </summary>
    public void UnitStateWriteToXml()
    {
        for (int i = 0; 16 > i; i++)
        {
            if (99 == gameManager.unitStateList[0].unitID)
            {
                // ユニットがいないエンプティ(空)の場合は終了する
                return;
            }

            // xmlファイルを取得
            XElement document = XElement.Load("var.xml");

            string elm = "UnitStatus_" + i.ToString();
            IEnumerable<XElement> de =
                                       from el in document.Descendants(elm)
                                       select el;
            foreach (XElement el in de)
            {
                el.Element("UnitID").Value = gameManager.unitStateList[i].unitID.ToString();
                el.Element("UnitClass").Value = gameManager.unitStateList[i].classType.ToString();
                el.Element("UnitName").Value = gameManager.unitStateList[i].unitName;
                el.Element("UnitAbility1").Value = gameManager.unitStateList[i].ability_A.ToString();
                el.Element("UnitAbility2").Value = gameManager.unitStateList[i].ability_B.ToString();
                el.Element("UnitElement").Value = gameManager.unitStateList[i].element.ToString();
            }
            // ファイルへ保存する
            document.Save("var.xml");
        }
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
