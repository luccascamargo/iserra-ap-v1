using System.ComponentModel.DataAnnotations;
using iserra_api.Enums;
using Microsoft.EntityFrameworkCore;

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
    public Plan Plano { get; set; } = Plan.GRATIS;
    [DataType(DataType.DateTime)] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Imagen { get; set; }
    public string Senha { get; set; }
    public ICollection<Advert> Anuncios { get; set; }
    
    private User() {}

    public User(string stripeId, string nome, string sobrenome, string email, string telefone, bool ativo, Plan plano, string imagen, string senha, ICollection<Advert> anuncios)
    {
        Id = new Guid();
        StripeId = stripeId;
        Nome = nome;
        Sobrenome = sobrenome;
        Email = email;
        Telefone = telefone;
        Ativo = ativo;
        Plano = plano;
        Imagen = imagen;
        Senha = senha;
        Anuncios = anuncios;
    }
}