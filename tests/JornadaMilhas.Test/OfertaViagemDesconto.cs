using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test;

public class OfertaViagemDesconto
{
    [Fact]
    public void RetornaPrecoAtualizadoQuandoAplicadoDesconto() // TDD
    {
        // arange
        Rota rota = new Rota("Origem", "Destino");
        Periodo periodo = new Periodo(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
        double precoOriginal = 100.0;
        double desconto = 20.0;

        double precoComDesconto = precoOriginal - desconto;
        OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);
        
        // act
        oferta.Desconto = desconto;
        
        // assert
        Assert.Equal(precoComDesconto, oferta.Preco);
    }
    
    [Theory]
    [InlineData(120, 30)]
    [InlineData(100, 30)]
    public void RetornaDescontoMaximoQuandoValorDescontoMaiorOuIgualQuePreco(double desconto, double precoComDesconto) // TDD
    {
        // arange
        Rota rota = new Rota("Origem", "Destino");
        Periodo periodo = new Periodo(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
        double precoOriginal = 100.0;
        
        OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);
        
        // act
        oferta.Desconto = desconto;
        
        // assert
        Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
    }
    
    [Fact]
    public void RetornaPrecoOriginalQuandoValorDescontoNegativo() // TDD
    {
        // arange
        Rota rota = new Rota("Origem", "Destino");
        Periodo periodo = new Periodo(new DateTime(2024, 05, 01), new DateTime(2024, 05, 10));
        double precoOriginal = 100.0;
        double desconto = -120.0;

        double precoComDesconto = precoOriginal;
        OfertaViagem oferta = new OfertaViagem(rota, periodo, precoOriginal);
        
        // act
        oferta.Desconto = desconto;
        
        // assert
        Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
    }

    [Fact]
    public void RetornaTresErrosDeValidacaoQuandoPeriodoEPrecoSaoInvalidos()
    {
        int quantidadeEsperada = 3;
        
        Rota rota = null;
        Periodo periodo = new Periodo(new DateTime(2024, 06, 01), new DateTime(2024, 05, 10));
        double preco = -100;
        
        OfertaViagem oferta = new OfertaViagem(rota, periodo, preco);
        
        Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
    }
}