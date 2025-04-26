using Microsoft.EntityFrameworkCore;
using ProjetoClinica.Context;
using ProjetoClinica.Models;
using ProjetoClinica.Services.Interfaces;
using ProjetoClinica.ViewModels;

namespace ProjetoClinica.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly AppDbContext _context;

        public PacienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Paciente> CreatePaciente(PacienteVM novoPaciente)
        {
            if (novoPaciente == null)
                throw new ArgumentNullException(nameof(novoPaciente), "Dados do paciente são obrigatórios.");

            var paciente = new Paciente
            {
                Nome = novoPaciente.Nome,
                Idade = novoPaciente.Idade,
                Cpf = novoPaciente.Cpf
            };

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task DeletePaciente(Guid id)
        {
            Paciente? pacienteASerDeletado = await _context.Pacientes.FindAsync(id);

            if (pacienteASerDeletado == null) throw new Exception("Paciente não encontrado!");

            _context.Pacientes.Remove(pacienteASerDeletado);

            await _context.SaveChangesAsync();
        }

        public async Task<Paciente> GetPacienteById(Guid id)
        {
            Paciente? pacienteRetorno = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == id);

            if (pacienteRetorno == null) throw new Exception("Paciente não encontrado!");

            return pacienteRetorno;
        }

        public async Task<IEnumerable<Paciente>> GetPacientes()
        {
            IEnumerable<Paciente> pacientesRetorno = await _context.Pacientes.ToListAsync();

            return pacientesRetorno;
        }

        public async Task UpdatePaciente(Guid id, PacienteVM pacienteVM)
        {
            Paciente? paciente = await _context.Pacientes.FindAsync(id);

            if (paciente == null) throw new Exception("Paciente não encontrado!");

            paciente.Cpf = pacienteVM.Cpf;
            paciente.Idade = pacienteVM.Idade;
            paciente.Nome = pacienteVM.Nome;

            await _context.SaveChangesAsync();
        }
    }
}
