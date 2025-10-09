using Godot;
using System;
using System.Runtime.CompilerServices;


namespace HCTSTankGame;

[Tool]
public partial class Node2DEditorDrawer : Node2D
{

    [ExportGroup("Debug Properties")]
    [Export]
    public Texture2D debugTexture;

    //Editor
    public override void _Draw()
    {
        if (!Engine.IsEditorHint()) return;

        if (debugTexture != null)
        {
            int width = debugTexture.GetWidth();
            int height = debugTexture.GetHeight();
            DrawTexture(debugTexture, new Vector2(-width / 2f, -height /2f));
        }
    }

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
        {
            QueueRedraw();
        }
    }
}
