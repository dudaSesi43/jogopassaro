using CoreMedia;
using UIKit;

namespace jogodopassaro;

public partial class MainPage : ContentPage
{
	const int granidade = 1; //
	const int tempoEntreFrames =Z5; //
	bool estaMorto = false;

	


	public MainPage()
	{
		InitializeComponent();
	}
	void AplicaGravidade()
	{
		bird.png.TranslationY+= gravidade;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		Desenha();
    }
	Async tase Desenha()
	{
	Whice (!estaMorto)
	}
		Aplica Gravidade()
		Await Task.Delay (TempoEntreFrames);
	}

}

