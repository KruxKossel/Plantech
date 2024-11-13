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

    public async Task<Usuarios> GetByIdAsync(int id)
    {
        return await _context.Usuarios
            .Include(u => u.Funcionarios)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuarios> GetByUsernameAsync(string username)
    {
        return await _context.Usuarios
            .SingleOrDefaultAsync(u => u.NomeUsuario == username);
    }

    public async Task<IEnumerable<Usuarios>> GetAllAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task AddAsync(Usuarios user)
    {
        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuarios user)
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
    
    public async Task<Usuarios> GetByEmailAsync(string email)
    {
        return await _context.Usuarios.Where(f => f.Email.Contains(email)).SingleOrDefaultAsync(u => u.Email == email);
    }


    public async Task<Usuarios> GetUltimoUsuarioAsync()
    {
        return await _context.Usuarios
            .OrderByDescending(p => p.Id)
            .FirstOrDefaultAsync();
    }

    public async Task MudarStatus(int id)
    {
        var user = await _context.Usuarios.FindAsync(id);
        if(user != null){
                user.Status = user.Status == "ativo" ? "inativo" : "ativo";
                _context.Usuarios.Update(user);
                await _context.SaveChangesAsync();
            }
    }
}