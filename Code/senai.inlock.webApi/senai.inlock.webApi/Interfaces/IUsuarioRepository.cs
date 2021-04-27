using senai.inlock.webApi_.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Interfaces
{
    interface IUsuarioRepository
    {
        void Create(UsuarioDomain novoUsuario);
        List<UsuarioDomain> Read();
        void Update(UsuarioDomain usuarioAtualizado, int id);
        void Delete(int id);
        UsuarioDomain ReadById(int id);
        UsuarioDomain Login(string email, string id);
    }
}
