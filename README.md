# DesafioMovieDb

API apresenta os próximos filmes a serem lançados no cinema utilizando a API do TheMovieDb.org

## Build


## Tecnologias

* [.NET Core 3.](https://dotnet.microsoft.com/download)
* [ASP.NET Core 3.0](https://docs.microsoft.com/en-us/aspnet/core)
* [C# 8.0](https://docs.microsoft.com/en-us/dotnet/csharp)

* [Newtonsoft.Json](https://www.newtonsoft.com/json)
Este é um serializador Json de alto desempenho.
Utilizei para anotar as propriedades da classes de retorno.
É um componete que tem grande participação da comunidade de desenvolvimento.

```csharp
[JsonObject("results")]
public class Filme
{
	public int Id { get; set; }

	[JsonProperty("popularity")]
	public double Popularidade { get; set; }

	[JsonProperty("vote_count")]
	public int Votos { get; set; }

	public bool Video { get; set; }

	[JsonProperty("poster_path")]
	public string PosterPath { get; set; }
}
```

* [Scrutor](https://github.com/khellang/Scrutor)
Este é uma extensão do Microsoft.Extensions.DependencyInjection.
Utilizei para escannear as classes para suas devidas interfaces de forma mais fluente

```csharp
public static class ServiceCollectionExtensions
{
	public static void AddClassesMatchingInterfaces(
	   this IServiceCollection services,
	   params Assembly[] assemblies)
	{
		services.Scan(scan => scan
			.FromAssemblies(assemblies)
			.AddClasses()
			.UsingRegistrationStrategy(RegistrationStrategy.Skip)
			.AsMatchingInterface()
			.WithScopedLifetime());
	}
}
```
* [RestSharp](http://restsharp.org/)
Está é uma biblioteca cliente para RestAPI muito popular.
Também possui grande participação da comunidade de desenvolvimento.

```csharp
public Estreias ObterEstreias(int pagina = 1)
{
	var request = new RestRequest
	{
		Method = Method.GET,
		Resource = "movie/upcoming",
	};

	request.AddParameter("api_key", this.endpoints.TheMovieDbSettings.Key, ParameterType.QueryString);
	request.AddParameter("page", pagina, ParameterType.QueryString);

	var response = this.Executar(request);

	var estreias = JsonConvert.DeserializeObject<Estreias>(response.Content);

	return estreias;
}
```

* [xUnit](https://xunit.net/)
O xUnit.net é uma ferramenta de teste de unidade gratuita, de código aberto e focada na comunidade para o .NET Framework
A utilização de testes no desenvolvimento de sistemas é uma pratica para 
1. Desenhar ou projetar de forma mais concisa e diminuindo o acoplamento;
2. Uma forma de adicionar novas funcionalidades ao sistema de forma segura e controlada;

* [moq](https://github.com/moq/moq4)
É uma ferramenta muito simples para mockar objetos e executar testes de comportamento.

```csharp
public class FilmesControllerTest
{
	/// <summary>
	/// Testar o comportamento básico da controller
	/// </summary>
	[Fact]
	public void DeveRetornarFilmesEstreando()
	{
		//// Cenario
		var consultaFilmeServico = new Mock<IConsultaFilmeServico>();
		
		var estreias = new Estreias()
		{
			Filmes = new System.Collections.Generic.List<Filme>
			{
				new Filme { Titulo = "Era do gelo" }
			}
		};

		consultaFilmeServico
			.Setup(s => s.ObterTodasEstreias())
			.Returns(estreias);

		//// Execução
		var controller = new FilmesController(consultaFilmeServico.Object);
		var result = controller.Estreias();

		//// Verificação
		var viewResult = Assert.IsType<OkObjectResult>(result);

		var model = Assert.IsAssignableFrom<Estreias>(viewResult.Value);
		model.Filmes.Count.Should().Be(1);
	}
}
```
* [FluentAssertions](https://fluentassertions.com/)
Permite uma especificação mais fluente dos Asserts dos testes automatizados.

```csharp
[Fact]
public void DeveRetornarEstreiasNaoLimitadoA20Resultados()
{
	var theMovieDb = new TheMovieDb(this.endpoints);

	var resultado = theMovieDb.ObterTodasEstreias();

	resultado.Filmes.Count.Should().BeGreaterThan(20);
}
```

## Práticas

* Clean Code
Procurei manter o código o mais simples;
Procurei não me repetir para não duplicar codigo;
Código está bem estrutura e identado sem espaços desnecessários

* SOLID
Cada classe e método desenvolvido possui responsabilidade única (Open-Closed Principle);
Uma classe não deve ser forçada a implementar interfaces e métodos que não irão utilizar (Interface Segregation Principle);
As classes dependem de abstrações e não de implementações (Dependency Inversion Principle);

* Separation of Concerns
Separados em 
Servicos, Web, Testes

## Camadas

**Web:** API.

**Aplicacao:** Controle de fluxo.

**Dominio:** Regras e lógica do negócio.

**Modelo:** Transferencia de objetos.

