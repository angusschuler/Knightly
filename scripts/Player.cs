using Godot;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 200.0f;
	[Export]
	public float Acceleration { get; set; } = 5f;
	[Export]
	public float Friction { get; set; } = 5f;
	[Export]
	public float JumpVelocity { get; set; } = -400.0f;

	private AnimatedSprite2D animatedSprite;
	private Camera2D camera;
	private AnimationPlayer animationPlayer;


	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		camera = GetNode<Camera2D>("Camera2D");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = new(Input.GetAxis("move_left", "move_right"), 1);

		// Flip sprite based on direction
		if (direction.X > 0)
		{
			animatedSprite.FlipH = false;
		}

		else if (direction.X < 0)
		{
			animatedSprite.FlipH = true;
		}

		// Animate
		Animate(direction);

		Vector2 targetVelocity = direction * Speed;

		// Apply the movement vector.
		if (direction != Vector2.Zero)
		{
			var acceleration = direction != Vector2.Zero ? Acceleration : Friction;

			velocity.X = Mathf.Lerp(velocity.X, targetVelocity.X, (float)delta * acceleration);
			GD.Print(velocity.X);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
	private void Animate(Vector2 direction)
	{
		if (animationPlayer?.IsPlaying() == true)
		{
			animatedSprite.Play("death");
			return;
		}

		if (IsOnFloor())
		{
			if (direction.X == 0)
			{
				animatedSprite.Play("idle");
			}
			else
			{
				animatedSprite.Play("run");
			}
		}
		else
		{
			animatedSprite.Play("jump");
		}
	}

	public void Kill()
	{
		animationPlayer?.Play("death");
	}
}

