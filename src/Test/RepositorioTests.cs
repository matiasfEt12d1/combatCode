using System.Data;
using Moq;
using Persistencia;
using Persistencia.Entidades;
using Persistencia.Repositorios;

namespace Test;

public class RepositorioTests
{
    private readonly Mock<IConexionDapper> _dapperMock;
    private readonly PersonajeRepositorio _personajeRepositorio;
    private readonly UsuarioRepositorio _usuarioRepositorio;

    public RepositorioTests()
    {
        _dapperMock = new Mock<IConexionDapper>();
        _personajeRepositorio = new PersonajeRepositorio(_dapperMock.Object);
        _usuarioRepositorio = new UsuarioRepositorio(_dapperMock.Object);
    }

    [Fact]
    public async Task PersonajeRepositorio_ObtenerPorIdAsync_LlamaAQueryFirstAsync()
    {
        var personajeEnBD = new PersonajeTabla { Id = 1, Nombre = "Thor", TipoPersonaje = "Guerrero" };
        
        _dapperMock
            .Setup(d => d.QueryFirstAsync<PersonajeTabla>(It.IsAny<string>(), It.IsAny<object>(), CommandType.Text))
            .ReturnsAsync(personajeEnBD);

        var resultado = await _personajeRepositorio.ObtenerPorIdAsync(1);

        Assert.NotNull(resultado);
        Assert.Equal("Thor", resultado!.Nombre);
        _dapperMock.Verify(d => d.QueryFirstAsync<PersonajeTabla>(
            It.Is<string>(sql => sql.Contains("SELECT * FROM Personajes")),
            It.IsAny<object>(),
            CommandType.Text), Times.Once);
    }

    [Fact]
    public async Task UsuarioRepositorio_CrearUsuarioAsync_LlamaAQueryFirstAsyncConConsultaInsert()
    {
        _dapperMock
            .Setup(d => d.QueryFirstAsync<int>(It.IsAny<string>(), It.IsAny<object>(), CommandType.Text))
            .ReturnsAsync(5);

        int idCreado = await _usuarioRepositorio.CrearUsuarioAsync("JuanDev", "HASH_123");

        Assert.Equal(5, idCreado);
        _dapperMock.Verify(d => d.QueryFirstAsync<int>(
            It.Is<string>(sql => sql.Contains("INSERT INTO Usuarios")),
            It.IsAny<object>(),
            CommandType.Text), Times.Once);
    }

    [Fact]
    public async Task PersonajeRepositorio_ActualizarVidaAsync_RetornaTrueSiAfectaFilas()
    {
        _dapperMock
            .Setup(d => d.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), CommandType.Text))
            .ReturnsAsync(1);

        bool resultado = await _personajeRepositorio.ActualizarVidaAsync(1, 80.0);

        Assert.True(resultado);
        _dapperMock.Verify(d => d.ExecuteAsync(
            It.Is<string>(sql => sql.Contains("UPDATE Personajes SET VidaActual")),
            It.IsAny<object>(),
            CommandType.Text), Times.Once);
    }
}