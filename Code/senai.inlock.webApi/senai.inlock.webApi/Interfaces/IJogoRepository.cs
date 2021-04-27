using senai.inlock.webApi_.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi_.Interfaces
{
    interface IJogoRepository
    { 
        void Create(JogoDomain novoJogo);
        List<JogoDomain> Read();
        void Update(JogoDomain jogoAtualizado, int id);
        void Delete(int id);
        JogoDomain ReadById(int id);

    }
}
