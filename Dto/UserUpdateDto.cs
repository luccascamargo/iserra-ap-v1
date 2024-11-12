
namespace iserra_api.Dto {
    public record UserUpdateDto {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public string Imagem { get; set; }
        public string Senha { get; set; }
    }
}
