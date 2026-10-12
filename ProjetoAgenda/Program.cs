
using System;
using System.Globalization;

namespace ProjetoAgenda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Contatos agenda = new Contatos();
            int opcao;

            do
            {
                Console.Clear();

                Console.WriteLine("===== AGENDA DE CONTATOS =====");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("1 - Adicionar contato");
                Console.WriteLine("2 - Pesquisar contato");
                Console.WriteLine("3 - Alterar contato");
                Console.WriteLine("4 - Remover contato");
                Console.WriteLine("5 - Listar contatos");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    opcao = -1;
                }

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando a agenda...");
                        break;

                    case 1:
                        adicionarContato(agenda);
                        break;

                    case 2:
                        pesquisarContato(agenda);
                        break;

                    case 3:
                        alterarContato(agenda);
                        break;

                    case 4:
                        removerContato(agenda);
                        break;

                    case 5:
                        listarContatos(agenda);
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione ENTER para continuar...");
                    Console.ReadLine();
                }

            } while (opcao != 0);
        }

        // ADICIONAR CONTATO
        static void adicionarContato(Contatos agenda)
        {
            Console.Clear();
            Console.WriteLine("===== ADICIONAR CONTATO =====");

            Console.Write("Nome: ");
            string nome = (Console.ReadLine() ?? "").Trim();

            Console.Write("E-mail: ");
            string email = (Console.ReadLine() ?? "").Trim();

            if (nome == "" || email == "")
            {
                Console.WriteLine("Nome e e-mail são obrigatórios!");
                return;
            }

            // Verifica se o e-mail já existe
            Contato busca = new Contato(
                email, "", new Data(1, 1, 2000));

            if (agenda.pesquisar(busca) != null)
            {
                Console.WriteLine("Já existe um contato com esse e-mail!");
                return;
            }

            Data nascimento;

            if (!lerData(out nascimento))
            {
                Console.WriteLine("Data inválida!");
                return;
            }

            Contato contato = new Contato(email, nome, nascimento);

            string resposta;

            do
            {
                Console.Write("\nDeseja adicionar um telefone? (s/n): ");
                resposta = (Console.ReadLine() ?? "").Trim().ToLower();

                if (resposta == "s")
                {
                    Console.Write("Tipo (celular, residencial etc.): ");
                    string tipo = Console.ReadLine() ?? "";

                    Console.Write("Número: ");
                    string numero = Console.ReadLine() ?? "";

                    Console.Write("É o telefone principal? (s/n): ");
                    string principalTexto =
                        (Console.ReadLine() ?? "").Trim().ToLower();

                    bool principal = principalTexto == "s";

                    Telefone telefone =
                        new Telefone(tipo, numero, principal);

                    contato.addTelefone(telefone);
                }

            } while (resposta == "s");

            if (agenda.addContato(contato))
                Console.WriteLine("\nContato cadastrado com sucesso!");
            else
                Console.WriteLine("\nNão foi possível cadastrar o contato.");
        }

        // PESQUISAR CONTATO
        static void pesquisarContato(Contatos agenda)
        {
            Console.Clear();
            Console.WriteLine("===== PESQUISAR CONTATO =====");

            Console.Write("Digite o e-mail: ");
            string email = (Console.ReadLine() ?? "").Trim();

            Contato busca = new Contato(
                email, "", new Data(1, 1, 2000));

            Contato encontrado = agenda.pesquisar(busca);

            if (encontrado != null)
            {
                Console.WriteLine("\n===== CONTATO ENCONTRADO =====");
                Console.WriteLine(encontrado.ToString());
            }
            else
            {
                Console.WriteLine("\nContato não encontrado!");
            }
        }

        // ALTERAR CONTATO
        static void alterarContato(Contatos agenda)
        {
            Console.Clear();
            Console.WriteLine("===== ALTERAR CONTATO =====");

            Console.Write("Digite o e-mail do contato: ");
            string email = (Console.ReadLine() ?? "").Trim();

            Contato busca = new Contato(
                email, "", new Data(1, 1, 2000));

            Contato encontrado = agenda.pesquisar(busca);

            if (encontrado == null)
            {
                Console.WriteLine("\nContato não encontrado!");
                return;
            }

            Console.WriteLine("\nContato atual:");
            Console.WriteLine(encontrado);

            Console.Write("\nNovo nome: ");
            string nome = (Console.ReadLine() ?? "").Trim();

            if (nome == "")
            {
                Console.WriteLine("O nome não pode ficar vazio!");
                return;
            }

            Data nascimento;

            if (!lerData(out nascimento))
            {
                Console.WriteLine("Data inválida! Alteração cancelada.");
                return;
            }

            encontrado.setNome(nome);
            encontrado.setdtNasc(nascimento);

            if (agenda.alterar(encontrado))
                Console.WriteLine("\nContato alterado com sucesso!");
            else
                Console.WriteLine("\nNão foi possível alterar o contato.");
        }

        // REMOVER CONTATO
        static void removerContato(Contatos agenda)
        {
            Console.Clear();
            Console.WriteLine("===== REMOVER CONTATO =====");

            Console.Write("Digite o e-mail do contato: ");
            string email = (Console.ReadLine() ?? "").Trim();

            Contato busca = new Contato(
                email, "", new Data(1, 1, 2000));

            Contato encontrado = agenda.pesquisar(busca);

            if (encontrado == null)
            {
                Console.WriteLine("\nContato não encontrado!");
                return;
            }

            Console.WriteLine("\nContato selecionado:");
            Console.WriteLine(encontrado);

            Console.Write("Confirma a remoção? (s/n): ");
            string resposta =
                (Console.ReadLine() ?? "").Trim().ToLower();

            if (resposta == "s")
            {
                if (agenda.remover(encontrado))
                    Console.WriteLine("\nContato removido com sucesso!");
                else
                    Console.WriteLine("\nNão foi possível remover o contato.");
            }
            else
            {
                Console.WriteLine("\nRemoção cancelada.");
            }
        }

        // LISTAR CONTATOS
        static void listarContatos(Contatos agenda)
        {
            Console.Clear();
            Console.WriteLine("===== LISTA DE CONTATOS =====");

            var contatos = agenda.getAgenda();

            if (contatos.Count == 0)
            {
                Console.WriteLine("\nA agenda está vazia!");
                return;
            }

            int contador = 1;

            foreach (Contato contato in contatos)
            {
                Console.WriteLine($"\n--- CONTATO {contador} ---");
                Console.WriteLine(contato.ToString());
                contador++;
            }

            Console.WriteLine($"Total de contatos: {contatos.Count}");
        }

        // LER E VALIDAR DATA DE NASCIMENTO
        static bool lerData(out Data nascimento)
        {
            nascimento = null;

            Console.Write("Data de nascimento (dd/mm/aaaa): ");
            string texto = Console.ReadLine() ?? "";

            DateTime data;

            bool valida = DateTime.TryParseExact(
                texto,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out data
            );

            if (!valida || data.Date > DateTime.Today)
                return false;

            nascimento = new Data(data.Day, data.Month, data.Year);
            return true;
        }
    }
}
