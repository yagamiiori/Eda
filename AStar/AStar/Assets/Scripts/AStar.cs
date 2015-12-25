using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// A-star algorithm
public class AStar : MonoBehaviour
{
    /// <summary>
    /// 現在の座標や最小スコアのパネル座標など、様々な座標を設定する構造体
    /// </summary>
	struct Point2
    {
		public int x;
		public int y;

        /// <summary>
        /// コンストラクタ
        /// </summary>
		public Point2(int x=0, int y=0)
        {
			this.x = x;
			this.y = y;
		}

        // セッター
		public void Set(int x, int y)
        {
			this.x = x;
			this.y = y;
		}
	}

    /// <summary>
    /// A*ノードクラス
    /// <para>　ノード（パネル）にアタッチし、自身の情報を持つ</para>
    /// </summary>
	class ANode
    {
        /// <summary>自身のパネルのステータス</summary>
        enum eStatus
        {
            /// <summary>オープン/クローズなし</summary>
            None,
            /// <summary>オープン状態</summary>
            Open,
            /// <summary>クローズ状態</summary>
            Closed,
		}
        /// <summary>自身のパネルのステータス</summary>
		eStatus _status = eStatus.None;
        /// <summary>ヒューリスティック・コスト（推定コスト）</summary>
        int _heuristic = 0;
        /// <summary>親ノード（一つ前のノード）</summary>
        ANode _parentNode = null;

        public ANode ParentNode
        {
            get { return _parentNode; }
            set {
                    _parentNode = value;
                }
        }
        /// <summary>パネルの座標</summary>
        int _x = 0;
		int _y = 0;
		public int X {
			get { return _x; }
		}
		public int Y {
			get { return _y; }
		}
        /// <summary>実コスト(1歩歩けば+1されるやつ)</summary>
        int _cost = 0;
		public int Cost {
			get { return _cost; }
		}

        /// <summary>
        /// コンストラクタ
        /// </summary>
		public ANode(int x, int y)
        {
			_x = x;
			_y = y;
		}

        /// <summary>
        /// スコア計算メソッド
        /// <para>　実コスト＋ヒューリスティックコストの値を返す。</para>
        /// </summary>
        /// <returns>実コストにヒューリスティックコストを加算した値</returns>
		public int GetScore()
        {
			return _cost + _heuristic;
		}

        /// <summary>
        /// ヒューリスティックコスト計算メソッド
        /// <para>　到達地点までのヒューリスティックコスト（想定コスト）を算出する
        /// </summary>
        /// <param name="allowdiag"></param>
        /// <param name="xgoal"></param>
        /// <param name="ygoal"></param>
		public void CalcHeuristic(bool allowdiag, int xgoal, int ygoal)
        {

			if(allowdiag)
            {
				// 斜め移動ありの場合
				var dx = (int)Mathf.Abs (xgoal - X);
				var dy = (int)Mathf.Abs (ygoal - Y);
				// 大きい方をコストにする
				_heuristic =  dx > dy ? dx : dy;
			}
			else
            {
				// 縦横移動のみの場合
				var dx = Mathf.Abs (xgoal - X);
				var dy = Mathf.Abs (ygoal - Y);
				_heuristic = (int)(dx + dy);
			}
			Dump(); // ログを吐く
		}

        /// <summary>
        /// NONEステータス判定メソッド
        /// <para>　現ステータスがNONEならtrueを返す
        /// </summary>
        /// <returns>現ステータスisNONE判定結果</returns>
		public bool IsNone()
        {
			return _status == eStatus.None;
		}

        /// <summary>
        /// ステータスOpenメソッド
        /// <para>　ステータス状態をOpenに設定する。</para>
        /// </summary>
        /// <param name="parent">ノード</param>
        /// <param name="cost">コスト</param>
		public void Open(ANode parent, int cost)
        {
			Debug.Log (string.Format("Open: ({0},{1})", X, Y));
			_status = eStatus.Open;
            _parentNode = parent;
            _cost = cost;
		}

        /// <summary>
        /// ステータスCloseメソッド
        /// <para>　ステータスをClosedにする。</para>
        /// </summary>
		public void Close()
        {
			Debug.Log (string.Format ("Closed: ({0},{1})", X, Y));
			_status = eStatus.Closed;
		}

