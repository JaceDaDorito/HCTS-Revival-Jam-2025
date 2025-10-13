using Godot;
using System;
using static Godot.SkeletonProfile;


namespace HCTSTankGame;
public partial class GameManager : Node
{

	[Export]
	public PackedScene Player;
	[Export]
	public LevelCatalog levelCatalog;

	public static GameManager Instance { get; set; }
	public PlayerMaster PlayerInstance { get; set; }
	public SceneInfo SceneInfoInstance { get; set; }
	private Node2D spawnPoint => SceneInfoInstance.spawnPoint;

	private Node currentScene => GetTree().CurrentScene;
	private int currentLevelIndex = 0;

	//Called on starts
	public override void _Ready()
	{
		Instance = this;
	}
	public void SpawnPlayer()
	{
		if(SceneInfoInstance.spawnPoint == null)
		{
			GD.PrintErr("Spawn Point not found, not spawning player.");
			return;
		}

		GD.Print("Initializing Player");
		PlayerInstance = Player.Instantiate<PlayerMaster>();

		//Should probably delay this to the scene with actual gameplay.
		currentScene.CallDeferred(Node.MethodName.AddChild, PlayerInstance);
		PlayerInstance.TargetMotor.Position = spawnPoint.GlobalPosition;
		PlayerInstance.TargetMotor.Rotation = spawnPoint.GlobalRotation;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsAction(Global.DEBUGNEXTLEVL) & @event.IsPressed())
		{
			NextLevel();
		}
	}

	//Hardcoded, will make proper level loading later

	public void NextLevel()
	{
		currentLevelIndex++;
		string levelUID = levelCatalog.GetLevelUID(currentLevelIndex);
		GD.Print(currentLevelIndex);
		SceneTransition.Instance.ChangeScene(levelUID);
	}

	//Called every frame
	public override void _Process(double delta)
	{
		
	}


}
