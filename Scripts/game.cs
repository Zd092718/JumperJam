using Godot;
using System;
using static Godot.GD;

public partial class game : Node2D
{
	private PackedScene cameraScene;
	private Camera2D camera = null;
	private level_generator _Generator;
	private player _player;
	private Vector2 viewportSize;
	private Sprite2D groundSprite;
	private Vector2 newGroundPosition;
	private ParallaxLayer backLayer;
	private ParallaxLayer middleLayer;
	private ParallaxLayer frontLayer;


	public override void _Ready()
	{
		viewportSize = GetViewportRect().Size;
		backLayer = GetNode<ParallaxLayer>("ParallaxBackground/Back");
		middleLayer = GetNode<ParallaxLayer>("ParallaxBackground/Middle");
		frontLayer = GetNode<ParallaxLayer>("ParallaxBackground/Front");
		groundSprite = GetNode<Sprite2D>("GroundSprite");

		newGroundPosition = groundSprite.GlobalPosition;
		newGroundPosition.X = viewportSize.X / 2;
		newGroundPosition.Y = viewportSize.Y;
		groundSprite.GlobalPosition = newGroundPosition;


		SetupParallaxLayer(backLayer);
		SetupParallaxLayer(middleLayer);
		SetupParallaxLayer(frontLayer);

		NewGame();
	}

	private Vector2 GetParallaxSpriteScale(Sprite2D parallaxSprite)
	{
		var parallaxTexture = parallaxSprite.Texture;
		var parallaxTextureWidth = parallaxTexture.GetWidth();
		var scale = viewportSize.X / parallaxTextureWidth;
		var result = new Vector2(scale, scale);
		return result;
	}

	private void SetupParallaxLayer(ParallaxLayer parallaxLayer)
	{
		var newMotionMirroring = parallaxLayer.MotionMirroring;
		Sprite2D parallaxSprite = parallaxLayer.FindChild("Sprite2D") as Sprite2D;
		if (parallaxSprite != null)
		{
			parallaxSprite.Scale = GetParallaxSpriteScale(parallaxSprite);
			float my = parallaxSprite.Texture.GetHeight() * parallaxSprite.Scale.Y;
			newMotionMirroring.Y = my;
			parallaxLayer.MotionMirroring = newMotionMirroring;

		}
		Print(parallaxLayer.Scale);
		Print(parallaxLayer.MotionMirroring.Y);
	}

	private void NewGame()
	{
		_player = GetNode<player>("Player");
		_Generator = GetNode<level_generator>("LevelGenerator");
		cameraScene = ResourceLoader.Load("res://scenes/game_camera.tscn") as PackedScene;
		camera = cameraScene.Instantiate() as Camera2D;
		AddChild(camera);
		if (_player != null)
		{
			_Generator.Setup(_player);
		}
	}



	public override void _Process(double _delta)
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
