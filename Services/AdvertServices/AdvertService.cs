using iserra_api.Data;
using iserra_api.Dto;
using iserra_api.Enums;
using iserra_api.Models;
using iserra_api.Responses;
using Microsoft.EntityFrameworkCore;

namespace iserra_api.Services.AdvertServices {
    public class AdvertService : IAdvertService {
        private readonly ApplicatonDbContext _context;

        public AdvertService(ApplicatonDbContext context) {
            _context = context;
        }
        public async Task<ResponseModel<AdvertDto>> Create(AdvertCreateDto advertCreateDto) {

            var response = new ResponseModel<AdvertDto>();

            var slug = $"{advertCreateDto.Marca}-{advertCreateDto.Modelo}-{advertCreateDto.AnoModelo}-{Guid.NewGuid()}";


            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == advertCreateDto.UsuarioId);

            if (user == null) {
                //tratar
                response.Mensagem = "Usuario nao encontrado";
                return response;
            }

            var optionals = await _context.Optionals.Where(opt => advertCreateDto.OptionalIds.Contains(opt.Id)).ToListAsync();

            if (optionals.Count != advertCreateDto.OptionalIds.Count) {
                response.Mensagem = "Um ou mais opcionais não encontrados";
                return response;
            }

            var imagens = advertCreateDto.Imagens.Select(img => new Photo(img.Url, img.Chave)).ToList();

            var advert = new Advert(advertCreateDto.Tipo, advertCreateDto.Marca, advertCreateDto.Modelo,
                advertCreateDto.AnoModelo, advertCreateDto.Cor, advertCreateDto.Cep,
                advertCreateDto.Cidade, advertCreateDto.Estado, advertCreateDto.Preco,
                advertCreateDto.Portas, advertCreateDto.Quilometragem, advertCreateDto.Descricao,
                advertCreateDto.Placa, advertCreateDto.Cambio, DateTime.UtcNow, Condition.REQUESTED,
                slug,
                advertCreateDto.Destaque, advertCreateDto.UsuarioId, user, imagens, optionals);

            await _context.AddAsync(advert);
            await _context.SaveChangesAsync();

            var advertDto = new AdvertDto() {
                AnoModelo = advert.AnoModelo,
                Cambio = advert.Cambio,
                Cep = advert.Cep,
                Cidade = advert.Cidade,
                Cor = advert.Cor,
                Condicao = advert.Condicao,
                DataCricao = advert.DataCricao,
                DataAtualizacao = advert.DataAtualizacao,
                Descricao = advert.Descricao,
                Destaque = advert.Destaque,
                Estado = advert.Estado,
                Id = advert.Id,
                Imagens = advert.Imagens.Select(i => new PhotoDto {
                    Chave = i.Chave,
                    Url = i.Url,
                }).ToList(),
                Marca = advert.Marca,
                Modelo = advert.Modelo,
                Placa = advert.Placa,
                Portas = advert.Portas,
                Quilometragem = advert.Quilometragem,
                Preco = advert.Preco,
                Tipo = advert.Tipo,
                Slug = advert.Slug,
                Opcionais = advert.Opcionais.Select(opt => new OptionalDto { Name = opt.Nome}).ToList(),
                Usuario = new AdvertUserDto {
                    Email = advert.Usuario.Email,
                    Nome = advert.Usuario.Nome,
                    Imagem = advert.Usuario.Imagem,
                    Sobrenome = advert.Usuario.Sobrenome,
                    Id = advert.Id,
                    Telefone = advert.Usuario.Telefone

                }
            };

            response.Dados = advertDto;
            response.Mensagem = "Anuncio criado com sucesso";

            return response;
        }

        public async Task<ResponseModel<AdvertDto>> Delete(Guid id) {
            var response = new ResponseModel<AdvertDto>();

            var advert = await _context.Adverts.FindAsync(id);

            if (advert == null) {
                response.Mensagem = "Anuncio nao encontrado";
                return response;
            }

            _context.Adverts.Remove(advert);
            await _context.SaveChangesAsync();

            response.Mensagem = "Anuncio excluido com sucesso";


            return response;

        }

        public async Task<ResponseModel<List<AdvertDto>>> GetAll() {
            var response = new ResponseModel<List<AdvertDto>>();

            var result = await _context
                .Adverts
                .Include(x => x.Imagens)
                .Include(x => x.Opcionais)
                .Include(x => x.Usuario)
                    .Select(x =>
                        new AdvertDto {
                            AnoModelo = x.AnoModelo,
                            Cambio = x.Cambio,
                            Condicao = x.Condicao,
                            Cidade = x.Cidade,
                            Cep = x.Cep,
                            Cor = x.Cor,
                            DataAtualizacao = x.DataAtualizacao,
                            DataCricao = x.DataCricao,
                            Descricao = x.Descricao,
                            Destaque = x.Destaque,
                            Estado = x.Estado,
                            Id = x.Id,
                            Marca = x.Marca,
                            Placa = x.Placa,
                            Modelo = x.Modelo,
                            Portas = x.Portas,
                            Preco = x.Preco,
                            Tipo = x.Tipo,
                            Quilometragem = x.Quilometragem,
                            Slug = x.Slug,
                            Imagens = x.Imagens.Select(img => new PhotoDto { Chave = img.Chave, Url = img.Url }).ToList(),
                            Usuario = new AdvertUserDto {
                                Id = x.Usuario.Id,
                                Nome = x.Usuario.Nome,
                                Email = x.Usuario.Email,
                                Imagem = x.Usuario.Imagem,
                                Sobrenome = x.Usuario.Sobrenome,
                                Telefone = x.Usuario.Telefone
                            },
                            Opcionais = x.Opcionais.Select(opt => new OptionalDto { Name = opt.Nome}).ToList(),
                            UsuarioId = x.UsuarioId
                        })
                                .ToListAsync();

            response.Dados = result;

            return response;
        }

