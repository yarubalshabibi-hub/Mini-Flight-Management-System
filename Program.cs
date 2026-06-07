namespace Mini_Flight_Management_System
{
    internal class Program
    {
        static List<string> passengerNames = new List<string>();
        static List<string> ticketNumbers = new List<string>();
        static string[] flightNumbers = { "OA101", "OA102", "OA103", "OA104", "OA105", "OA106" };
        static List<string> availableDates = new List<string>();
        static Dictionary<string, string> bookingRecord = new Dictionary<string, string>();
        static Queue<string> checkedInQueue = new Queue<string>();
        static Stack<string> boardingStack = new Stack<string>();
        static List<string> cancelledTickets = new List<string>();
        static Dictionary<string, string> passengerSeatMap = new Dictionary<string, string>();
        static Queue<string> waitlistQueue = new Queue<string>();

        static int currentRow = 10;
        static char currentSeat = 'A';
        static void Main(string[] args)
        {
            ///passengerNames
            passengerNames.Add("Ahmed");
            passengerNames.Add("Ali");
            passengerNames.Add("Mohammed");
            passengerNames.Add("Salim");
            passengerNames.Add("Khalid");

            ///ticketNumbers
            ticketNumbers.Add("TKT-1");
            ticketNumbers.Add("TKT-2");
            ticketNumbers.Add("TKT-3");
            ticketNumbers.Add("TKT-4");
            ticketNumbers.Add("TKT-5");

            /// availableDates
            availableDates.Add("12-5-2026");
            availableDates.Add("15-6-2027");
            availableDates.Add("20-7-2028");
            availableDates.Add("05-8-2029");

            int choice = -1;
            while (choice != 0)
            {
                PrintMenu();
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Please enter a valid number: ");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        RegisterPassenger();
                        break;

                    case 2:;
                        break;

                    case 3:;
                        break;

                    case 4:;
                        break;

                    case 5:;
                        break;

                    case 6:;
                        break;

                    case 7:;
                        break;

                    case 8:;
                        break;

                    case 9:;
                        break;

                    case 10:;
                        break;

                    case 0: Console.WriteLine("Goodbye Safe travels ");
                        break;

                    default: Console.WriteLine("Invalid option Please choose 0-10");
                        break;

                }
                Console.WriteLine();
            }
        }

        static void PrintMenu()
        {
            Console.WriteLine("Enter your choice: ");
            Console.WriteLine("========================================");
            Console.WriteLine("  SKY WINGS FLIGHT MANAGEMENT SYSTEM   ");
            Console.WriteLine("========================================");
            Console.WriteLine("1.  Register New Passenger");
            Console.WriteLine("2.  View All Passengers");
            Console.WriteLine("3.  Book a Flight Ticket");
            Console.WriteLine("4.  View Booking Details");
            Console.WriteLine("5.  Update a Booking");
            Console.WriteLine("6.  Cancel a Ticket");
            Console.WriteLine("7.  Passenger Check in");
            Console.WriteLine("8.  Board Passengers Boarding Stack");
            Console.WriteLine("9.  Generate Flight Manifest");
            Console.WriteLine("10. Manage Waitlist & Seat Assignment");
            Console.WriteLine("0. Exit");
            Console.WriteLine("========================================");
        }
        static void RegisterPassenger()
        {
            Console.WriteLine("Register New Passenger: "); 
        }
        

    }
}


