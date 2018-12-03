using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen_Segundo_Parcial
{

    public class Nodo
    {
        public int fila;
        public int columna;
        /// <summary>
        /// tipo de Vertic, 'I'->Punto de inicio(partida), 'S'->Punto final(Salida), 'P'->Pared,  'X'->CAMINO VALIDO, UN ESPACIO YA ES LA RUTA ENCONTRADA
        /// </summary>
        public char tipo; 
        /// <summary>
        /// Indica si el nodo ya ha sido visitado
        /// </summary>
        public bool visitado; 
        /// <summary>
        /// Nodo anterior por el cual se debe seguir a la siguiente ruta
        /// </summary>
        public Nodo anterior;
    }

    public class BFS
    {
        const int max = 100;
        /// <summary>
        /// El atributo matriz representa el laberinto  por medio de Vertices que en realidad le corresponden a la clase Nodo
        /// </summary>
        Nodo[,] matriz = null;
        /// <summary>
        /// Matriz de distancias
        /// </summary>
        int[,] distancia;
        /// <summary>
        /// cola donde se atenderá a cada uno de los nodos del laberinto
        /// </summary>
        Queue<Nodo> cola;
        /// <summary>
        /// se asume que los nodos adyacentes a un vertice son 4, y estan dados por el que esta arriba(Norte), abajo(Sur), este(Derecha), oeste(izquieda)
        /// <b>dx</b> es la variacion en la columna
        /// </summary>        
        int[] dx = { -1, 0, 1, 0 };
        /// <summary>
        /// dy es la variacion en la fila para los vertices adyacentes, recuerde que ambos, tanto dy y dx combinados en la misma posicion forman el vertice adyacente aumentandole el valor
        /// almacenado en el vector
        /// </summary>
        int[] dy = { 0, -1, 0, 1 };
        /// <summary>
        /// inicio es el vertice de partida
        /// fin es el vertice objetivo o salida
        /// </summary>
        Nodo inicio, fin;
        /// <summary>
        /// n y m son el tamaño de filas y columnas de la matriz
        /// </summary>
        int n, m;
        /// <summary>
        /// Constructor 
        /// </summary>
        public BFS()
        {
            matriz = new Nodo[max, max];
            distancia = new int[max, max];
            cola = new Queue<Nodo>();
        }

        /// <summary>
        /// la lectora del grafo se hace por consola...  se podria reacomodar esta función a que sea a travez de una interfaz
        /// </summary>
        public void leerGrafo()
        {
            Console.WriteLine("Introduzca el tamaño del grafo");
            n = int.Parse(Console.ReadLine());
            m = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduzca el laberinto representado por cadenas");
            ///recorremos las filas
            ///iniciamos en el indice 1, dejando el indice 0 para hacer consultas en las adyacencias
            for (int i = 1; i <= n; i++)
            {
                //leemos la linea
                string linea = Console.ReadLine();
                //recorremos cada carecter de la cadena leida
                for (int j = 1; j <= linea.Length; j++)
                {
                    Nodo nodo = new Nodo();
                    nodo.tipo = linea[j - 1];
                    nodo.fila = i;
                    nodo.columna = j;
                    nodo.visitado = false;

                    matriz[i, j] = nodo;

                    if (nodo.tipo == 'I')
                        inicio = nodo;
                    if (nodo.tipo == 'S')
                        fin = nodo;
                }
            }

        }

        public void imprimirRecorrido()
        {
            Console.WriteLine("Nodos a recorrer");
            Nodo actual = fin;
            while (true)
            {
                Console.WriteLine("["+actual.fila + ", " + actual.columna + "] - Distancia = " + distancia[actual.fila, actual.columna].ToString());
                //if(conDistancias)
                //    matriz[actual.fila, actual.columna].tipo =   distancia[actual.fila, actual.columna].ToString()[0];
                //else
                matriz[actual.fila, actual.columna].tipo = ' ';
                actual = actual.anterior;
                if (actual == null)
                    break;
            }

            for(int i = 1; i<=n; i++)
            {
                for(int j = 1; j<=m; j++)
                {
                    Console.Write(matriz[i, j].tipo);
                }
                Console.WriteLine();
            }
        }

        public void recorrido()
        {
            //Array.Clear(matriz, 0, matriz.Length);
            //Array.Clear(distancia, 0, distancia.Length);

            //iniciamos la distnaica del nodo inicio en cero
            distancia[inicio.fila, inicio.columna] = 0;

            //marcamos el nodo como visitado
            matriz[inicio.fila, inicio.columna].visitado = true;

            //ingresamo el nodo en la cola
            cola.Enqueue(inicio);

            //mientras que la cola no este vacia
            while (cola.Count != 0)
            {
                //sacamos un nodo de la cola (el primero)
                Nodo nodoActual = cola.Dequeue();
                //recorremos los nodos adyacentes al nodoActualS
                //se recorren 4 posibles nodos adycentes, arriba, abajo, derecha e izquierda
                for (int i = 0; i < 4; i++)
                {
                    //dy y dx nos ayudan a solamente sumar +1,-1 o 0 tanto en fila o columna para los cuatro vertices adycentes
                    Nodo nodoAdy = matriz[nodoActual.fila + dy[i], nodoActual.columna + dx[i]];
                    // si el nodo no es null , no ha sido visitado... si es un camino valido o es la salida
                    if (nodoAdy != null && !nodoAdy.visitado && (nodoAdy.tipo == 'X' || nodoAdy.tipo == 'S'))
                    {
                        //marcamos como visitado
                        nodoAdy.visitado = true;
                        //incrementamos su distancia
                        distancia[nodoAdy.fila, nodoAdy.columna] = distancia[nodoActual.fila, nodoActual.columna] + 1;
                        //guardamos la referencia a su nodo anterior para trazar la ruta
                        nodoAdy.anterior = nodoActual;
                        //ingresamo el nuevo nodo descubierto a la cola
                        cola.Enqueue(nodoAdy);

                        //si hemos llegado a la salida o al objetivo... no terminamos de recorrer TODO el grafo, y la paramos ahi
                        if (nodoAdy.tipo == 'S')
                        {
                            
                            break;
                        }

                        

                       
                    }

                }


            }


        }

    }

    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());


            BFS grafo = new BFS();
            grafo.leerGrafo();
            grafo.recorrido();            
            grafo.imprimirRecorrido();
            Console.ReadKey();
        }
    }
}
