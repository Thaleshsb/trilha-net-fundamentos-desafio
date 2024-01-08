using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
internal class DataTime
    {
        public static DataTime Now { get; internal set; }
    }
namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private readonly decimal precoInicial = 0;
        private readonly decimal precoPorHora = 0;
        private readonly List<string> veiculos = new();
        private readonly List<DateTime> horarios = new();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
            veiculos =  new List<string>();
            horarios =  new List<DateTime>();

        }
        public void AdicionarVeiculo(){

            //regular expresão que verifica o padrão mercosul, sem se preocupar com tipo de letra

            string modelo = @"^(?i)[A-Z]{3}\d[A-Z]\d{2}$";
            static bool IsValidPlaca(string placa, string modelo){
                return Regex.IsMatch(placa, modelo);
            }
            
            Console.WriteLine("Digite a placa do veículo no padrão Mercosul 2023 AAA0A00 para estacionar: \n");
            string placa = Console.ReadLine().ToUpper();

            //verificação se tá no modelo e se já foi cadastrado

            while (!IsValidPlaca(placa, modelo) || veiculos.Contains(placa)){
                Console.WriteLine();
                Console.WriteLine("Placa inválida ou Já cadastrada Digite novamente: ");
                placa = Console.ReadLine().ToUpper();
            }
            // caso passe em tudo, efetiva cadastro
            veiculos.Add(placa);
            horarios.Add(DateTime.Now);
            Console.WriteLine();
            Console.WriteLine("Placa válida! Pode estacionar meu querido.\n");
        }  
        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:\n");
            string placa = Console.ReadLine().ToUpper();

            // Verifica se o veículo existe

            if (veiculos.Any(x => x == placa.ToUpper())){   
                
                decimal valorTotal = 0;
                Console.WriteLine();
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:\n");               
                string input = Console.ReadLine();
                Console.WriteLine();

                // Se o input for númerico procede com a saida
                if(int.TryParse(input, out int horas)){
                    valorTotal = precoInicial + (horas * precoPorHora);
                    veiculos.Remove(placa);
                    Console.WriteLine($"O veículo {placa.ToUpper()} foi removido e o preço total foi de: {valorTotal:C}\n");
                }
                else{
                    Console.WriteLine("Hora incerida não numérica\n");
                }
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente\n");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:\n");
                for(int i=0; i < veiculos.Count ;i++){
                    Console.WriteLine($"ID:{i+1} Placa: {veiculos[i]} Entrada: {horarios[i]}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