        /// <summary>
        /// パス取得メソッド
        /// <para>　通ってきたパネル座標（パス）をリストに追加する。</para>
        /// </summary>
        /// <param name="pathList"></param>
        public void GetPath(List<Point2> pathList)
        {
            // 再帰処理をしながらユニットが通るパスのパネル座標を順番にパスリストに追加する
			pathList.Add(new Point2(X, Y));

            // 一つ前のノード情報がnull（つまりスタート地点のノード）の場合
            if (_parentNode != null)
            {
                // 一つ前のノード情報のGetPath()をコールする
                // 更にその一つ前のノード情報から見た一つ前のノード情報のGetPath()をコール、
                // それを繰り返す事で一番最初（スタート地点）の_parentNodeになる
				_parentNode.GetPath(pathList);
			}
		}
        /// <summary>
        /// ログ出力メソッド
        /// <para>　チップ毎のログを吐く</para>
        /// </summary>
		public void Dump()
        {
			Debug.Log (string.Format("({0},{1})[{2}] cost={3} heuris={4} score={5}", X, Y, _status, _cost, _heuristic, GetScore()));
		}

        /// <summary>
        /// 再帰的ダンプ？
        /// </summary>
		public void DumpRecursive()
        {
            // 一つ前のノード情報がnull（つまりスタート地点のノード）の場合
			if(_parentNode != null) {
                // 一つ前のノード情報(_parentNode)のDumpRecursive()をコールしている
                // 更にその一つ前のノード情報から見た一つ前のノード情報のDumpRecursive()をコール、
                // それを繰り返す事で一番最初（スタート地点）の_parentNodeになる
				_parentNode.DumpRecursive();
			}
		}
	}

    /// <summary>
    /// A-starノード管理クラス
    /// </summary>
	class ANodeManager
    {
        /// <summary>地形レイヤー</summary>
		Layer2D _layer;
        /// <summary>斜め移動を許可するかどうか</summary>
        bool _allowdiag = false;
        /// <summary>現在オープン中のノードリスト</summary>
        List<ANode> _openList = null;
        /// <summary>全ノードリスト</summary>
        Dictionary<int, ANode> _nodeList = null;
		/// ゴール座標
		int _xgoal = 0;
		int _ygoal = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="xgoal"></param>
        /// <param name="ygoal"></param>
        /// <param name="allowdiag"></param>
		public ANodeManager(Layer2D layer, int xgoal, int ygoal, bool allowdiag=true)
        {
			_layer = layer;
			_allowdiag = allowdiag;
			_openList = new List<ANode>();
			_nodeList = new Dictionary<int, ANode>();
			_xgoal = xgoal;
			_ygoal = ygoal;
		}

        /// <summary>
        /// ノード生成メソッド
        /// <para>　ノードを生成する。</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>作成したノード</returns>
        public ANode GetNode(int x, int y)
        {
            // 座標をインデックスに変換する
			var idx = _layer.ToIdx(x, y);
			if(_nodeList.ContainsKey(idx))
            {
				// インデックスがノードリストに既に存在している場合はそれを返す
				return _nodeList[idx];
			}

			// インデックスがノードリストに存在しない場合は新規で作成し、連想配列に追加する
			var node = new ANode(x, y);
			_nodeList[idx] = node;

			// ノードのヒューリスティック・コストを計算する
			node.CalcHeuristic(_allowdiag, _xgoal, _ygoal);
			return node;
		}

        /// <summary>
        /// オープンノードリスト追加メソッド
        /// <para>　指定したノードをオープンリストに追加する。
        /// </summary>
        /// <param name="node">オープンノードリストに追加するノード</param>
		public void AddOpenNodeList(ANode addNode)
        {
            _openList.Add(addNode);
		}

        /// <summary>
        /// オープンノードリスト削除メソッド
        /// <para>　指定したノードをオープンリストから削除する。</para>
        /// </summary>
        /// <param name="node">オープンノードリストから削除するノード</param>
        public void RemoveOpenNodeList(ANode deleteNode)
        {
            _openList.Remove(deleteNode);
		}

        /// <summary>
        /// 指定座標ステータスOpenメソッド
        /// <para>　通過不能オブジェクト判定、およびパネルステータスがNONEか否かを</para>
        /// <para>　判定し、判定を通ったノードのステータスをOpenにする。</para>
        /// <para>　また、オープンノードリスト（_openList）への追加も本メソッド内にて行う。</para>
        /// </summary>
        /// <param name="x">指定座標X</param>
        /// <param name="y">指定座標Y</param>
        /// <param name="cost">コスト</param>
        /// <param name="parentNode">親ノード</param>
        /// <returns></returns>
        public ANode OpenNode(int x, int y, int cost, ANode parentNode)
        {
			// 座標をチェック
			if(_layer.IsOutOfRange(x, y))
            {
                // 領域外の場合はOpenリストに追加できないためメソッドを抜ける
				return null;
			}
			if(_layer.Get(x, y) > 1)
            {
                // 通過不能オブジェクトで通れない場合はOpenリストに追加できないためメソッドを抜ける
				return null;
			}
			// ノードをノードリストから取得する。無ければ作成する
			var node = GetNode(x, y);
			if(node.IsNone() == false)
            {
				// パネルのステータスがNONE以外の場合はOpenリストに追加できないためメソッドを抜ける
				return null;
			}

			// ノードをOpenし、オープンノードリストへそのノードを追加する
			node.Open(parentNode, cost);
			AddOpenNodeList(node);

			return node;
		}