        public async Task<ResponseModel<AdvertDto>> GetWithId(Guid id) {
            var response = new ResponseModel<AdvertDto>();

            var advert = await _context.Adverts
                .Include(ad => ad.Imagens)
                .Include(ad => ad.Opcionais)
                .Include(ad => ad.Usuario)
                .FirstOrDefaultAsync(ad => ad.Id == id);

            if (advert == null) {
                response.Mensagem = "Anuncio nao encontrado";
                return response;
            }

            var advertDto = new AdvertDto {
                AnoModelo = advert.AnoModelo,
                Cambio = advert.Cambio,
                Condicao = advert.Condicao,
                Cidade = advert.Cidade,
                Cep = advert.Cep,
                Cor = advert.Cor,
                DataAtualizacao = advert.DataAtualizacao,
                DataCricao = advert.DataCricao,
                Descricao = advert.Descricao,
                Destaque = advert.Destaque,
                Estado = advert.Estado,
                Id = advert.Id,
                Marca = advert.Marca,
                Placa = advert.Placa,
                Modelo = advert.Modelo,
                Portas = advert.Portas,
                Preco = advert.Preco,
                Tipo = advert.Tipo,
                Quilometragem = advert.Quilometragem,
                Slug = advert.Slug,
                Imagens = advert.Imagens.Select(img => new PhotoDto { Chave = img.Chave, Url = img.Url }).ToList(),
                Usuario = new AdvertUserDto {
                    Id = advert.Usuario.Id,
                    Nome = advert.Usuario.Nome,
                    Email = advert.Usuario.Email,
                    Imagem = advert.Usuario.Imagem,
                    Sobrenome = advert.Usuario.Sobrenome,
                    Telefone = advert.Usuario.Telefone
                },
                Opcionais = advert.Opcionais.Select(opt => new OptionalDto { Name = opt.Nome}).ToList(),
                UsuarioId = advert.UsuarioId
            };

            response.Dados = advertDto;

            return response;
        }

        public async Task<ResponseModel<AdvertDto>> Update(AdvertUpdateDto advertUpdateDto, Guid id) {
            var response = new ResponseModel<AdvertDto>();

            // Busca o anúncio no banco de dados, incluindo relacionamentos
            var advert = await _context.Adverts
                .Include(a => a.Opcionais)
                .Include(a => a.Imagens)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (advert == null) {
                response.Mensagem = "Anuncio nao encontrado";
                return response;
            }

            var optionals = await _context.Optionals
                .Where(opt => advertUpdateDto.Opcionais.Contains(opt.Id))
                .ToListAsync();

            if (optionals.Count != advertUpdateDto.Opcionais.Count) {
                throw new ArgumentException("Um ou mais opcionais fornecidos não são válidos.");
            }

            // Atualiza propriedades básicas do anúncio
            advert.Cor = advertUpdateDto.Cor;
            advert.Cep = advertUpdateDto.Cep;
            advert.Cidade = advertUpdateDto.Cidade;
            advert.Estado = advertUpdateDto.Estado;
            advert.Preco = advertUpdateDto.Preco;
            advert.Portas = advertUpdateDto.Portas;
            advert.Quilometragem = advertUpdateDto.Quilometragem;
            advert.Descricao = advertUpdateDto.Descricao;
            advert.Cambio = advertUpdateDto.Cambio;
            advert.Opcionais = optionals;

            // Remove e adiciona imagens
            _context.Photos.RemoveRange(advert.Imagens);
            advert.Imagens.Clear();
            advert.Imagens = (advertUpdateDto.Imagens
                .Select(img => new Photo(img.Url, img.Chave))).ToList();

            // Tenta salvar as alterações
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                response.Mensagem = "Erro ao atualizar o anúncio. Os dados foram modificados ou removidos por outra operação.";
                return response;
            }

            // Prepara o resultado para retornar ao usuário
            var result = new AdvertDto {
                AnoModelo = advert.AnoModelo,
                Cambio = advert.Cambio,
                Condicao = advert.Condicao,
                Cidade = advert.Cidade,
                Cep = advert.Cep,
                Cor = advert.Cor,
                DataAtualizacao = advert.DataAtualizacao,
                DataCricao = advert.DataCricao,
                Descricao = advert.Descricao,
                Destaque = advert.Destaque,
                Estado = advert.Estado,
                Id = advert.Id,
                Marca = advert.Marca,
                Placa = advert.Placa,
                Modelo = advert.Modelo,
                Portas = advert.Portas,
                Preco = advert.Preco,
                Tipo = advert.Tipo,
                Quilometragem = advert.Quilometragem,
                Slug = advert.Slug,
                Opcionais = advert.Opcionais.Select(opt => new OptionalDto { Name = opt.Nome }).ToList(),
                Imagens = advert.Imagens.Select(img => new PhotoDto { Chave = img.Chave, Url = img.Url }).ToList(),
                Usuario = new AdvertUserDto {
                    Id = advert.Usuario.Id,
                    Nome = advert.Usuario.Nome,
                    Email = advert.Usuario.Email,
                    Imagem = advert.Usuario.Imagem,
                    Sobrenome = advert.Usuario.Sobrenome,
                    Telefone = advert.Usuario.Telefone
                },
                UsuarioId = advert.UsuarioId
            };

            response.Dados = result;
            response.Mensagem = "Anuncio alterado com sucesso";

            return response;
        }


    }
}