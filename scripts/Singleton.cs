using System;
using System.Collections.Generic;
using Godot;

namespace PlanetsInSpace.Map
{
	public partial class Singleton<T> where T : class
	{
		private static readonly object lockObject = new object();
		private static T _instance;
		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (lockObject)
					{
						if (_instance == null)
						{
							_instance = FindOrCreateInstance();
						}
					}
				}
				return _instance;
			}
		}

		private static T FindOrCreateInstance()
		{
			//var sceneTree = (SceneTree)Engine.GetMainLoop();
			//var instance = FindObjectOfType<T>();   //sceneTree.Root.GetNode<T>("res://universum.tscn");
			T instance = default(T);

			if (instance != null)
			{
				return instance;
			}

			// Script components can only exist when teyare attached to a game object
			//, so we have to create a new GO and attach the new component

			// In Godot kann ein C# Skript ausserhalb einer Node existeiren (anders als in Unity), oder?
			//var name = typeof(T).Name + " Singleton";
			//var containerNode = new Node3D() { Name = name };
			//containerNode.SetScript(T);// .AddComponent<T>();
			//return singletonComponent;

			//instance = new T();
			return instance;
		}

		//TODO Reines C# Template
		// Evtl. noch Erweitern um, wie vorher eine unity / jetzt eine Godot Instantz dazu zu finden
		// Überhaupt notwendig für eine C# Klasse?
		// untenstehend Helper Methode hierfür, evtl. nützlich??

		public static T2 GetNodeOfType<T2>(Node root) where T2 : Node
		{
			List<Node> unsolved = new List<Node>();
			unsolved.Add(root);
			while (unsolved.Count > 0)
			{
				if (unsolved[0].GetType() == typeof(T)) return unsolved[0] as T2;
				if (unsolved[0].GetType().IsSubclassOf(typeof(T))) return unsolved[0] as T2;
				if (unsolved[0].GetChildCount() > 0)
				{
					foreach (Node n in unsolved[0].GetChildren(false))
					{
						unsolved.Add(n);
					}
				}
				unsolved.RemoveAt(0);
			}
			return null;
		}
	}
}
