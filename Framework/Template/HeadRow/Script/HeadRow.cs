using Godot;

public partial class HeadRow : Control
{
	[Export] private string title_text;
	[Export] private Label title_label;
	[Export] private Button back_button;

	public override void _Ready()
	{
		title_label.Text = title_text;
		back_button.Pressed += to_Main_Menu;
	}

	public void setTitle(string title_text)
	{
		this.title_text = title_text;
	}

	private void to_Main_Menu()
	{
		GetTree().ChangeSceneToFile("res://Framework/HomeMenu/MainMenu/MainMenu.tscn");
	}
}
