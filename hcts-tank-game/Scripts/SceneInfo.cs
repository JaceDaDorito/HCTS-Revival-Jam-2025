using Godot;
using System;
using System.Runtime.CompilerServices;


namespace HCTSTankGame;

[Tool]
public partial class SceneInfo : Node2D
{
	[Export]
	public Vector2 originOfRoom = Vector2.Zero;
	[Export]
	public Vector2 roomDimensions = Global.DEFAULT_RESOLUTION; //Don't go beyond these bounds, make sure it is an even number

	private Vector2 TopLeft => new Vector2( (originOfRoom.X - roomDimensions.X / 2f), (originOfRoom.Y - roomDimensions.Y / 2f));
	private Vector2 BottomRight => new Vector2((originOfRoom.X + roomDimensions.X / 2f), (originOfRoom.Y + roomDimensions.Y / 2f) );

	[ExportGroup("Debug Properties")]
	[Export]
	public Texture2D debugTexture;
	[Export]
	public Color debugColor;
	[Export]
	public int debugWidth = 2;

	public override void _Ready()
	{
		if (Engine.IsEditorHint()) return;

		Global.SceneInfoInstance = this;
		Global.GameManager.SpawnPlayer(); //Move this to wherever is necessary. Make sure Spawning the Player happens AFTER the spawn point is stored

		SetCameraParams();
	}

	//Left and Top is positive for some reason
	private void SetCameraParams()
	{
		Global.PlayerInstance.Camera.LimitTop = (int)TopLeft.Y;
		Global.PlayerInstance.Camera.LimitBottom = (int)BottomRight.Y;

		Global.PlayerInstance.Camera.LimitLeft = (int)TopLeft.X;
		Global.PlayerInstance.Camera.LimitRight = (int)BottomRight.X;
	}

	//Editor
	public override void _Draw()
	{
		if (!Engine.IsEditorHint()) return;

		if(debugTexture != null) DrawTexture(debugTexture, new Vector2());
		DrawCircle(originOfRoom - GlobalPosition, 4, debugColor, false, debugWidth);

		Rect2 rect2 = new Rect2(TopLeft - GlobalPosition, roomDimensions);
		DrawRect(rect2, debugColor, false, debugWidth);
	}

	public override void _Process(double delta)
	{
		if (Engine.IsEditorHint())
		{
			QueueRedraw();
		}
	}
}
