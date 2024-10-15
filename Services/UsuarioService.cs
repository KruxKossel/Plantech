using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plantech.Interfaces;
using Plantech.Models;
using Plantech.DTOs;
using System.Text;
using System.Security.Cryptography;

namespace Plantech.Services
{
    public class UsuarioService(IUsuarioRepository usuarioRepository, IFuncionarioRepository funcionarioRepository) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IFuncionarioRepository _funcionarioRepository = funcionarioRepository;

        public async Task<UsuarioDTO> AuthenticateAsync(string username, string password)
        {
            var user = await _usuarioRepository.GetByUsernameAsync(username);
            if (user == null || !VerifyPasswordHash(password, user.Senha, user.Salt))
                return null;

            return new UsuarioDTO { Id = user.Id, NomeUsuario = user.NomeUsuario, Email = user.Email, Status = user.Status };
        }

        public async Task<UsuarioDTO> GetByIdAsync(int id)
        {
            var user = await _usuarioRepository.GetByIdAsync(id);
            return new UsuarioDTO { Id = user.Id, NomeUsuario = user.NomeUsuario, Email = user.Email, Status = user.Status };
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var users = await _usuarioRepository.GetAllAsync();
            return users.Select(user => new UsuarioDTO { Id = user.Id, NomeUsuario = user.NomeUsuario, Email = user.Email, Status = user.Status });
        }

        public async Task CreateAsync(UsuarioDTO userDto)
        {
            var user = new Usuario
            {
                NomeUsuario = userDto.NomeUsuario,
                Email = userDto.Email,
                Senha = HashPassword(userDto.Senha, out var salt),
                Salt = salt,
                Status = userDto.Status
            };

            await _usuarioRepository.AddAsync(user);
        }

        public async Task UpdateAsync(UsuarioDTO userDto)
        {
            var user = await _usuarioRepository.GetByIdAsync(userDto.Id) ?? throw new KeyNotFoundException("Usuário não encontrado");
            user.NomeUsuario = userDto.NomeUsuario;
            user.Email = userDto.Email;
            user.Status = userDto.Status;

            await _usuarioRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<Cargo> GetCargoByUserIdAsync(int userId)
        {
            var funcionario = await _funcionarioRepository.GetByUserIdAsync(userId);
            return funcionario?.Cargo;
        }

        private static string HashPassword(string password, out string salt)
        {
            // Gerando salt com RandomNumberGenerator
            var saltBytes = new byte[16];
            RandomNumberGenerator.Fill(saltBytes);
            salt = Convert.ToBase64String(saltBytes);

            // Concatenando senha e salt
            var saltedPassword = password + salt;
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(saltedPassword));

            // Retornando o hash da senha
            return Convert.ToBase64String(hashBytes);
        }


        // Método para verificar se a senha fornecida corresponde ao hash armazenado
        private static bool VerifyPasswordHash(string password, string hashedPassword, string salt)
        {
            // Concatenando a senha fornecida com o sal armazenado
            var saltedPassword = password + salt;

            // Gerando o hash da senha concatenada com o sal usando SHA256
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(saltedPassword));

            // Convertendo o hash gerado para uma string Base64
            var computedHash = Convert.ToBase64String(hashBytes);

            // Comparando o hash gerado com o hash armazenado
            // Retorna verdadeiro se forem iguais, falso caso contrário
            
            return computedHash == hashedPassword;
        }

    }
}
