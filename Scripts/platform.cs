using Godot;
using System;
using static Godot.GD;

public partial class platform : Area2D
{
	private player _player;

	public override void _Ready()
	{
		_player = GetNode<player>("/root/Main/Game/Player");
	}

	private void _on_body_entered(Node2D body)
	{
		if (body == _player)
		{
			if (_player.Velocity.Y > 0)
			{
				_player.Jump();
			}
		}
	}

}

