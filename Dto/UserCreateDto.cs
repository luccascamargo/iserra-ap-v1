using iserra_api.Enums;
using System.Text.Json.Serialization;

namespace iserra_api.Dto
{
    public record UserCreateDto
    {
        public string StripeId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public bool Ativo { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))] public Plan Plano { get; set; } = Plan.gratis;
        public string Imagem { get; set; }
        public string Senha { get; set; }
    }
}
