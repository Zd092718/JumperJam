using Godot;
using System;
using static Godot.GD;

public partial class game : Node2D
{
	private PackedScene cameraScene;
	private PackedScene platformScene;
	private Camera2D camera = null;
	private Vector2 viewportSize;

	// level gen variables
	private Node2D platformContainer;
	private float startPlatformY;
	private float yDistanceBetweenPlatforms = 100f;
	private int levelSize = 50;
	private int platformWidth = 136;


	public override void _Ready()
	{
		platformContainer = GetNode<Node2D>("PlatformContainer");

		cameraScene = ResourceLoader.Load("res://scenes/game_camera.tscn") as PackedScene;
		platformScene = ResourceLoader.Load("res://scenes/platform.tscn") as PackedScene;
		camera = cameraScene.Instantiate() as Camera2D;
		AddChild(camera);

		viewportSize = GetViewportRect().Size;
		// Generate ground
		GenerateGround();

		// Generate rest of level
		startPlatformY = viewportSize.Y - (yDistanceBetweenPlatforms * 2);
		for (int i = 0; i < levelSize; i++)
		{
			var maxXPosition = viewportSize.X - platformWidth;
			var randomX = (float)RandRange(0.0, maxXPosition);
			Vector2 location;
			location.X = randomX;
			location.Y = startPlatformY - (yDistanceBetweenPlatforms * i);


			Print(location);
			CreatePlatform(location);
		}
	}

	private void GenerateGround()
	{
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
