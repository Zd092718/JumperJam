using Godot;
using System;

public partial class game : Node2D
{
	private PackedScene cameraScene;
	private PackedScene platformScene;
	private Camera2D camera = null;
	private Node2D platformContainer;


	public override void _Ready()
	{
		platformContainer = GetNode<Node2D>("PlatformContainer");

		cameraScene = ResourceLoader.Load("res://scenes/game_camera.tscn") as PackedScene;
		platformScene = ResourceLoader.Load("res://scenes/platform.tscn") as PackedScene;
		camera = cameraScene.Instantiate() as Camera2D;
		AddChild(camera);

		CreatePlatform(new Vector2(100, 300));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("quit"))
		{
			GetTree().Quit();
		}
		if (Input.IsActionJustPressed("reset"))
		{
			GetTree().ReloadCurrentScene();
		}
	}

	public Area2D CreatePlatform(Vector2 location)
	{
		Area2D newPlatform = platformScene.Instantiate() as Area2D;
		newPlatform.GlobalPosition = location;
		platformContainer.AddChild(newPlatform);
		return newPlatform;
	}
}
