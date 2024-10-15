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
            using (var rng = new RNGCryptoServiceProvider())
            {
                var saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                salt = Convert.ToBase64String(saltBytes);
            }

            var saltedPassword = password + salt;
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(saltedPassword));
            return Convert.ToBase64String(hashBytes);
        }

        private static bool VerifyPasswordHash(string password, string hashedPassword, string salt)
        {
            var saltedPassword = password + salt;
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(saltedPassword));
            var computedHash = Convert.ToBase64String(hashBytes);
            return computedHash == hashedPassword;
        }
    }
}
