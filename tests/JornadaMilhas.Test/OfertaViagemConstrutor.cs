using JornadaMilhasV1.Modelos;

namespace JornadaMilhas.Test
{
    public class OfertaViagemConstrutor
    {
        [Theory] // varias verificações com o mesmo teste
        [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)] // invalida
        [InlineData("OrigemTeste", "DestinoTeste", "2024-03-01", "2024-03-05", 100, true)] // valida
        [InlineData(null, "São Paulo", "2024-01-01", "2024-01-02", -1, false)]
        [InlineData("Vitória", "São Paulo", "2024-01-01", "2024-01-01", 0, false)]
        [InlineData("Rio de Janeiro", "São Paulo", "2024-01-01", "2024-01-02", -500, false)]
        public void RetornaEhValidoDeAcordoComDadosDeEntrada(string origem, string destino, string dataIda, string dataVolta, double preco, bool validacao)
        {
            Rota rota = new Rota(origem, destino);
            Periodo periodo = new Periodo(DateTime.Parse(dataIda), DateTime.Parse(dataVolta));

            OfertaViagem ofertaViagem = new OfertaViagem(rota, periodo, preco);
        
            Assert.Equal(validacao, ofertaViagem.EhValido);
        }
        
        [Fact]
        public void RetornaErroDeRotaQuandoRotaOuPeriodoInvalidosQuandoRotaNula()
        {
            // PADRAO AAA
        
            // cenario - arrange
            Rota rota = null;
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;
            
            // açao - act
            OfertaViagem ofertaViagem = new OfertaViagem(rota, periodo, preco);
        
            // validaçoes - acert
            Assert.Contains("A oferta de viagem não possui rota ou período válidos", ofertaViagem.Erros.Sumario);
            Assert.False(ofertaViagem.EhValido);
        }
    
        [Fact]
        public void RetornaErroDeDataQuandoDataInicialMenorDataFinal()
        {
            // PADRAO AAA
        
            // cenario - arrange
        
            Rota rota = new Rota("Origiem", "Destino");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 1, 5));
            double preco = 100.0;

            // açao - act
            OfertaViagem ofertaViagem = new OfertaViagem(rota, periodo, preco);
        
            // validaçoes - acert
            Assert.Contains("Erro: Data de ida não pode ser maior que a data de volta.", ofertaViagem.Erros.Sumario);
            Assert.False(ofertaViagem.EhValido);
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(0)]
        public void RetornaErroDePrecoInvalidoQuandoPrecoMenorQueZero(double preco)
        {
            Rota rota = new Rota("Origem1", "Destino1");
            Periodo periodo = new Periodo(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
        
            OfertaViagem ofertaViagem = new OfertaViagem(rota, periodo, preco);
        
            Assert.Contains("O preço da oferta de viagem deve ser maior que zero.", ofertaViagem.Erros.Sumario);
        }
    }
}