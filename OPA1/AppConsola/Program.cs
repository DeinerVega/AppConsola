using System;
using System.Collections.Generic;

namespace Escalada
{
    class Program
    {
        static void Main(string[] args)
        {
            // Solicitar al usuario el mínimo de calorías y el peso máximo
            Console.Write("Mínimo de calorías: ");
            int minCalorias = int.Parse(Console.ReadLine());

            Console.Write("Peso máximo: ");
            int pesoMaximo = int.Parse(Console.ReadLine());

            // Crear una lista para almacenar los elementos
            List<Elemento> elementos = new List<Elemento>();

            // Usar un bucle while para agregar elementos a la lista
            bool agregarElemento = true;
            while (agregarElemento)
            {
                Console.Write("Nombre del elemento: ");
                string nombre = Console.ReadLine();

                Console.Write("Peso del elemento: ");
                int peso = int.Parse(Console.ReadLine());

                Console.Write("Calorías del elemento: ");
                int calorias = int.Parse(Console.ReadLine());

                // Agregar el elemento a la lista
                elementos.Add(new Elemento(nombre, peso, calorias));

                // Preguntar al usuario si desea agregar otro elemento
                Console.Write("¿Desea agregar otro elemento? (S/N): ");
                string respuesta = Console.ReadLine().ToUpper();
                agregarElemento = (respuesta == "S");
            }

            // Encontrar el conjunto de elementos óptimos
            List<Elemento> conjuntoOptimo = EncontrarConjuntoOptimo(elementos, minCalorias, pesoMaximo);

            // Imprimir los elementos óptimos
            Console.WriteLine("Elementos viables para escalar el risco:");
            foreach (Elemento elemento in conjuntoOptimo)
            {
                Console.WriteLine($"{elemento.Nombre} - Peso: {elemento.Peso}, Calorías: {elemento.Calorias}");
            }
        }

        // Función para encontrar el conjunto de elementos óptimos
        static List<Elemento> EncontrarConjuntoOptimo(List<Elemento> elementos, int minCalorias, int pesoMaximo)
        {
            int n = elementos.Count;
            int mejorValor = 0;
            List<Elemento> mejorConjunto = new List<Elemento>();

            // Generar todas las combinaciones posibles de elementos usando un enfoque de fuerza bruta
            for (int i = 1; i < (1 << n); i++)
            {
                List<Elemento> combinacion = new List<Elemento>();
                int pesoTotal = 0;
                int caloriasTotal = 0;

                for (int j = 0; j < n; j++)
                {
                    if ((i & (1 << j)) > 0)
                    {
                        combinacion.Add(elementos[j]);
                        pesoTotal += elementos[j].Peso;
                        caloriasTotal += elementos[j].Calorias;
                    }
                }

                // Verificar si la combinación es válida y mejora la mejor combinación anterior
                if (caloriasTotal >= minCalorias && pesoTotal <= pesoMaximo && caloriasTotal > mejorValor)
                {
                    mejorConjunto = new List<Elemento>(combinacion);
                    mejorValor = caloriasTotal;
                }
            }

            return mejorConjunto;
        }
    }

    // Clase para representar un elemento
    class Elemento
    {
        public string Nombre { get; }
        public int Peso { get; }
        public int Calorias { get; }

        public Elemento(string nombre, int peso, int calorias)
        {
            Nombre = nombre;
            Peso = peso;
            Calorias = calorias;
        }
    }
}
