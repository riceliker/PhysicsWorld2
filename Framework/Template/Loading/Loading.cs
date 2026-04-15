using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class Loading : Control
{
	[Export] private Label loading_label;
	[Export] private Label tips_label;
	[Export] private ProgressBar loading_progress_bar;
	[Export] private Label description_label;
	private string loading_text = "Loading";
	private float _timer = 0f;
	private List<string> tips;
	private int loading_point_number = 0;
	private int loading_process_number = 0;
	private int tips_render_time = 0;
	private int tips_index = 0;
    public override void _Ready()
    {
        var tips_file = FileAccess.Open("res://Assets/config/tips.json", FileAccess.ModeFlags.Read);
		string tips_json = tips_file.GetAsText();
		tips_file.Close();
		var json = Json.ParseString(tips_json);
        tips = new List<string>();
        foreach (var item in json.AsGodotArray())
        	tips.Add(item.ToString());
		tips_label.Text = "Tips: " + tips[0];

    }
    public override void _Process(double delta)
    {
		simpleTimer(delta);
		render();
    }
	public void setInformation(string info)
	{
		description_label.Text = info;
	}
	public void setProcess(int percent)
	{
		loading_process_number = percent;
	}
	private void render()
	{
		loading_label.Text = loading_text;
		(string info, int per) = LoadingProcess.GetLoadingInfo();
		loading_progress_bar.Value = per;
		description_label.Text = "Description: " + info;
	}
	private void simpleTimer(double delta)
	{
		_timer += (float)delta;

        if (_timer >= 0.5f)
        {
            _timer -= 0.5f; 
            loadingPoint();
			renderTips();
        }
	}
	private void loadingPoint()
	{
		loading_point_number += 1;
		loading_text = "Loading";
		if (loading_point_number == 4)
			loading_point_number = 0;
		else
		{
			for (int i = 0; i < loading_point_number; i++)
			{
				loading_text += ".";
			}
		}
	}
	private void renderTips()
	{
		tips_render_time += 1;
		if (tips_render_time == 10)
		{
			tips_render_time = 0;
			if (tips_index == tips.Count - 1)
				tips_index = 0;
			else
				tips_index++;
			tips_label.Text = "Tips: " + tips[tips_index];
		}		
		
	}

	private bool isEnd()
	{
		return loading_process_number == 100 ? true : false;
	}
}