        /// <summary>
        /// 周囲ノードOpenメソッド
        /// <para>　周囲のノードをOpenにする。
        /// </summary>
        /// <param name="parentNode">親ノード</param>
        public void OpenAround(ANode parentNode)
        {
            // 基準座標XとY
			var xbase = parentNode.X;
			var ybase = parentNode.Y;
            // コスト
			var cost = parentNode.Cost;
			cost += 1; // 一歩進むので+1する.

			if(_allowdiag)
            {
				// 斜め移動ありの場合（8方向を開く）
				for(int j = 0; j < 3; j++) {
					for(int i = 0; i < 3; i++) {
						var x = xbase + i - 1; // -1～1
						var y = ybase + j - 1; // -1～1
                        // 上下左右＋斜めのパネルをオープンにし、オープンノードリストに追加する
                        OpenNode(x, y, cost, parentNode);
					}
				}
			}
			else
            {
                // 斜め移動なしの場合（4方向を開く）
                var x = xbase;
				var y = ybase;
                // 上下左右のパネルをオープンにし、オープンノードリストに追加する
				OpenNode (x-1, y,   cost, parentNode); // 右.
				OpenNode (x,   y-1, cost, parentNode); // 上.
				OpenNode (x+1, y,   cost, parentNode); // 左.
				OpenNode (x,   y+1, cost, parentNode); // 下.
			}
		}

        /// <summary>
        /// 最小スコアノード検索メソッド
        /// <para>　最小スコアのノードをオープンノードリスト内から検索し、取得する。</para>
        /// </summary>
        /// <returns>オープンリスト内で最小スコアのノード</returns>
		public ANode SearchMinScoreNodeFromOpenList()
        {
			// 最小スコア
			int min = 9999;
			// 最小実コスト
			int minCost = 9999;
            // 最小スコアのノード
			ANode minNode = null;

            // 最小スコアノード検索のためにオープンノードリスト内を検索
			foreach(ANode node in _openList)
            {
                // ノードのスコア（実コスト＋ヒューリスティックコスト）を取得する
				int score = node.GetScore();

				if(score > min)
                {
					// スコアが最小コストより大きい場合はループ先頭に戻る
					continue;
				}
                // スコアと最小スコアが同じ場合は実コストだけの比較を行う
				if(score == min && node.Cost >= minCost)
                {
                    // 実コストが最小実コストより大きい場合はループ先頭に戻る
					continue;
				}

                // 最小スコア、最小実コスト、最小スコアのノードを更新する
				min = score;
				minCost = node.Cost;
				minNode = node;
			}
            // 最終的に残ったオープンリスト内で最小スコアのノードを返す
			return minNode;
		}
	}

    /// <summary>
    /// チップ座標X取得メソッド
    /// <para>　チップ上のX座標を取得する。</para>
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
	float GetChipX(int i)
    {
        // カメラをワールド座標に変換
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        // 取得対象のスプライト
        var spr = Util.GetSprite("Levels/base", "base_0");
		var sprW = spr.bounds.size.x;

		return min.x + (sprW * i) + sprW/2;
	}

    /// <summary>
    /// チップ座標Y取得メソッド
    /// <para>　チップ上のY座標を取得する。</para>
    /// </summary>
    /// <param name="j"></param>
    /// <returns></returns>
    float GetChipY(int j)
    {
        // カメラをワールド座標に変換
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        // 取得対象のスプライト
		var spr = Util.GetSprite("Levels/base", "base_0");
		var sprH = spr.bounds.size.y;

		return max.y - (sprH * j) - sprH/2;
	}

    /// <summary>
    /// ランダム座標取得メソッド
    /// <para>　マップ上のランダムな座標を取得する。</para>
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
	Point2 GetRandomPosition(Layer2D layer)
    {
		Point2 p;
		while(true)
        {
			p.x = Random.Range(0, layer.Width);
			p.y = Random.Range(0, layer.Height);

			if(layer.Get(p.x, p.y) == 1)
            {
				// 通過可能
				break;
			}
		}
		return p;
	}

