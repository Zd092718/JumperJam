using Godot;
using System;
using static Godot.GD;

public partial class game_camera : Camera2D
{
	private player _player;
	private Vector2 newGlobalPosition;
	private Vector2 newDestroyerPosition;
	private Vector2 viewportSize;
	private Area2D destroyer;
	private CollisionShape2D destroyerShape;

	public override void _Ready()
	{
		newGlobalPosition = GlobalPosition;
		viewportSize = GetViewportRect().Size;
		newGlobalPosition.X = viewportSize.X / 2;

		LimitBottom = (int)viewportSize.Y;
		LimitLeft = 0;
		LimitRight = (int)viewportSize.X;

		_player = GetNode<player>("/root/Main/Game/Player");
		destroyer = GetNode<Area2D>("Destroyer");
		destroyerShape = GetNode<CollisionShape2D>("Destroyer/CollisionShape2D");

		//Setting the platform destroyer position
		newDestroyerPosition = destroyer.Position;
		newDestroyerPosition.Y = viewportSize.Y / 2 + 100;
		destroyer.Position = newDestroyerPosition;

		//Setting up the collision shape
		var rectShape = new RectangleShape2D();
		var rectShapeSize = new Vector2(viewportSize.X, 200);
		rectShape.Size = rectShapeSize;
		destroyerShape.Shape = rectShape;
	}

	public override void _Process(double delta)
	{
		if (_player != null)
		{
			var limitDistance = 420;
			if (LimitBottom > _player.GlobalPosition.Y + limitDistance)
			{
				LimitBottom = (int)_player.GlobalPosition.Y + limitDistance;
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_player != null)
		{
			newGlobalPosition.Y = _player.Position.Y;
		}
		GlobalPosition = newGlobalPosition;
	}

}
