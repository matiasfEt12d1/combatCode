using Aplicacion.Interfaces;
using Moq;
using Persistencia.Entidades;

namespace Test;

public class UsuarioTests
{
    private readonly Mock<IUsuarioRepositorio> _usuarioRepositorioMock;

    public UsuarioTests()
    {
        _usuarioRepositorioMock = new Mock<IUsuarioRepositorio>();
    }

    [Fact]
    public async Task ObtenerUsuario_UsuarioExistente_RetornaUsuarioTabla()
    {
        var usuarioEsperado = new UsuarioTabla { Id = 1, NombreUsuario = "Gamer123" };
        _usuarioRepositorioMock
            .Setup(repo => repo.ObtenerPorIdAsync(1))
            .ReturnsAsync(usuarioEsperado);

        var resultado = await _usuarioRepositorioMock.Object.ObtenerPorIdAsync(1);

        Assert.NotNull(resultado);
        Assert.Equal("Gamer123", resultado!.NombreUsuario);
        _usuarioRepositorioMock.Verify(repo => repo.ObtenerPorIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task CrearUsuario_DatosValidos_InvocaRepositorioYRetornaId()
    {
        _usuarioRepositorioMock
            .Setup(repo => repo.CrearUsuarioAsync("NuevoUsuario", "hash_pass"))
            .ReturnsAsync(10);

        int idGenerado = await _usuarioRepositorioMock.Object.CrearUsuarioAsync("NuevoUsuario", "hash_pass");

        Assert.Equal(10, idGenerado);
        _usuarioRepositorioMock.Verify(repo => repo.CrearUsuarioAsync("NuevoUsuario", "hash_pass"), Times.Once);
    }

    [Fact]
    public async Task ObtenerUsuario_Inexistente_RetornaNull()
    {
        _usuarioRepositorioMock
            .Setup(repo => repo.ObtenerPorIdAsync(It.IsAny<int>()))
            .ReturnsAsync((UsuarioTabla?)null);

        var resultado = await _usuarioRepositorioMock.Object.ObtenerPorIdAsync(999);

        Assert.Null(resultado);
    }
}