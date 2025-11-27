using Godot;
using System;

public partial class GreenSlime : Node2D
{
	[Export]
	float Speed { get; set; } = 30.0f;

	private int direction = 1;

	private RayCast2D raycastRight;
	private RayCast2D raycastLeft;

	private RayCast2D raycastRightDown;
	private RayCast2D raycastLeftDown;
	private AnimatedSprite2D animatedSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		raycastRight = GetNode<RayCast2D>("RayCastRight");
		raycastLeft = GetNode<RayCast2D>("RayCastLeft");
		raycastRightDown = GetNode<RayCast2D>("RayCastRightDown");
		raycastLeftDown = GetNode<RayCast2D>("RayCastLeftDown");
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		animatedSprite.Play("spawn");
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		if (animatedSprite.IsPlaying() && animatedSprite.Animation == "spawn")
		{
			return;
		}

		if (raycastRight.IsColliding())
		{
			direction = -1;
			animatedSprite.FlipH = true;
		}

		if (raycastLeft.IsColliding())
		{
			direction = 1;
			animatedSprite.FlipH = false;
		}

		if (!raycastRightDown.IsColliding() && direction == 1)
		{
			direction = -1;
			animatedSprite.FlipH = true;
		}

		if (!raycastLeftDown.IsColliding() && direction == -1)
		{
			direction = 1;
			animatedSprite.FlipH = false;
		}

		Vector2 velocity = new(Speed, 0);
		Position += velocity * (float)delta * direction;
	}

	private void On_Spawn_Animation_Finished()
	{
		animatedSprite.Play("idle");
	}
}
