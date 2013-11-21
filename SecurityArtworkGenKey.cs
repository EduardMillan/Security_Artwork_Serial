using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

/**
 * Reto reversing Security Artwork
 * http://www.securityartwork.es/2013/11/18/reto-de-reversing/
 * GenKey
 * Eduard Millán Forn
 * http://codementia.blogspot.com.es/
**/

/**
 * Generador de serials aleatorios basado en la tabla de valores válidos obtenida con SecurityArtWorkSerial
 * Posición 0: 0 1 2 3 4 5 6 7 8 9
 * Posición 1: 4
 * Posición 2: 3 8
 * Posición 3: 0
 * Posición 4: 4 9
 * Posición 5: 0 2 4 6 8
 * Posición 6: 0 5
 * Posición 7: 5
 * Posición 8: 2 7
 * Posición 9: 7
 * Posición 10: 0 1 2 3 4 5 6 7 8 9
 * Posición 11: 5
 * Posición 12: 3 8
 * Posición 13: 3
 * Posición 14: 3 8
 * Posición 15: 1 3 5 7 9
 * Si se eliminan las filas iguales y las que solo contienes un valor quedan 7 filas (array digits[]):
 * Posición 0: 0 1 2 3 4 5 6 7 8 9
 * Posición 1: 3 8
 * Posición 2: 4 9
 * Posición 3: 0 2 4 6 8
 * Posición 4: 0 5
 * Posición 5: 2 7
 * Posición 6: 1 3 5 7 9
 * La fila que se precisa para calcular cada dígito se indica en su´posición el lista[] (16 entradas = 16 digitos del serial).
 * Para indicar valores fijos se indica su valor + 0xF0.
**/

namespace SecurityArtWorkSerialGenKey
{
    class Program
    {
        static void Main(string[] args)
        {
            string serial = "";
            Int16[] lista = new Int16[16] { 0, 0xF4, 1, 0xF0, 2, 3, 4, 0xF5, 5, 0xF7, 0, 0xF5, 1, 0xF3, 1, 6 };
            Int16[][] digits = new Int16[][] {
                new Int16[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 },
                new Int16[] { 3, 8 },
                new Int16[] { 4, 9 },
                new Int16[] { 0, 2, 4, 6, 8},
                new Int16[] { 0, 5 },
                new Int16[] { 2, 7 },
                new Int16[] { 1, 3, 5, 7, 9 }
            };

            //  Si el indice actual contiene un valor >= 0x>F0 se resta 0xF y se toma el resultado obtenido
            //  Sino se toma un valor aleatorio dentro de la lista de digitos indexada por 'lista[n]'
            for (Int16 i = 0; i < lista.Length; i++)
                serial += ((lista[i] >= 0xF0) ? lista[i] & 0x0F : Digit(digits[lista[i]])).ToString();
            //  Se muestra el serial calculado
            Console.WriteLine(serial);
            //  Y se lanza serial.exe para comprobarlo
            Process p = new Process();
            p.StartInfo.FileName = "serial.exe";
            p.StartInfo.Arguments = serial;
            p.StartInfo.UseShellExecute = false;
            p.Start();
            p.WaitForExit();
        }

        static Int16 Digit(Int16[] digits)
        {
            Random rnd = new Random();

            return digits[rnd.Next(0, digits.Length)];
        }
    }
}
