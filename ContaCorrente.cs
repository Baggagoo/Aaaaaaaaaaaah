using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaaaaaaaaaaah
{
    internal class ContaCorrente : Conta
    {
        public double Limite { get; set; }

        public double CalcularTributo()
        {
            return Saldo * 0.015;
        }

        public void Sacar(double valor)
        {
            if(Saldo + Limite >= valor)
            {
                Saldo -= valor;
            }
        }

        public void Depositar(double valor)
        {
            Saldo = Saldo + valor; 
        }
    }
}
