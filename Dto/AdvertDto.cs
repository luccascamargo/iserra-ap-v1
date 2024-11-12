using iserra_api.Enums;

namespace iserra_api.Dto;

public record AdvertDto
{
    public Guid Id { get; set; }
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
    public DateTime DataCricao { get; set; } = DateTime.UtcNow;
    public DateTime? DataAtualizacao { get; set; }
    public Condition Condicao { get; set; } = Condition.REQUESTED;
    public string Slug { get; set; }
    public bool? Destaque { get; set; }
    public Guid? UsuarioId { get; set; }
    public AdvertUserDto Usuario { get; set; }
    public ICollection<PhotoDto> Imagens { get; set; }
    public ICollection<OptionalDto>? Opcionais { get; set; }
}