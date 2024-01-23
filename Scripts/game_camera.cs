using Godot;
using System;

public partial class game_camera : Camera2D
{
	private player _player;
	private Vector2 newGlobalPosition;
	private Vector2 viewportSize;
	public override void _Ready()
	{
		newGlobalPosition = GlobalPosition;
		viewportSize = GetViewportRect().Size;
		newGlobalPosition.X = viewportSize.X / 2;

		LimitBottom = (int)viewportSize.Y;
		LimitLeft = 0;
		LimitRight = (int)viewportSize.X;

		_player = GetNode<player>("/root/Main/Game/Player");
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
