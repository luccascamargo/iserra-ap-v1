
namespace iserra_api.Dto
{
    public record AdvertCreateDto
    {
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
        public bool? Destaque { get; set; }
        public Guid? UsuarioId { get; set; }
        public ICollection<PhotoDto> Imagens { get; set; }
        public List<Guid> OptionalIds { get; set; }
    }
}
