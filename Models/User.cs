using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using iserra_api.Enums;

namespace iserra_api.Models;

public sealed class User
{
    [Key] public Guid Id { get; set; }
    public string StripeId { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public bool Ativo { get; set; }
    public Plan Plano { get; set; } = Plan.gratis;
    [DataType(DataType.DateTime)] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Imagem { get; set; }
    public string Senha { get; set; }
    [JsonIgnore]
    public ICollection<Advert>? Anuncios { get; set; }
    
    private User() {}

    public User(string stripeId, string nome, string sobrenome, string email, string telefone, bool ativo, Plan plano, string imagen, string senha, ICollection<Advert> anuncios)
    {
        Id = Guid.NewGuid();
        StripeId = stripeId;
        Nome = nome;
        Sobrenome = sobrenome;
        Email = email;
        Telefone = telefone;
        Ativo = ativo;
        Plano = plano;
        Imagem = imagen;
        Senha = senha;
        Anuncios = anuncios;
    }
}