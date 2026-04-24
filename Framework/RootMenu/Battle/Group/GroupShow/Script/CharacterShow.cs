using Godot;
using System.Threading.Tasks;

public partial class CharacterShow : Node3D
{
	[Export] public Timer EyeTimer;
	[Export] public Node3D character_model;
	private AnimationPlayer face_animation;
	public override void _Ready()
	{
		AnimationPlayer body_animation = character_model.GetNode<Node3D>("VRM").GetNode<AnimationPlayer>("Body");
		body_animation.Play("stay_here");
		face_animation = character_model.GetNode<Node3D>("VRM").GetNode<AnimationPlayer>("Face");
		EyeTimer.Timeout += onEyeBlink;

	}
    public async void onEyeBlink()
	{
		face_animation.Play("blink");
		await Task.Delay(200);
		face_animation.Play("RESET");
	}


}
