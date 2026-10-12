using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAgenda
{
    internal class Contatos
    {
        private List<Contato> agenda;

        public Contatos()
        {
            agenda = new List<Contato>();
        }

        public IReadOnlyList<Contato> getAgenda()
        {
            return agenda.AsReadOnly();
        }

        public bool addContato(Contato c)
        {
            if (c == null || agenda.Contains(c))
            {
                return false;
            }


            agenda.Add(c);
            return true;
        }

        public Contato pesquisar(Contato c)
        {
            if (c == null)
            {
                return null;
            }

            foreach (Contato contato in agenda)
            {
                if (contato.Equals(c))
                {
                    return contato;
                }
            }
            return null;
        }

        public bool alterar(Contato c)
        {
            if (c == null)
            {
                return false;
            }

            for (int i = 0; i <agenda.Count; i++)
            {
                if (agenda[i].Equals(c))
                {
                    agenda[i] = c;
                    return true;
                }
            }
            return false;
        }

        public bool remover(Contato c)
        {
            Contato encontrado = pesquisar(c);

            if (encontrado != null)
            {
                agenda.Remove(encontrado);
                return true;
            }
            return false;
        }

    }


}
