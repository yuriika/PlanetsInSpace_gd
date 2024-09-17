using Godot;
using PlanetsInSpace.Map;


public static class GDUtils
{
    public static PackedScene GetScene(string scene)
    {
        var sceneTree = (SceneTree)Godot.Engine.GetMainLoop();
        var sceneManager = sceneTree.Root.GetNode("/root/SceneManager");
        PackedScene returndata = (PackedScene)sceneManager.Call("get_scene", scene);
        return returndata;
    }
}
