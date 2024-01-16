using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafraEmprestimo.Domain._Base
{
    public abstract class Entidade
    {
        public int Id { get; protected set; }
    }
}
