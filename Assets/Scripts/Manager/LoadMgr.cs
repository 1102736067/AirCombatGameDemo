﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class LoadMgr : NormalSingleton<LoadMgr>,ILoader
{
	private ILoader _loader;

	public LoadMgr()
	{
		_loader = new ResourceLoader();
	}
	
	public GameObject LoadPrefab(string path,Transform parent = null)
	{
		return _loader.LoadPrefab(path, parent);
	}

	public Sprite LoadSprite(string path)
	{
		return _loader.LoadSprite(path);
	}
	
	public Sprite[] LoadAllSprites(string path)
	{
		return _loader.LoadAllSprites(path);
	}

	public void LoadConfig(string path, Action<object> complete)
	{
		_loader.LoadConfig(path, complete);
	}
}
