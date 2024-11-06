

namespace jogodopassaro;

public partial class MainPage : ContentPage
{
	int score = 0;
	const int AberturaMinima = 20;
	const int forcaPulo = 40;
	const int maxTempoPulando = 3;//frames
	bool estaPulando = false;
	int tempoPulando = 0;
	const int TempoEntreFrames = 50;
	const int Gravidade = 5;
	double LarguraJanela = 0;
	double AlturaJanela = 0;
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
				estaMorto = true;
				FrameGameOver.IsVisible = true;
				break;
			}
			else
			{
				AplicaGravidade();
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
		Passaro.TranslationY -= forcaPulo;
		tempoPulando++;
		if (tempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			tempoPulando = 0;
		}
	}
	void OnGridClicked(object sender, TappedEventArgs e)
	{
		estaPulando = true;
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
		estilinguein.TranslationX -= velocidade;

		if (estilinguein.TranslationX <= -LarguraJanela)
		{
			estilingue.TranslationX = estilingue.WidthRequest;
			estilinguein.TranslationX = estilingue.WidthRequest;

			var alturaMax = -100;
			var alturaMin = -estilinguein.HeightRequest;

			estilingue.TranslationY = Random.Shared.Next((int)alturaMin, (int)alturaMax);
			estilinguein.TranslationY = estilinguein.TranslationY + AberturaMinima + estilingue.HeightRequest;
			score++;
			LabelScore.Text = "estilingue: " + score.ToString("D3");
		}

	}


	void OnGameOverClicked(object s, TappedEventArgs e)
	{
		FrameGameOver.IsVisible = false;
		estaMorto = false;
		Inicializar();
		Desenha();
	}
	void Inicializar()
	{
		estilingue.TranslationX = -LarguraJanela;
		estilinguein.TranslationX = -LarguraJanela;
		Passaro.TranslationY = 0;
		Passaro.TranslationX = 0;
		GerenciaCanos();
	}
	bool VerificaColisaoTeto()
	{
		var minY = -AlturaJanela / 2;
		if (Passaro.TranslationY <= minY)

			return true;

		else

			return false;


	}
	bool VerificaColisaoChao()
	{
		var maxY = AlturaJanela / 2;
		if (Passaro.TranslationY >= maxY)
			return true;
		else
			return false;
	}

	bool VerificaColisao()
	{
		if (!estaMorto)
		{
			return VerificaColisaoTeto() || VerificaColisaoChao() || VerificaColisaoestiligue() || VerificaColisaoEstilinguein();
		}
		return false;
	}

	bool VerificaColisaoestiligue()
{
    var posicaoHorizontalPardal = (LarguraJanela - 50) - (Passaro.WidthRequest / 2);
    var posicaoVerticalPardal   = (AlturaJanela / 2) - (Passaro.HeightRequest / 2) + Passaro.TranslationY;

    if (
         posicaoHorizontalPardal >= Math.Abs(estilingue.TranslationX) - estilingue.WidthRequest &&
         posicaoHorizontalPardal <= Math.Abs(estilingue.TranslationX) + estilingue.WidthRequest &&
         posicaoVerticalPardal   <= estilingue.HeightRequest + estilingue.TranslationY
       )
      return true;
    else
      return false;
  }
	bool VerificaColisaoEstilinguein()
	{
    var posicaoHorizontalPardal = LarguraJanela - 50 - Passaro.WidthRequest / 2;
    var posicaoVerticalPardal   = (AlturaJanela / 2) + (Passaro.HeightRequest / 2) + Passaro.TranslationY;

    var yMaxCano = estilingue.HeightRequest + estilingue.TranslationY + AberturaMinima;

    if (
         posicaoHorizontalPardal >= Math.Abs(estilingue.TranslationY) - estilingue.WidthRequest &&
         posicaoHorizontalPardal <= Math.Abs(estilingue.TranslationY) + estilingue.WidthRequest &&
         posicaoVerticalPardal   >= yMaxCano
       )
      return true;
    else
      return false;
  }

}



