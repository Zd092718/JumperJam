using Godot;
using System;
using static Godot.GD;

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

		// Generate ground
		GenerateGround();
	}

	private void GenerateGround()
	{
		var viewportSize = GetViewportRect().Size;
		var platformWidth = 136;
		// Get platform count from viewport size and platform width
		// Adding one to ensure no gaps
		var groundLayerPlatformCount = (viewportSize.X / platformWidth) + 1;
		var groundLayerYOffset = 62;
		// create platforms based on platform count 
		for (int i = 0; i < groundLayerPlatformCount; i++)
		{
			Vector2 groundLocation = new Vector2(i * platformWidth, viewportSize.Y - groundLayerYOffset);
			CreatePlatform(groundLocation);
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

	public Area2D CreatePlatform(Vector2 location)
	{
		Area2D newPlatform = platformScene.Instantiate() as Area2D;
		newPlatform.GlobalPosition = location;
		platformContainer.AddChild(newPlatform);
		return newPlatform;
	}
}
