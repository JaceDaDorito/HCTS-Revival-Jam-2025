using Godot;
using System;
using System.Runtime.CompilerServices;


namespace HCTSTankGame;

[Tool]
public partial class SceneInfo : Node2D
{
	[Export]
	public Node2D spawnPoint;
	[Export]
	public Vector2 roomDimensions = Global.DEFAULT_RESOLUTION; //Don't go beyond these bounds, make sure it is an even number

	private Vector2 TopLeft => new Vector2( (GlobalPosition.X - roomDimensions.X / 2f), (GlobalPosition.Y - roomDimensions.Y / 2f));
	private Vector2 BottomRight => new Vector2((GlobalPosition.X + roomDimensions.X / 2f), (GlobalPosition.Y + roomDimensions.Y / 2f) );

	[ExportGroup("Debug Properties")]
	[Export]
	public Color debugColor;
	[Export]
	public int debugWidth = 2;

	public delegate void OnSceneStart();
	public event OnSceneStart onSceneStart;

	public override void _Ready()
	{
		if (Engine.IsEditorHint()) return;

		GameManager.Instance.SceneInfoInstance = this;
		SceneStart(); //Move this to wherever is necessary. Make sure Spawning the Player happens AFTER the spawn point is stored

		SetCameraParams();
	}

	public void SceneStart()
	{
		GameManager.Instance.SpawnPlayer();
		SetCameraParams();
		onSceneStart?.Invoke();
	}

	//Left and Top is positive for some reason
	private void SetCameraParams()
	{
		GameManager.Instance.PlayerInstance.Camera.LimitTop = (int)TopLeft.Y;
		GameManager.Instance.PlayerInstance.Camera.LimitBottom = (int)BottomRight.Y;

		GameManager.Instance.PlayerInstance.Camera.LimitLeft = (int)TopLeft.X;
		GameManager.Instance.PlayerInstance.Camera.LimitRight = (int)BottomRight.X;
	}

	//Editor
	public override void _Draw()
	{
		if (!Engine.IsEditorHint()) return;

		DrawCircle(new Vector2(0,0), 4, debugColor, false, debugWidth);

		Rect2 rect2 = new Rect2(TopLeft, roomDimensions);
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
