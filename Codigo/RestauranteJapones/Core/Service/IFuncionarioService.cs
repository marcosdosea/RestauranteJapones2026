using Core.DTO;

namespace Core.Service
{
    public interface IFuncionarioService
    {
        uint Create(Funcionario funcionario);   
        void Edit(Funcionario funcionario);  
        void Delete(uint id);
        Funcionario Get(uint id);
        IEnumerable<FuncionarioDTO> GetAll();
    }
}
