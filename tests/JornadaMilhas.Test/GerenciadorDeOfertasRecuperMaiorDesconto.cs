using Bogus;
using JornadaMilhasV1.Gerencidor;
using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test;

public class GerenciadorDeOfertasRecuperMaiorDesconto
{
    [Fact]
    public void RetornaOfertaNulaQuandoListaEstaVazia()
    {
        var lista = new List<OfertaViagem>();
        var gerenciador = new GerenciadorDeOfertas(lista);
        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");

        var oferta = gerenciador.RecuperaMaiorDesconto(filtro);

        Assert.Null(oferta);
    }

    [Fact]
    // destino = são paulo, deconto = 40, preco = 80
    public void RetornaOfertaEspecificaQuandoDestinoSaoPauloEDesconto40()
    {
        // arrange
        var fakerPeriodo = new Faker<Periodo>()
            .CustomInstantiator(f =>
            {
                DateTime dataInicio = f.Date.Soon();
                return new Periodo(dataInicio, dataInicio.AddDays(30));
            });
        var rota = new Rota("Curitiba", "São Paulo");

        var fakerOferta = new Faker<OfertaViagem>().CustomInstantiator(f => new OfertaViagem
            (
                rota,
                fakerPeriodo.Generate(),
                100 * f.Random.Int(1, 100) // gera preco acima de 100 reais
            ))
            .RuleFor(o => o.Desconto, f => 40)
            .RuleFor(o => o.Ativa, true);

        var ofertaEscolhida = new OfertaViagem(rota, fakerPeriodo, 80)
        {
            Desconto = 40,
            Ativa = true
        };

        var ofertaInativa = new OfertaViagem(rota, fakerPeriodo, 70)
        {
            Desconto = 40,
            Ativa = false
        };
        
        var lista = fakerOferta.Generate(200);
        lista.Add(ofertaEscolhida);
        lista.Add(ofertaInativa);
        
        var gerenciador = new GerenciadorDeOfertas(lista);
        Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
        var precoEsperado = 40; // 80 - 40

        // act
        var oferta = gerenciador.RecuperaMaiorDesconto(filtro);

        // assert
        Assert.NotNull(oferta);
        Assert.Equal(precoEsperado, oferta.Preco, 0.0001);
    }
}