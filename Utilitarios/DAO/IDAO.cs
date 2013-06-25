using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios.DAO
{
    public interface IDAO<T>
    {
        ISet<T> Listar();
        T BuscarPeloId(Object id);
        ulong Incluir(T obj);
        void Alterar(T obj);
        void Excluir(T obj);
    }
}
