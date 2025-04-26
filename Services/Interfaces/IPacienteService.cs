using Microsoft.AspNetCore.Mvc;
using ProjetoClinica.Models;
using ProjetoClinica.ViewModels;

namespace ProjetoClinica.Services.Interfaces
{
    public interface IPacienteService
    {
        Task<IEnumerable<Paciente>> GetPacientes();

        Task<Paciente> GetPacienteById(Guid id);

        Task<Paciente> CreatePaciente(PacienteVM novoPaciente);

        Task DeletePaciente(Guid id);

        Task UpdatePaciente(Guid id, PacienteVM pacienteVM);
    }
}
