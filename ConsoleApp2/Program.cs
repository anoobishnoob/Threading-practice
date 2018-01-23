using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp2
{ // this is just some practive. this is not real programming just 
    // I am just practing OOP and threads
    class Program
    {
        static void Main(string[] args)
        {
            BankAcct acct = new BankAcct(10);
            Thread[] threads = new Thread[15];

            Thread.CurrentThread.Name = "main";

            for (int i = 0; i < 15; i++)
            {
                Thread t = new Thread(new ThreadStart
                    (acct.IssueWithdraw));
                t.Name = i.ToString();
                threads[i] = t;
            }

            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine("Thread {0} Alive :" +
                    "{1}",
                    threads[i].Name,
                    threads[i].IsAlive);

                threads[i].Start();

                Console.WriteLine("Thread {0} Alive :" +
                    "{1}",
                    threads[i].Name,
                    threads[i].IsAlive);
            }

            Console.WriteLine("Current Priority :{0}",
                    Thread.CurrentThread.Priority);

            Console.WriteLine("Thread {0} Ending",
                    Thread.CurrentThread.Name);


            Console.ReadLine();
            
            
            
            
            
            
            
            
            
            /*int num = 1;

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(num);
                Thread.Sleep(1000);
                num++;
            }
            Console.Write("Thread ends here");


            Console.ReadLine();*/
            
            
            
            
            /*Thread t = new Thread(Print1); 

            t.Start();

            for (int i = 0; i < 1000; i++) // this is one thread 
            {
                Console.Write(0);
            }

            Console.ReadLine(); ;*/
        }

        /* void Print1()
        {
            for (int i = 0; i < 1000; i++) // this is another thread
            {
                Console.Write(1); // these two will just run back and forth with no real order it looks like
            }
        }*/

    }
}

class BankAcct 
{
    private Object acctLock = new object();
    double Balance { set; get; }

    public BankAcct(double bal)
    {
        Balance = bal;
    }
    public double Withdraw(double amt)
    {
        if ((Balance - amt) < 0)
        {
            Console.WriteLine($"Sorry $ {Balance} in Account");
            return Balance;
        }

        lock (acctLock) // this part protects your data when threading is happening 
        {
            if (Balance >= amt)
            {
                Console.WriteLine("Removed {0} and {1} left in Account",
                    amt, (Balance - amt));
                Balance -= amt;
            }
            return Balance;
        }
    }
        public void IssueWithdraw()
        {
            Withdraw(1);
        }
    


}

