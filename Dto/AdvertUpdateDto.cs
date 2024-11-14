
namespace iserra_api.Dto {
    public record AdvertUpdateDto {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Cor { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public int Preco { get; set; }
        public string Portas { get; set; }
        public int Quilometragem { get; set; }
        public string Descricao { get; set; }
        public string Cambio { get; set; }          
        public ICollection<PhotoDto> Imagens { get; set; }
        public List<Guid> Opcionais { get; set; }
    }
}
