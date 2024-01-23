using Godot;
using System;

public partial class game_camera : Camera2D
{
	private player _player;
	private Vector2 newGlobalPosition;
	public override void _Ready()
	{
		newGlobalPosition = GlobalPosition;
		newGlobalPosition.X = GetViewportRect().Size.X / 2;
		
		_player = GetNode<player>("/root/Main/Game/Player");
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
