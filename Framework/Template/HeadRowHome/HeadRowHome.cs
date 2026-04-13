using Godot;

public partial class HeadRowHome : Control
{
	[Export] private string title_text;
	[Export] private Label title_label;
	[Export] private TextureButton back_button;

	public override void _Ready()
	{
		title_label.Text = title_text;
		back_button.Pressed += toTscn;
	}

	public void setTitle(string title_text)
	{
		this.title_text = title_text;
	}
	

	private void toTscn()
	{
		GetTree().ChangeSceneToFile("res://Framework/RootMenu/MainMenu/MainMenu.tscn");
	}
}
