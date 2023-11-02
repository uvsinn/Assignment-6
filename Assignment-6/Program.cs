using System.CodeDom.Compiler;
using System.Xml.Linq;

namespace Assignment_6
{
    class Product
    {
        readonly string pid;
        string? pname;
        int qty_in_stock;
        int discount_allowed;
        int amount;
        static string brand;

        //Parameterized constuctor
        //public Product(string p_id, string name, int qty)
        //{
        //    this.pid = p_id;
        //    this.pname = name;  
        //    this.qty_in_stock = qty;
        //    DateTime currentDateTime = DateTime.Now;
        //    string str = currentDateTime.ToString("MM / dd / yyyy");
        //    string[] myStr = str.Split('/');

        //    if(myStr[0]=="01" && myStr[1]=="26")
        //    {
        //        this.discount_allowed = 50;
        //    }
        //    else
        //        this.discount_allowed = 0;
        //}

        //Static constuctor
        static Product()
        {
            brand ="puma";
        }
        public Product(string pid)
        {
            this.pid = pid;
        }

        void add_product()
        {
            Console.WriteLine("Enter Product name\n");
            this.pname = Console.ReadLine();
            Console.WriteLine("Enter Product quantity\n");
            this.qty_in_stock = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter amount\n");
            this.amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter if any Discount available\n");
            this.discount_allowed = Convert.ToInt32(Console.ReadLine());
            
        }

        static void displayproducts(List<Product> listproducts)
        {
            foreach(var i in  listproducts)
            {
                Console.WriteLine(i.pid);
                Console.WriteLine(i.pname);
                Console.WriteLine(i.qty_in_stock);
                Console.WriteLine(i.amount);
                Console.WriteLine(i.discount_allowed);
                Console.WriteLine(brand);
            }
        }

        static bool order_products(string p_name,List<Product> listproducts)
        {
            foreach(var i in  listproducts)
            {
                if(i.pname== p_name)
                {
                    Console.WriteLine($"Product id: {i.pid}");
                    Console.WriteLine($"Product name: {i.pname}");
                    Console.WriteLine($"Quantity in stock: {i.qty_in_stock}");
                    Console.WriteLine($"Amount: {i.amount}");
                    Console.WriteLine($"Discount available: {i.discount_allowed}");
                    Console.WriteLine($"Product brand: {brand}");

                    if (i.qty_in_stock == 0)
                        return false;

                    Console.WriteLine("IF YOU WANT TO GENERATE BILL NOW THEN SELECT (Y/N)");
                    int ch = Console.Read();

                    if (ch == 'Y')
                    {
                       generatebill(p_name,listproducts);

                    }
                    else
                    {
                        return true;
                    }
                }
            }
      
            return false;
        }
        static void generatebill(string p_name, List<Product> listproducts)
        {
            foreach (var i in listproducts)
            {
                i.qty_in_stock -= 1;
                Console.WriteLine($"Your Product id: {i.pid}");
                Console.WriteLine($"Your Product name: {i.pname}");
                Console.WriteLine($"Your Product brand: {brand}");
                Console.WriteLine($"Discount available: {i.discount_allowed}");

                DateTime currentDateTime = DateTime.Now;
                string str = currentDateTime.ToString("MM / dd / yyyy");
                string[] myStr = str.Split('/');

                if (myStr[0] == "01" && myStr[1] == "26")
                {
                    i.discount_allowed = 50;
                }
                else
                    i.discount_allowed = 0;


                int amt = i.amount - (i.discount_allowed / 100);
                Console.WriteLine($"Total Amount: {amt}");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Select 1 if you are ADMIN\n Select 2 if you are a Customer");
            int choice = Convert.ToInt32(Console.ReadLine());

            List<Product> listproducts = new List<Product>();
            //ADMIN
            if(choice == 1)
            {
                Console.WriteLine("1. Add products\n 2. Display Products\n Select an option-");
                int innner_choice=Convert.ToInt32(Console.ReadLine());

                Product product = null;
                //Add product
                if (innner_choice == 1)
                {
                    Console.WriteLine("Enter the productid: ");
                    string pid=Console.ReadLine();
                    product=new Product(pid);
                    product.add_product();
                    listproducts.Add(product);
                }

                //Display products
                else if (innner_choice ==2)
                {
                    displayproducts(listproducts);
                }
                else
                {
                    Console.WriteLine("WRONG CHOICE");
                }

  
            }
            //USER
            else if(choice == 2)
            {
                Console.WriteLine("1. Order Products\n 2. Get Bill\n Select an option-");
                int innner_choice = Convert.ToInt32(Console.ReadLine());

                // Order Product
                if (innner_choice == 1)
                {
                    Console.WriteLine("Enter the product you want to order: ");
                    string orderedproduct=Console.ReadLine();

                    if (order_products(orderedproduct, listproducts) == false)
                    {
                        Console.WriteLine("Can't order this product");
                        return;
                    }


                }
                //Get Bill
                else if (innner_choice == 2)
                {
                    Console.WriteLine("Enter the product name whose bill you want to generate");
                    string p_name=Console.ReadLine();
                    generatebill(p_name,listproducts);
                }
                else
                {
                    Console.WriteLine("WRONG CHOICE");
                }
            }
        }
    }
}