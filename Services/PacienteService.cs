using Microsoft.EntityFrameworkCore;
using ProjetoClinica.Context;
using ProjetoClinica.Models;
using ProjetoClinica.Repositories;
using ProjetoClinica.Services.Interfaces;
using ProjetoClinica.ViewModels;

namespace ProjetoClinica.Services
{
    public class PacienteService : IPacienteService
    {
        //private readonly AppDbContext _context;
        private readonly IRepository<Paciente> _repository; 

        public PacienteService(IRepository<Paciente> repository)
        {
            _repository = repository;
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

            await _repository.AddAsync(paciente);
            await _repository.SaveChangesAsync();
            return paciente;
        }

        public async Task DeletePaciente(Guid id)
        {
            Paciente? pacienteASerDeletado = await _repository.GetByIdAsync(id);

            if (pacienteASerDeletado == null) throw new Exception("Paciente não encontrado!");

            _repository.Delete(pacienteASerDeletado);

            await _repository.SaveChangesAsync();
        }

        public async Task<Paciente> GetPacienteById(Guid id)
        {
            Paciente? pacienteRetorno = await _repository.GetByIdAsync(id);

            if (pacienteRetorno == null) throw new Exception("Paciente não encontrado!");

            return pacienteRetorno;
        }

        public async Task<IEnumerable<Paciente>> GetPacientes()
        {
            IEnumerable<Paciente> pacientesRetorno = await _repository.GetAllAsync();

            return pacientesRetorno;
        }

        public async Task UpdatePaciente(Guid id, PacienteVM pacienteVM)
        {
            Paciente? paciente = await _repository.GetByIdAsync(id);

            if (paciente == null) throw new Exception("Paciente não encontrado!");

            paciente.Cpf = pacienteVM.Cpf;
            paciente.Idade = pacienteVM.Idade;
            paciente.Nome = pacienteVM.Nome;

            await _repository.SaveChangesAsync();
        }
    }
}
