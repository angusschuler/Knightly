using Godot;

public partial class Coin : Area2D
{
	[Export]
	public int Value { get; set; } = 1;
	GameManager gameManager;
	AnimationPlayer animationPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameManager = GetNode<GameManager>("%GameManager");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void On_body_entered(Node2D body)
	{
		if (body is Player)
		{
			gameManager.AddScore(Value);
			animationPlayer.Play("pickup");
		}
	}
}
