using Godot;
using System;
using System.ComponentModel;

public partial class CharacterItem : Control
{
	[Signal] public delegate void OnCharacterListItemButtonClickedEventHandler(string name);
	[Export] private Button button;
	[Export] private Label label;
	[Export] private TextureRect texture_rect;
	private DataUniqueID character_id;
	

	public void setInformation(CharacterInformation character)
	{
		character_id = character.getUniqueID();
		label.Text = character.description.name;
		texture_rect.Texture = character.getIcon();
		SetLabelBackground(label, getBackgroundColorByCharacterType(character.description.type));
	}
	private Color getBackgroundColorByCharacterType(string type)
	{
		switch (type)
		{
			case "Close Combat":
				return new Color(0.0f, 0.5f, 0.5f, 0.75f);
			case "Long Distance":
				return new Color(0.5f, 0.0f, 0.5f, 0.75f);
			default:
				return new Color(0.0f, 0.0f, 0.0f, 0.75f);
		}
	}
	private void SetLabelBackground(Label label, Color bgColor, int padding=4)
	{
		StyleBoxFlat style = new StyleBoxFlat();
		style.BgColor = bgColor;     

		style.CornerRadiusTopLeft = 4;
		style.CornerRadiusTopRight = 4;
		style.CornerRadiusBottomLeft = 4;
		style.CornerRadiusBottomRight = 4;

		label.AddThemeStyleboxOverride("normal", style);
	}
	public override void _Ready()
	{
		// Sent Signal: CharacterListItem -> CharacterShow: Clicked character button to description the character.
		button.Pressed += () => {
			EmitSignal(SignalName.OnCharacterListItemButtonClicked, character_id.getFullName());
		};
		
	}


	public override void _Process(double delta)
	{
	}
}
