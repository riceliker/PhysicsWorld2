using Godot;
using System;

namespace PhysicsWorld.GUI.Page.Template
{
	public partial class HeadRowBack : Control
	{
		[Export] private TextureButton back_button;
		[Export] private string back_scene;
		[Export] private Label label;
		[Export] private string title;
		public override void _Ready()
		{
			label.Text = title;
			back_button.Pressed += () => {GetTree().ChangeSceneToFile(back_scene);};
		}
	}
}
