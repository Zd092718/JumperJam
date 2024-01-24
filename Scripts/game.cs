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
	private float yDistanceBetweenPlatforms = 150f;
	private int levelSize = 50;
	private int platformWidth = 136;
	private int generatedPlatformCount = 0;
	private CharacterBody2D player;


	public override void _Ready()
	{
		platformContainer = GetNode<Node2D>("PlatformContainer");
		player = GetNode<CharacterBody2D>("Player");

		cameraScene = ResourceLoader.Load("res://scenes/game_camera.tscn") as PackedScene;
		platformScene = ResourceLoader.Load("res://scenes/platform.tscn") as PackedScene;
		camera = cameraScene.Instantiate() as Camera2D;
		AddChild(camera);

		viewportSize = GetViewportRect().Size;
		generatedPlatformCount = 0;
		startPlatformY = viewportSize.Y - (yDistanceBetweenPlatforms * 2);


		GenerateLevel(startPlatformY, true);
	}

	public void GenerateLevel(float startY, bool generateGround)
	{
		if (generateGround == true)
		{
			GenerateGround();
		}

		for (int i = 0; i < levelSize; i++)
		{
			var maxXPosition = viewportSize.X - platformWidth;
			var randomX = (float)RandRange(0.0, maxXPosition);
			Vector2 location;
			location.X = randomX;
			location.Y = startY - (yDistanceBetweenPlatforms * i);


			Print(location);
			CreatePlatform(location);
			generatedPlatformCount++;
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
		if (player != null)
		{
			var pY = player.GlobalPosition.Y;
			var endOfLevelPos = startPlatformY - (generatedPlatformCount * yDistanceBetweenPlatforms);
			var threshold = endOfLevelPos + (yDistanceBetweenPlatforms * 6);
			if (pY <= threshold)
			{
				GenerateLevel(endOfLevelPos, false);
			}
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
