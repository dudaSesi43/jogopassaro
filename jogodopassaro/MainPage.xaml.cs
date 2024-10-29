

namespace jogodopassaro;

public partial class MainPage : ContentPage
{
	int score=0;
	const int AberturaMinima=200;
	const int forcaPulo=30;
	const int maxTempoPulando=3;//frames
	bool estaPulando=false;	
	int tempoPulando=0;
	const int TempoEntreFrames = 25;
	const int Gravidade = 6;
	double LarguraJanela;
	double AlturaJanela;
	int velocidade = 10;
	bool estaMorto = true;
	public MainPage()
	{
		InitializeComponent();
	}

  public async void Desenha()
  {
	while (!estaMorto)
	{
		GerenciaCanos();
		if (VerificaColisao())
		{
			estaMorto=true;
			FrameGameOver.IsVisible=true;
			break;
		}
		if (estaPulando)
		  AplicaPulo();
		else
			AplicaGravidade();
		await Task.Delay(TempoEntreFrames);
	}
  }
	void AplicaPulo()
	{
		Passaro.TranslationY-=forcaPulo;
		tempoPulando++;
		if (tempoPulando >= maxTempoPulando)
		{
			estaPulando=false;
			tempoPulando=0;
		}
	}
	void OnGridCliked(object sender, TappedEventArgs e)
	{
		estaPulando=true;
	}
	void AplicaGravidade()
	{
		Passaro.TranslationY += Gravidade;
	
	}

	
	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		LarguraJanela = w;
		AlturaJanela = h;
		
	}
	void GerenciaCanos()
	{
		estilingue.TranslationX -= velocidade;
		estilingue.TranslationX -= velocidade;

		if (estilinguein.TranslationX <- LarguraJanela)
		{
			estilingue.TranslationX = 0;
			estilinguein.TranslationX = 0;
			var alturaMax=-100;
			var alturaMin=-estilinguein.HeightRequest;
			estilingue.TranslationY=Random.Shared.Next((int)alturaMin, (int)alturaMax);
			estilinguein.TranslationY=estilinguein.TranslationY+AberturaMinima+estilingue.HeightRequest;
			score++;
			LabelScore.Text="estilingue: "+score.ToString("D3");
		}
	
	}


	void OnGameOverCliked (object s, TappedEventArgs e)
	{
		FrameGameOver.IsVisible = false;
		estaMorto = false;
		Inicializar();
		Desenha();
	}
	void Inicializar()
	{
		estilingue.TranslationX=-LarguraJanela;
		estilingue.TranslationY=-LarguraJanela;
		estilinguein.TranslationX=-LarguraJanela;
		estilinguein.TranslationY=-LarguraJanela;
		Passaro.TranslationY = 0;
		Passaro.TranslationX = 0;
		GerenciaCanos();
	}
	bool VerificaColisaoTeto()
	{
		var minY=-AlturaJanela/2;
		if (Passaro.TranslationY <= minY)
		
		return true;
		
		else
		
		return false;
		
		
	}
	bool VerificaColisaoChao()
	{
		var maxY=AlturaJanela/2;
		if(Passaro.TranslationY >= maxY)
		return true;
		else 
		return false;
	}

	bool VerificaColisao()
	{
		if(! estaMorto)
		{
			return VerificaColisaoTeto() || VerificaColisaoChao()|| VerificaColisaoestiligue() || VerificaColisaoEstilinguein();
		}
		return false;
	}

	bool VerificaColisaoestiligue()
	{
		var posHPassaro=(LarguraJanela/2)-(Passaro.WidthRequest/2);
		var posVPassaro=(AlturaJanela/2)-(Passaro.HeightRequest/2)+Passaro.TranslationY;
		if (posHPassaro >=Math.Abs(estilingue.TranslationX)-estilingue.WidthRequest&&
			posHPassaro<=Math.Abs(estilingue.TranslationX)+estilingue.WidthRequest&&
			posVPassaro<=estilingue.HeightRequest+Passaro.TranslationY)
			{
				return true;
			}
			else
			{
				return false;
			}
	}
	bool VerificaColisaoEstilinguein()
	{
		var PosHPassaro=(LarguraJanela/2)-(Passaro.WidthRequest/2);
		var posVPassaro=(AlturaJanela/2)-(Passaro.HeightRequest/2)+Passaro.TranslationY;
		if (posHPassaro >=Math.Abs(estilinguein.TranslationX)-estilinguein.WidthRequest&&
		    posHPassaro<=Math.Abs(estilinguein.TranslationX)+estilinguein.WidthRequest&&
			posVPassaro<=estilinguein.HeightRequest+Passaro.TranslationY)
			{
				return true;
			}
			else
			{
				return false;
			}

	}

}



