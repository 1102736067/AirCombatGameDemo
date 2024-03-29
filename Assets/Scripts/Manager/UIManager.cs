﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : NormalSingleton<UIManager>
{
	private Canvas _canvas;
	private Stack<string> _uiStack = new Stack<string>();
	private Dictionary<string, IView> _views = new Dictionary<string, IView>();

	public Canvas Canvas
	{
		get
		{
			if (_canvas == null)
				_canvas = Object.FindObjectOfType<Canvas>();
			
			if(_canvas == null)
				Debug.LogError("can not find canvas");
			return _canvas;
		}
	}

	public IView Show(string path)
	{
		if (_uiStack.Count > 0)
		{
			string name = _uiStack.Peek();
			_views[name].Hide();
		}

		IView view = InitView(path);
		view.Show();
		_uiStack.Push(path);
		_views[path] = view;

		return view;
	}

	private IView InitView(string path)
	{
		if (_views.ContainsKey(path))
		{
			return _views[path];
		}
		else
		{
			GameObject viewGo = LoadMgr.Single.LoadPrefab(path,Canvas.transform);
			viewGo.AddComponent(BindUtil.GetScriptType(path));
			IView view = viewGo.GetComponent<IView>();
			view.Init();
			return view;
		}
	}

	public void Back()
	{
		if(_uiStack.Count <= 1)
			return;
		
		string name = _uiStack.Peek();
		_uiStack.Pop();
		_views[name].Hide();

		name = _uiStack.Peek();
		_views[name].Show();
	}
}
