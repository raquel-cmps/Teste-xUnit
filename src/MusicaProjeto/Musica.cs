namespace MusicaProjeto;

public class Musica
{
    private int? anoLancamento;
    private string? artista;
    public string Nome { get; set; }
    public int Id { get; set; }

    public string? Artista
    {
        get => artista;
        set
        {
            artista = value;
            if (artista == null)
            {
                Artista = "Artista desconhecido";
            }
        }
    }

    public int? AnoLancamento
    {
        get => anoLancamento;
        set
        {
            anoLancamento = value;
            if (anoLancamento <= 1)
            {
                AnoLancamento = null;
            }
        }
    }
    
    public Musica(string nome)
    {
        Nome = nome;
    }
    
    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");

    }

    public override string ToString()
    {
        return @$"Id: {Id} Nome: {Nome}";
    }
}