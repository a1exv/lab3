using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bancomat
{
    namespace NAccount
    {
        class Account
        {
            int number;
            int password;
            int sum;
            static Random r = new Random();
            public Account()
            {
                
                
                number=r.Next(1000, 9999);
                password=r.Next(100, 999);
                sum=0;
            }
            public void ToString()
            {
                Console.WriteLine("number is {0}, password is {1}, sum is {2}", number, password, sum);
            }
            public int Sum
            {
                get
                {
                    return sum;
                }
                set
                {
                    if (value > 0) sum = value;
                }
            }
            public int Password
            {
                get
                {
                    return password;
                }
            }
            public int Number
            {
                get
                {
                    return number;
                }
            }
        }
        }
    namespace NClient
    {
        using  NAccount;
        class Client
        {
            string firstName;
            string lastName;
            Account account;
            public Client(string _firstName, string _lastName)
            {
                firstName = _firstName;
                lastName = _lastName;
                account = new Account();
            }
            public void ToString()
            {
                Console.WriteLine("First name is {0}, Last name is {1}, account is:", firstName, lastName );
                account.ToString();
            }
            public string FirstName
            {
                get
                {
                    return firstName;
                }
            }
            public string LastName
            {
                get
                {
                    return lastName;
                }
            }
            public Account GetAccount
            {
                get{
                    return account;
                }
            }
        }
    }
    namespace NBank
    {
        using NClient;
        class Bank
        {
            string name;
            Client[] clientArray;
            public Bank(int i, string _name)
            {
                name = _name;
                clientArray = new Client[i];
                Console.WriteLine("Создан банк {0}, максимально обслуживающий {1} клиентов", name, i);
            }
            public string Name
            {
                get
                {
                    return name;
                }
            }
            public Client this[int i]
            {
                get
                {
                    if ((i < 0) || (i > clientArray.Length))
                        throw new System.IndexOutOfRangeException();
                    return (Client)clientArray[i];
                }
                set
                {
                    if ((i < 0) || (i > clientArray.Length))
                        throw new System.IndexOutOfRangeException();
                    clientArray[i] = (Client)value;
                }
            }
            public void ToString()
            {
                Console.WriteLine("Bank name is {0}", name);
                int max = clientArray.Length;
                for (int i = 0; i < max; i++)
                {
                    Console.WriteLine("{0} client is: ", i + 1);
                    if (clientArray[i] != null)
                        clientArray[i].ToString();
                    else Console.WriteLine("does not exist");
                }
            }
            public int Lenght()
            {
                return clientArray.Length;
            }

        }
    }
    
    class Program
    {
        
        static void Main(string[] args)
        {
            
            NBank.Bank bank = new NBank.Bank(10, "BNP");
            try
            {
                bank[0] = new NClient.Client("Иван", "Иванов");
                bank[1] = new NClient.Client("Петр", "Петров");
                bank[2] = new NClient.Client("Сидор", "Сидоров");
                bank[3] = new NClient.Client("Николай", "Николаев");
                bank.ToString();
                Console.WriteLine("press any bottom to enter in bacomat");
                Console.ReadLine();
                do
                {
                    Console.Clear();
                    Console.WriteLine("Enter number of account");

                    int accnum = 0;
                    accnum = Convert.ToInt32(Console.ReadLine());
                    int i = 0;
                    bool find = false;
                    while ((bank[i] != null) && (i < bank.Lenght()))
                    {
                        if (bank[i].GetAccount.Number == accnum)
                        {
                            find = true;
                            break;
                        }
                        i++;
                    }
                    if (find == true)
                    {
                        
                        int possabilities = 3;
                        do{
                            Console.Clear();
                        Console.WriteLine("Welcome, {0} {1}! enter your password: ", bank[i].FirstName, bank[i].LastName);
                        int pwd = int.Parse(Console.ReadLine());
                        if (pwd == bank[i].GetAccount.Password)
                        {
                            Console.Clear();
                            Console.WriteLine("Menu: \n 1. Get sum\n 2. Add to sum\n 3. Get from sum \n 4. exit");
                            int choice = int.Parse(Console.ReadLine());
                            switch (choice)
                            {
                                case 1:
                                    Console.Clear();
                                    Console.WriteLine("sum is: {0}", bank[i].GetAccount.Sum);
                                    Console.WriteLine("Press any bottom to continue");
                                    Console.ReadLine();
                                    break;
                                case 2:
                                    Console.Clear();
                                    Console.WriteLine("How much you want to add? ");
                                    int add = int.Parse(Console.ReadLine());
                                    bank[i].GetAccount.Sum += add;
                                    Console.WriteLine("Sum has been added");
                                    Console.WriteLine("Press any bottom to continue");
                                    Console.ReadLine();
                                    break;
                                case 3:
                                    Console.Clear();
                                    Console.WriteLine("How much you want to get? ");
                                    int get = int.Parse(Console.ReadLine());
                                    if (get > bank[i].GetAccount.Sum)
                                    {
                                        Console.WriteLine("you want too much");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sum has been got");
                                        bank[i].GetAccount.Sum -= get;
                                        Console.WriteLine("Press any bottom to continue");
                                        Console.ReadLine();
                                        break;
                                    }
                                case 4: throw new Exception("Bye!");
                                default:
                                    Console.Clear();
                                    Console.WriteLine("invalid choice! Try again...");
                                    Console.WriteLine("Press any bottom to continue");
                                        Console.ReadLine();
                                        break;
                            }
                        }
                        else
                        {
                            possabilities--;
                            if (possabilities == 0)
                            {
                                Console.WriteLine("Account is blocked, cops is coming");
                                throw new Exception("Under arrest");
                            }

                            Console.WriteLine("Wrong password, try again! \n Attention! You have only {0} possabilities left!", possabilities);
                        }
                        Console.WriteLine("if you want to change Client, press y, or press any bottom to continue");
                        if (Console.ReadLine() == "y")
                        {
                            Console.Clear();
                            break;
                        }
                        }while(possabilities>0);
                    }
                    else
                    {
                        Console.WriteLine("Invalid account, try again. \n Press any bottom, Press q to exit");
                        if(Console.ReadLine()=="q") break;
                        
                    }

                } while (true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally { }
        }
    }
}
