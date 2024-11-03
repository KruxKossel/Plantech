using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plantech.Interfaces;
using Plantech.Models;
using Plantech.Data;

public class UsuarioRepository(PlantechContext context) : IUsuarioRepository
{
    private readonly PlantechContext _context = context;

    public async Task<Usuario> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario> GetByUsernameAsync(string username)
    {
        return await _context.Usuarios
            .SingleOrDefaultAsync(u => u.NomeUsuario == username);
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task AddAsync(Usuario user)
    {
        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario user)
    {
        _context.Usuarios.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Usuarios.FindAsync(id);
        if (user != null)
        {
            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Usuario> GetByEmailAsync(string email)
    {
        return await _context.Usuarios.Where(f => f.Email.Contains(email)).FirstOrDefaultAsync();
    }
}
