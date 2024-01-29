using Godot;
using System;
using static Godot.GD;

public partial class level_generator : Node2D
{
	private PackedScene platformScene;
	private Vector2 viewportSize;
	private Node2D platformContainer;
	private float startPlatformY;
	private float yDistanceBetweenPlatforms = 150f;
	private int levelSize = 50;
	private int platformWidth = 136;
	private int generatedPlatformCount = 0;
	private player Player;


	public override void _Ready()
	{
		platformContainer = GetNode<Node2D>("PlatformContainer");
		Player = GetNode<player>("/root/Main/Game/Player");
		platformScene = ResourceLoader.Load("res://scenes/platform.tscn") as PackedScene;
		viewportSize = GetViewportRect().Size;
		generatedPlatformCount = 0;
		startPlatformY = viewportSize.Y - (yDistanceBetweenPlatforms * 2);
		GenerateLevel(startPlatformY, true);
	}


	public override void _Process(double delta)
	{
		if (Player != null)
		{
			var pY = Player.GlobalPosition.Y;
			var endOfLevelPos = startPlatformY - (generatedPlatformCount * yDistanceBetweenPlatforms);
			var threshold = endOfLevelPos + (yDistanceBetweenPlatforms * 6);
			if (pY <= threshold)
			{
				GenerateLevel(endOfLevelPos, false);
			}
		}
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

	public Area2D CreatePlatform(Vector2 location)
	{
		Area2D newPlatform = platformScene.Instantiate() as Area2D;
		newPlatform.GlobalPosition = location;
		platformContainer.AddChild(newPlatform);
		return newPlatform;
	}

	public void Setup(player _player)
	{
		if (_player != null)
		{
			Player = _player;
		}
	}
}
