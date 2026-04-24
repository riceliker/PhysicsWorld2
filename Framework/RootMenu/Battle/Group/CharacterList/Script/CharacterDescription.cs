using Godot;
using System;

public partial class CharacterDescription : ListItemDescription
{
	[Signal] public delegate void OnChooseCharacterButtonClickedEventHandler(string full_name);
	[Export] private Label name;
	[Export] private Label type;
	[Export] private Label hp;
	[Export] private Label speed;
	[Export] private Label skill;
	[Export] private Label ultimate_skill;
	[Export] private TextEdit description;
	[Export] private Button choose_button;
	public override void setCharacterDescription(DataUniqueID id)
	{
		CharacterInformation info = DataManager.getInformation<CharacterInformation>(id);
		name.Text = info.description.name;
		type.Text = info.description.type;
		hp.Text = "HP: " + info.basic.HP.ToString();
		speed.Text = "Speed: " +info.basic.Speed.ToString();
		skill.Text = "E-Skill: \n" + info.description.skill_name;
		ultimate_skill.Text = "Q-Skill: \n" + info.description.ultimate_skill_name;
		description.Text = info.description.description;
		choose_button.Pressed += () => 
		{
			// Signal: CharacterShow -> Group: Clicked choose character button to start the game with the character.
			EmitSignal(SignalName.OnChooseCharacterButtonClicked, id.getFullName());
		};
	}
	private void clear()
	{
		name.Text = "Please select a character.";
		type.Text = "?";
		hp.Text = "HP: NaN";
		speed.Text = "Speed: NaN";
		skill.Text = "E-Skill: ?";
		ultimate_skill.Text = "R-Skill: ?";
		description.Text = "Description: No Description.";
	}
	
}
