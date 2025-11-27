using Godot;

public partial class Killzone : Area2D
{

	private Timer timer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void On_body_entered(Node2D body)
	{
		timer.Start();
		if (body is Player player)
		{
			// todo: We should ideally have a "Killable" component to fetch and call here
			player.Kill();
		}
		Engine.TimeScale = 0.5f;
	}

	public void On_timer_timeout()
	{
		Engine.TimeScale = 1.0f;
		GetTree().ReloadCurrentScene();
	}
}
