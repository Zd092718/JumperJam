using Godot;
using System;
using static Godot.GD;

public partial class game : Node2D
{
	private PackedScene cameraScene;
	private Camera2D camera = null;
	private level_generator _Generator;
	private player _player;


	public override void _Ready()
	{
		cameraScene = ResourceLoader.Load("res://scenes/game_camera.tscn") as PackedScene;
		camera = cameraScene.Instantiate() as Camera2D;
		AddChild(camera);
		_player = GetNode<player>("Player");
		_Generator = GetNode<level_generator>("LevelGenerator");
		if (_player != null)
		{
			_Generator.Setup(_player);
		}
	}




	public override void _Process(double delta)
	{
		// Debug keys
		if (Input.IsActionJustPressed("quit"))
		{
			GetTree().Quit();
		}
		if (Input.IsActionJustPressed("reset"))
		{
			GetTree().ReloadCurrentScene();
		}
	}

}
