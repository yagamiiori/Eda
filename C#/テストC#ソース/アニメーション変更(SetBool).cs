// ================================
// キャラオブジェクト（主人公）（プレハブ）
// 機能：主人公アニメを表示する
// CharacterControllerコンポーネントを持つ
// 
// 起動契機：プレイヤーオブジェクト表示時（常時）
// 
// リダイレクト：コンテナクラス（LVやHPなど各パラメータ値の参照、上書き）
// 　　　　　　　 村人、イベントオブジェクトなど
// ================================
using UnityEngine;
using System.Collections;

public class Player_2D : MonoBehaviour, IGetPutCont {

	// アニメコンポーネント取得用スタック
	Animator anim;

	// ==================
	// 初期実行関数
	// 機能：キャラコン取得
	// 　　　パラメータ読み込み
	// ==================
	void  Start ()
	{
		// アニメコンポーネント取得
		anim = GetComponent<Animator>();
	}// void Start()

	void  Update (){
		
		// 操作可能かつInout_Boxにヒットしていない場合
		if(ctrl == 1 && obj_hitflag == 0)
		{
			// 左右方向キー判定（-1:左キー　1：右キー）
			int input_facing = (int) Input.GetAxisRaw("Horizontal");
			
			// 上下方向キー判定（-1:下キー　1：上キー）
			int input_facing2 = (int) Input.GetAxisRaw("Vertical");
			
			// 右に移動する場合
			if(input_facing == 1)
			{
				// anim.SetBoolでアニメステータス変数を制御
				transform.localScale = new Vector2(1, 1);
				anim.SetBool("Tina_Down",false);
				anim.SetBool("Tina_Up",false);
				anim.SetBool("Tina_FwdBack",true);
			//左に移動する場合
			}else if(input_facing == -1)
			{
				// anim.SetBoolでアニメステータス変数を制御
				transform.localScale = new Vector2(-1, 1);
				anim.SetBool("Tina_Down",false);
				anim.SetBool("Tina_Up",false);
				anim.SetBool("Tina_FwdBack",true);
			}
}


