using MusicaProjeto;

namespace MusicaTest
{
    public class MusicaConstrutor
    {
        [Theory]
        [InlineData("Qual o seu desejo?")]
        [InlineData("Solto")]
        public void RetornaMusicaQuandoNomeIniciadoCorretamente(string nome) // verificando se o nome foi inicializado corretamente
        {
            // act
            Musica musica = new Musica(nome);
        
            // assert
            Assert.Equal(nome, musica.Nome);
        }
    
        [Theory]
        [InlineData(1, "Qual o seu desejo?")]
        [InlineData(2, "Solto")]
        public void RetornaIdQuandoIdIniciadoCorretamente(int id, string nome) // verificando se o id foi inicializado corretamente
        {
            // act
            Musica musica = new Musica(nome) {Id = id};
        
            // assert
            Assert.Equal(id, musica.Id);
        }
    
        [Theory]
        [InlineData(1, "Música Teste", "Id: 1 Nome: Música Teste")]
        [InlineData(2, "Outra Música", "Id: 2 Nome: Outra Música")]
        [InlineData(3, "Mais uma Música", "Id: 3 Nome: Mais uma Música")]
        public void RetornaToStringQuandoStringInstanciadaCorretamenete(int id, string nome, string toStringEsperado) // verifica a saida do metodo toString()
        {
            Musica musica = new Musica(nome);
            musica.Id = id;

            // Act
            string resultado = musica.ToString();

            // Assert
            Assert.Equal(toStringEsperado, resultado);
        }

        [Theory]
        [InlineData("Música Teste", 0)]
        [InlineData("Música Teste", -1)]
        public void RetornaAnoLancamentoComoNuloQuandoForMenorOuIgualUm(string nome, int anoInvalido)
        {
            Musica musica = new Musica(nome);
            
            musica.AnoLancamento = anoInvalido;
            
            Assert.Null(musica.AnoLancamento);
        }
        
        [Fact]
        public void RetornaArtistaDesconhecidoQuandoArtistaForNull()
        {
            Musica musica = new Musica("nome");

            musica.Artista = null;
            
            Assert.Equal("Artista desconhecido", musica.Artista);
        }
    }
}