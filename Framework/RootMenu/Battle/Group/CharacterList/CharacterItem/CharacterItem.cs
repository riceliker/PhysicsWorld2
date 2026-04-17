using Godot;



public partial class CharacterItem : DataListItem<CharacterInformation>
{
	[Export] private Label label;
	public override void setInformationCanOverride(CharacterInformation info)
	{
		CharacterInformation character = info;
		label.Text = character.description.name;
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
}
