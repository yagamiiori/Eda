using UnityEngine;
using System.Collections;

/// タイル管理
public class Tile : Token {

	public static TokenMgr<Tile> parent = null;

	public static Tile Add(int id, float x, float y)
    {
		if(parent == null) 
        {
			parent = new TokenMgr<Tile>("Tile");
		}
		var t = parent.Add(x, y);
		t.Create(id);
		return t;
	}

	public void Create(int id)
    {
		var spr = Util.GetSprite("Levels/base", "base_" + (id-1));
		SetSprite(spr);
	}
}
