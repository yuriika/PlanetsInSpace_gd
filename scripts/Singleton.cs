using Godot;

namespace PlanetsInSpace.Map
{
    public partial class Singleton<T> : Node where T : Node
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    //_instance = FindOrCreateInstance();
                }
                return _instance;
            }
        }

        private static T _instance;

        //private static T FindOrCreateInstance()
        //{
        //    var sceneTree = (SceneTree)Engine.GetMainLoop();
        //    var instance = sceneTree.Root.GetNode<T>("res://universum.tscn");

        //    if (instance != null)
        //    {
        //        return instance;
        //    }

        //    // Script components can only exist when teyare attached to a game object
        //    //, so we have to create a new GO and attach the new component

        //    var name = typeof(T).Name + " Singleton";

        //    var containerNode = new Node3D() { Name = name };

        //    containerNode.SetScript(T);// .AddComponent<T>();

        //    return singletonComponent;
        //}
    }
}
