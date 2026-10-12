using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAgenda
{
    internal class Contato
    {
        private string email;
        private string nome;
        private Data dtNasc;
        private List<Telefone> telefones;

        public Contato(string email, string nome, Data dtNasc)
        {
            this.email = email;
            this.nome = nome;
            this.dtNasc = dtNasc;
            this.telefones = new List<Telefone>();
        }

        public string getEmail()
        {
            return email;
        }

        public void setEmail(string email)
        {
            this.email = email;
        }

        public string getNome()
        {
            return nome;
        } 

        public void setNome(string nome)
        {
            this.nome = nome;
        }   

        public Data getdtNasc()
        {
             return dtNasc;
        }

        public void setdtNasc(Data dtNasc)
        {
            this.dtNasc = dtNasc;
        }

        public void addTelefone(Telefone t)
        {
            if (t == null)
            {
                return;
            }

            if (t.getPrincipal())
            {
                foreach (Telefone telefone in telefones)
                {
                    telefone.setPrincipal(false);
                }
            }

            telefones.Add(t);
        }

        public string getTelefonePrincipal()
        {
            foreach (Telefone telefone in telefones)
            {
                if (telefone.getPrincipal())
                {
                    return telefone.getNumero();
                }
            }
            return "não informado";
        }

        public int getIdade()
        {
            DateTime hoje = DateTime.Today;
            int idade = hoje.Year - dtNasc.getAno();

            if (hoje.Month < dtNasc.getMes() || (hoje.Month == dtNasc.getMes() && hoje.Day < dtNasc.getDia()))
            {
                idade--;
            }
            return idade;
        }

        public override string ToString()
        {
            string dados = $"Nome: {nome}\n";
            dados += $"Email: {email}\n";
            dados += $"Nascimento: {dtNasc}\n";
            dados += $"Idade: {getIdade()} anos\n";
            dados += $"Telefone principal: {getTelefonePrincipal()}\n";
            dados += "Telefones:\n";

            foreach (Telefone telefone in telefones)
            {
                dados += " - " + telefone + "\n";
            }

            return dados;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Contato outro)
                return false;

            return string.Equals(
                email,
                outro.email,
                StringComparison.OrdinalIgnoreCase
            );
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(email ?? "");
        }
    }
}

