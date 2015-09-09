
// ===========================================================================
// 「Subject」が「Observer」に通知する。
// Subjectは一つ、Observerは複数。
// 
// 
// フロー：
// 　　　　Subjectのフィールドが変更される
// 　　　　↓
// 　　　　Subjectが変更があった事を全てのObserverに通知
// 
// 必要なもの：
// 　　　　　　①Subjectの抽象メソッド（abstructクラスかinterface）
// 　　　　　　　┗管理Observerの追加メソッド、削除メソッド、通知メソッドの3つのメソッドを定義する。
// 　　　　　　②Observerの抽象メソッド（abstructクラスかinterface）
// 　　　　　　　┗ObserverクラスとSubjectクラスの密結合を抑止する目的。Subjectからの通知を受信するメソッドのみを定義する。
// 　　　　　　③Subjectの具象メソッド（普通のクラスって事）
// 　　　　　　　┗管理するObserverの追加、削除と、このクラス内に変更があった場合それを通知するメソッドを持つ。
// 　　　　　　④Observerの具象メソッド（普通のクラスって事）
// 　　　　　　　┗Subjectから通知を受けるメソッドを定義する。通知を受けたら全てのObserverはxxxを行う(全Observerの共通処理)、みたいに使う。
// 
// ===========================================================================

// =====================================
// ①サブジェクトIF
// =====================================
public interface ISubject
{
	// 管理するObserverの登録メソッド
    void Attach(IObserver observer);
	// 管理するObserverの削除メソッド
    void Detach(IObserver observer);
	// 管理するObserverへの通知メソッド
    void Notify();
}

// =====================================
// ②オブサーバIF
// =====================================
public interface IObserver
{
	// Subject通知受信メソッド
    void Notify(bool num);
}


// =====================================
// ③オブサーバ具象クラス（Subjectから変化を通知されるクラス）
// =====================================
public class ObserverClass :
    MonoBehaviour,
    IObserver				// オブサーバIF
{
    void Start()
    {
        // サブジェクトコンポを取得し、自身をリストに追加（追加しなければ通知が来ない）
        subjectComp =  canVas.GetComponent<UnitSubject>();
        subjectComp.Attach(this); // IObserverインターフェイスを背負っているクラスなら全てAttack(this)で登録できる
    }

    // ----------------------------------------
    // 通知メソッド
    // ----------------------------------------
    public void Notify(bool num)
    {
        // Subjectが変更されたら自分(Observer)を透明化
        thisAlpha = new Color(255, 255, 255, -255);
        unitSpriteImage.color = thisAlpha;
    }
}

// =====================================
// ④サブジェクト具象クラス（Observerに通知するクラス）
// =====================================
public class SubjectClass :
    MonoBehaviour,
    ISubject				// サブジェクトIF
{
    private List<IObserver> obServers = new List<IObserver>();      // 管理オブサーバリスト
    private bool _status = false;
    public bool status
    {
        get
        {
            return _status;
        }
        set
        {
            _status = value;
            Notify();		// 通知メソッドをコール
        }
    }
    
    // --------------------------------------------
    // 管理オブサーバ追加メソッド
    // --------------------------------------------
    public void Attach(IObserver observer)
    {
        obServers.Add(observer);
    }

    // --------------------------------------------
    // 管理オブサーバ削除メソッド
    // --------------------------------------------
    public void Detach(IObserver observer)
    {
        obServers.Remove(observer);
    }

    // ----------------------------------------
    // 通知メソッド
    // ----------------------------------------
    public void Notify(bool num)
    {
        // 全てのオブサーバ具象クラス内の通知メソッドへ通知
        obServers.ForEach(observer => observer.Notify(this.status));
    }
}




