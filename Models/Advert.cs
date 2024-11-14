using System.ComponentModel.DataAnnotations;
using iserra_api.Enums;

namespace iserra_api.Models;

public sealed class Advert
{
    [Key] public Guid Id { get; set; }
    public string Tipo { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public int AnoModelo { get; set; }
    public string Cor { get; set; }
    public string Cep { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public int Preco { get; set; }
    public string Portas { get; set; }
    public int Quilometragem { get; set; }
    public string Descricao { get; set; }
    public string Placa { get; set; }
    public string Cambio { get; set; }
    [DataType(DataType.DateTime)] public DateTime DataCricao { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime? DataAtualizacao { get; set; }
    public Condition Condicao { get; set; } = Condition.REQUESTED;
    public string Slug { get; set; }
    public bool? Destaque { get; set; }
    public Guid? UsuarioId { get; set; }
    public User Usuario { get; set; }
    public ICollection<Photo> Imagens { get; set; }
    public ICollection<Optional>? Opcionais { get; set; }
    
    private Advert() {}

    public Advert(string tipo, string marca, string modelo, int anoModelo, string cor, string cep,
        string cidade, string estado, int preco, string portas, int quilometragem, string descricao, string placa,
        string cambio, DateTime dataAtualizacao, Condition condicao, string slug, bool? destaque, Guid? usuarioId,
        User usuario,
        ICollection<Photo> imagens, ICollection<Optional>? opcionais)
    {
        Id = Guid.NewGuid();
        Tipo = tipo;
        Marca = marca;
        Modelo = modelo;
        AnoModelo = anoModelo;
        Cor = cor;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
        Preco = preco;
        Portas = portas;
        Quilometragem = quilometragem;
        Descricao = descricao;
        Placa = placa;
        Cambio = cambio;
        DataAtualizacao = dataAtualizacao;
        Condicao = condicao;
        Slug = slug;
        Destaque = destaque;
        UsuarioId = usuarioId;
        Usuario = usuario;
        Imagens = imagens;
        Opcionais = opcionais;
    }
}