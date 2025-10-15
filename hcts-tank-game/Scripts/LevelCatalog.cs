using Godot;
using System;
using System.Linq;

namespace HCTSTankGame;

public partial class LevelCatalog : Resource
{
    //Key is name of level, value is UID of level
    [Export]
    private Godot.Collections.Dictionary<string, string> _nameToSceneUID = new Godot.Collections.Dictionary<string, string>();

    public LevelCatalog(Godot.Collections.Dictionary<string, string> nameToSceneUID)
    {
        _nameToSceneUID = nameToSceneUID ?? new Godot.Collections.Dictionary<string, string>();
    }

    public LevelCatalog() : this(null) { }

    public string GetLevelUID(int index)
    {
        return index < _nameToSceneUID.Count ?
            _nameToSceneUID.ElementAt(index).Value:
            Global.NULL_LEVEL_UID;
    }

    public string GetLevelUID(string name)
    {
        string uid;
        bool success = _nameToSceneUID.TryGetValue(name, out uid);

        return success ? uid: Global.NULL_LEVEL_UID;
    }
}