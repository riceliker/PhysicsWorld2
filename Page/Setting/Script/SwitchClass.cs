using Godot;
using System;
using System.Runtime.Serialization;

namespace PhysicsWorld.GUI.Page.Setting
{
	public partial class SwitchClass : ColorRect
	{

		[Export] private Button to_general_button;
		[Export] private Button to_windows_button;
		[Export] private Button to_input_button;
		[Export] private Button to_voice_button;
		[Export] private Button to_debug_button;
		[Export] private Control general;
		[Export] private Control windows;
		[Export] private Control input;
		[Export] private Control voice;
		[Export] private Control debug;
		public override void _Ready()
		{
			onGeneralButtonClicked();
			to_general_button.Pressed += onGeneralButtonClicked;
			to_windows_button.Pressed += onWindowsButtonClicked;
			to_input_button.Pressed += onInputButtonClicked;
			to_voice_button.Pressed += onVoiceButtonClicked;
			to_debug_button.Pressed += onDebugButtonClicked;
		}

		private void hideAllControl()
		{
			general.Visible = false;
			windows.Visible = false;
			input.Visible = false;
			voice.Visible = false;
			debug.Visible = false;
		}

		private void onGeneralButtonClicked()
		{
			hideAllControl();
			general.Visible = true;
		}

		private void onWindowsButtonClicked()
		{
			hideAllControl();
			windows.Visible = true;
		}

		private void onInputButtonClicked()
		{
			hideAllControl();
			input.Visible = true;
		}

		private void onVoiceButtonClicked()
		{
			hideAllControl();
			voice.Visible = true;
		}

		private void onDebugButtonClicked()
		{
			hideAllControl();
			debug.Visible = true;
		}

	}
}