    /// <summary>
    /// ユニットの状態
    /// </summary>
	enum eState
    {
        /// <summary>実行中</summary>
		Exec,
        /// <summary>移動中</summary>
        nowMoving,
        /// <summary>移動終了</summary>
        moveEnd
	}
	eState _state = eState.Exec;

	IEnumerator Start ()
    {
        // 地形データのロード.
        var tmx = new TMXLoader();
        tmx.Load("Levels/001");
        var layer = tmx.GetLayer(0);

        // フィールドのタイルを１つずつ配置する
        for (int j = 0; j < layer.Height; j++) // 縦
        {
            for (int i = 0; i < layer.Width; i++) // 横
            {
                // 座標の値を取得
                var v = layer.Get(i, j);
                var x = GetChipX(i);
                var y = GetChipY(j);
                // スプライトを設定し、タイルを作成する
                Tile.Add(v, x, y);
            }
        }
        yield return new WaitForSeconds(0.1f);

        // パスリスト（最終的にユニットが通るパネル座標のリスト）
        var pathList = new List<Point2>();
        // ユニットの基底クラス
        Token player = null;

        // A-star実行
        {
            // スタート地点を設定
            Point2 pStart = GetRandomPosition(layer);
            player = Util.CreateUnit(GetChipX(pStart.x), GetChipY(pStart.y), "", "miku2", "Player");
            player.SortingLayer = "Chara";
            // ゴール地点を設定
            Point2 pGoal = GetRandomPosition(layer);
            var goal = Util.CreateUnit(GetChipX(pGoal.x), GetChipY(pGoal.y), "", "gate1", "Goal");
            goal.SortingLayer = "Chara";
            // 斜め移動可否判定を決定
            var allowdiag = false;

            // コンストラクタにそれぞれの値を渡してインスタンス化
            var anodeManager = new ANodeManager(layer, pGoal.x, pGoal.y, allowdiag);

            // スタート地点のノードを取得し、最小ノードに設定する。スタート地点なのでコストは「0」
            ANode minScoreNode = anodeManager.OpenNode(pStart.x, pStart.y, 0, null);
            // それをオープンノードリストに追加する
            anodeManager.AddOpenNodeList(minScoreNode);

            // 移動試行回数。1000回超えたら強制中断
            int cnt = 0;
            while (cnt < 1000)
            {
                // 今いるパネルをオープンノードリストから削除
                anodeManager.RemoveOpenNodeList(minScoreNode);
                // 周囲を開く
                anodeManager.OpenAround(minScoreNode);
                // 最小スコアのノードを探す.
                minScoreNode = anodeManager.SearchMinScoreNodeFromOpenList();

                if (minScoreNode == null)
                {
                    // 最小スコアのノードがない場合は終了
                    Debug.Log("Not found path.");
                    break;
                }

                // ゴールまでの経路算出が完了した場合？
                if (minScoreNode.X == pGoal.x && minScoreNode.Y == pGoal.y)
                {
                    Debug.Log("Success.");
                    // オープンノードリストから最小スコアのノードを削除する
                    anodeManager.RemoveOpenNodeList(minScoreNode);

                    // 再帰的ダンプ？
                    minScoreNode.DumpRecursive();

                    // パスを取得する
                    minScoreNode.GetPath(pathList);

                    // pathListがゴール→現在位置という並びになっているので現在位置→ゴールの並びにするため反転を行う
                    pathList.Reverse();
                    break;
                }
                yield return new WaitForSeconds(0.01f);
            }
        } // A-star実行ここまで

        // ユニットの状態を移動中に変更し、移動を実施する
        _state = eState.nowMoving;
        foreach (var p in pathList)
        {
            // 移動先パネルのXY値を取得
            var x = GetChipX(p.x);
            var y = GetChipY(p.y);
            // 移動実施
            player.X = x;
            player.Y = y;
            // ちょっと待たないと瞬間移動する
            yield return new WaitForSeconds(0.06f);
        }
        // ユニット状態を移動停止にする
        _state = eState.moveEnd;
    }

	void OnGUI() {
		switch(_state) {
		case eState.Exec:
			Util.GUILabel(160, 160, 128, 32, "経路計算中...");
			break;
		case eState.nowMoving:
			Util.GUILabel(160, 160, 128, 32, "移動中");
			break;
		case eState.moveEnd:
			if(GUI.Button(new Rect(160, 160, 128, 32), "もう一回")) {
				Tile.parent = null;
				Application.LoadLevel("Main");
			}
			break;
		}
	}
}
