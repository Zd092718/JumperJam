using Godot;
using System;

public partial class game : Node2D
{
	private PackedScene cameraScene;
	private Camera2D camera = null;

	public override void _Ready()
	{
		cameraScene = ResourceLoader.Load("res://scenes/game_camera.tscn") as PackedScene;
		camera = cameraScene.Instantiate() as Camera2D;
		AddChild(camera);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
